using cakeslice;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CustomerMono : MonoBehaviour
{
	public struct PriceRange
	{
		public float minPrice;
		public float maxPrice;

		public PriceRange ( float minPrice, float maxPrice )
		{
			this.minPrice = minPrice;
			this.maxPrice = maxPrice;
		}
	}

	#region variables
	public Customer customer;
	public Transform customerInfoUI;
	public GameObject reviewPrefab;
	public GameObject reviewContainer;
	public Table table;

	private OutlineRender outlineComponent;
	private Animator animator;
	private float averageRating = 0;
	private float bill = 0;
	private int orderedCount = 0;
	private int receivedCount = 0;
	private bool unrecievedFood = false;
	#endregion

	#region hover
	private void Start ()
	{
		animator = customerInfoUI.GetComponent<Animator>();
		outlineComponent = transform.GetChild(0).GetChild(1).gameObject.AddComponent<OutlineRender>();
		outlineComponent.enabled = false;
	}

	private void OnMouseEnter ()
	{
		animator.SetBool("pop", true);
		UI.ChildText(customerInfoUI, 0, customer.Name);
		UI.ChildText(customerInfoUI, 1, customer.Wealth.ToString());
		UI.MoveUIToGameObjectPosition(customerInfoUI.gameObject, transform.position, 0, 50);
		outlineComponent.enabled = true;
	}

	private void OnMouseExit ()
	{
		animator.SetBool("pop", false);
		outlineComponent.enabled = false;
	}

	private void OnDisabled ()
	{
		animator.SetBool("pop", false);
		outlineComponent.enabled = false;
	}
	#endregion

	#region ordering

	#region public_methods
	/// <summary>
	/// Customer rates and pays the received food and is deleted from the memory
	/// </summary>
	public void LeaveRestaurant ()
	{
		Debug.Log("Customer on table " + table.Id + " leaves! ");
		Pay(bill);
		if ( UnityEngine.Random.Range(1, 30) == 13 )
		{
			var review = Instantiate(reviewPrefab, reviewContainer.transform);
			int rating = receivedCount > 0 ? ( int ) averageRating / receivedCount : 0;
			review.GetComponent<ReviewInfo>().SetInfo(rating, customer.Name);
		}
		table.CustomersOnTable--;
		customer.UpdateVisitPercentage(averageRating);
	}

	public IEnumerator LeaveRestaurant(float time)
	{
		yield return new WaitForSecondsRealtime(time);

		LeaveRestaurant();
	}

	/// <summary>
	/// Pick and order food determined by the customer wealth
	/// </summary>
	public void OrderFood ()
	{
		// Determine the price range of the customer
		PriceRange priceRange = GetPriceRangeFromWealth();

		// Get all possible recipes within the pricerange
		List<RecipeManager.ActiveRecipe> possibleRecipes = RecipeManager.Instance.ActiveRecipes.FindAll(r => r.Price > priceRange.minPrice && r.Price < priceRange.maxPrice);
		if ( possibleRecipes.Count == 0 ) LeaveRestaurant();

		// Randomize a number of meals to be ordered
		int numberOfFoods = UnityEngine.Random.Range(1, (int)customer.Wealth * 2);
		// Validate
		if ( numberOfFoods > possibleRecipes.Count ) numberOfFoods = possibleRecipes.Count;

		// Remember how many meals were ordered
		orderedCount = numberOfFoods;

		// Pick meals
		PickMeals(possibleRecipes, numberOfFoods);
	}

	/// <summary>
	/// Customer receives the food and will pay and rate it
	/// </summary>
	/// <param name="order"></param>
	internal void ReceiveFood ( Order order )
	{
		Debug.Log("Customer (<b>" + customer.Name + "</b>) received " + order.recipe);
		Rate(order);
		bill += RecipeManager.Instance.FindActiveRecipeFromRecipe(order.recipe).Price;
		if ( orderedCount == ++receivedCount )
		{
			LeaveRestaurant();
		}
	}

	/// <summary>
	/// If a problem occured a meal can be declined
	/// </summary>
	internal void DeclineFood ()
	{
		orderedCount--;
		unrecievedFood = true;
		if(orderedCount == 0)
		{
			StartCoroutine(LeaveRestaurant(3));
		}
	}
	#endregion

	#region private_methods
	/// <summary>
	/// Pick random meals from a given list of recipes
	/// </summary>
	/// <param name="possibleRecipes"></param>
	/// <param name="numberOfFoods"></param>
	private void PickMeals ( List<RecipeManager.ActiveRecipe> possibleRecipes, int numberOfFoods )
	{
		for ( int i = 0 ; i < numberOfFoods ; i++ )
		{
			int randomIndex = UnityEngine.Random.Range(0, possibleRecipes.Count);
			AddOrder(possibleRecipes[randomIndex].Recipe);
			possibleRecipes.RemoveAt(randomIndex); // be sure not to order the same meal twice
		}
	}

	/// <summary>
	/// Adds an order to the order stack
	/// </summary>
	/// <param name="recipe"></param>
	private void AddOrder ( Recipe recipe )
	{
		Order order = new Order(recipe, DayCycle.Instance.GameTime)
		{
			customerMono = this,
			table = transform.parent.parent.GetComponent<Table>()
		};
		OrderStack.Instance.allOrders.Add(order);
		Debug.Log("Customer (+<b>" + customer.Name + "</b>)  ordered " + recipe.name);
	}

	/// <summary>
	/// Adds the rating of the meal to the overall rating variable
	/// </summary>
	/// <param name="order"></param>
	private void Rate ( Order order )
	{
		var rating = 5 * (order.AverageQuality) + 1 * (1 - (order.orderData.MinutesPassed(DayCycle.Instance.GameTime) - 15) / 35) + 4 * (1 - (order.recipe.Cost - 5) / 20);
		rating += Random.Range(-2f, 2f);
		rating = Mathf.RoundToInt(Mathf.Clamp(rating, 1, 10));
		averageRating += rating;
	}

	/// <summary>
	/// The customer pays for the meals received
	/// </summary>
	/// <param name="amount"></param>
	private void Pay ( float amount )
	{
		Budget.Instance.AddFunds(amount);
		Budget.Instance.AnimateIncome(amount);
	}

	/// <summary>
	/// Gives a range of prices determined by the customer wealth
	/// </summary>
	/// <returns></returns>
	private PriceRange GetPriceRangeFromWealth ()
	{
		switch ( customer.Wealth )
		{
			case Wealthiness.Poor:
				return new PriceRange(1, 6);
			case Wealthiness.Avarage:
				return new PriceRange(2, 9);
			case Wealthiness.Rich:
				return new PriceRange(3, 13);
			case Wealthiness.Millionaire:
				return new PriceRange(2, 23);
			default:
				throw new System.Exception("Unexpected customer wealth!");
		}
	}
	#endregion
	#endregion
}

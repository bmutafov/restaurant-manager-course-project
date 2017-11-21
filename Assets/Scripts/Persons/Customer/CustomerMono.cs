using cakeslice;
using System.Collections.Generic;
using UnityEngine;

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

	private OutlineRender outlineComponent;
	#endregion

	#region hover
	private void Start ()
	{
		outlineComponent = transform.GetChild(0).GetChild(1).gameObject.AddComponent<OutlineRender>();
		outlineComponent.enabled = false;
	}

	private void OnMouseEnter ()
	{
		customerInfoUI.gameObject.SetActive(true);
		UI.UpdateChildTextMeshText(customerInfoUI, 0, customer.Name);
		UI.MoveUIToGameObjectPosition(customerInfoUI.gameObject, transform.position, 0, 50);
		outlineComponent.enabled = true;
	}

	private void OnMouseExit ()
	{
		customerInfoUI.gameObject.SetActive(false);
		outlineComponent.enabled = false;
	}
	#endregion

	#region ordering
	public void OrderFood ()
	{
		PriceRange priceRange = GetPriceRangeFromWealth();
		List<RecipeManager.ActiveRecipe> possibleRecipes = RecipeManager.Instance.ActiveRecipes.FindAll(r => r.Price > priceRange.minPrice && r.Price < priceRange.maxPrice);
		Debug.Log(possibleRecipes.Count);
		int numberOfFoods = Random.Range(1, (int)customer.Wealth * 2);
		if ( numberOfFoods > possibleRecipes.Count ) numberOfFoods = possibleRecipes.Count;
		for ( int i = 0 ; i < numberOfFoods ; i++ )
		{
			int randomIndex = Random.Range(0, possibleRecipes.Count);
			AddOrder(possibleRecipes[randomIndex].Recipe);
			possibleRecipes.RemoveAt(randomIndex);
		}
	}

	private void AddOrder (Recipe recipe)
	{
		Order order = new Order(recipe, DayCycle.Instance.GameTime)
		{
			customer = customer,
			table = transform.parent.parent.GetComponent<Table>()
		};
		OrderStack.Instance.allOrders.Add(order);
		Debug.Log("Customer " + customer.Id + " ordered " + recipe.name);
	}

	private PriceRange GetPriceRangeFromWealth ()
	{
		switch ( customer.Wealth )
		{
			case Wealthiness.Poor:
				return new PriceRange(3, 6);
			case Wealthiness.Avarage:
				return new PriceRange(4, 9);
			case Wealthiness.Rich:
				return new PriceRange(8, 13);
			case Wealthiness.Millionaire:
				return new PriceRange(11, 23);
			default:
				throw new System.Exception("Unexped customer wealth!");
		}
	}
	#endregion
}

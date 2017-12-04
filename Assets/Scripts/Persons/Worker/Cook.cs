using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cook : Worker
{
	#region variables
	[SerializeField]
	private int ordersAtATime = 0;

    private List<Order> currentOrders = new List<Order>();
    #endregion

    public Cook (string name, int skill) : base(name, skill)
    {
		currentOrders = new List<Order>();
    }

	public Cook (Cook cook) : base(cook.Name, cook.skill)
	{
		currentOrders = new List<Order>();
		SetId = cook.Id;
		ordersAtATime = cook.ordersAtATime;
		salaryPerHour = cook.salaryPerHour;
	}

	#region overrides
	public override void DoWork ()
    {
        Order newOrder = GetNewOrder();
        if ( newOrder != null )
        {
            if ( AreEnoughIngredients(newOrder) )
            {
                Debug.Log("Cooking " + newOrder.recipe.recipeName);
                TakeIngredients(newOrder);
                currentOrders.Add(newOrder);
                newOrder.recipe.CalculateCost();
            }
            else
            {
				newOrder.customerMono.DeclineFood();
                Debug.Log("Not enough ingredients!");
            }
        }

        CheckForCookedOrders();
    }

    public override void CalculateWorkloadFromSkill ()
    {
        ordersAtATime = skill;
    }
    #endregion

    #region private_methods

    /// <summary>
    /// Selects a new order from the OrderStack list to cook
    /// </summary>
    /// <returns>Order if available, null if not</returns>
    private Order GetNewOrder ()
    {
        try
        {
			if ( currentOrders == null ) currentOrders = new List<Order>();
            if ( currentOrders.Count < ordersAtATime && OrderStack.Instance.allOrders.FindAll(order => !order.isBeingCooked).Count > 0 )
            {
                Debug.Log("Cook is available for an order!");
                Order newOrder = OrderStack.Instance.allOrders.Find(order => !order.isBeingCooked);
                newOrder.isBeingCooked = true;
                return newOrder;
            }
            return null;

        }
        catch ( System.Exception e )
        {
            Debug.LogError("Could not get order, cook Id " + Id + " Error <color=red> " + e.Message + "</color>");
            return null;
        }
    }

    /// <summary>
    /// Iterates through the List from current orders.
    /// 
    /// Finds orders which time ordered + prepartion time = current time.
    /// 
    /// If found removes them from the list and adds them to the
    /// ready to serve OrderStack
    /// </summary>
    private void CheckForCookedOrders ()
    {
        for ( int i = 0 ; i < currentOrders.Count ; i++ )
        {
            var order = currentOrders[i];
            if ( order.orderData.MinutesPassed(DayCycle.Instance.GameTime) == order.recipe.preparationTime )
            {
                Debug.Log("Order " + order.recipe.recipeName + " cooked! ");
                currentOrders.RemoveAt(i);
                OrderStack.Instance.CookOrder(order);
                i--;
            }
        }
    }

    /// <summary>
    /// Asks the Storage class if there are enough ingredients for this order
    /// </summary>
    /// <param name="order"> Reference to the order </param>
    /// <returns> true if order can be cooked ; false otherwise </returns>
    private bool AreEnoughIngredients (Order order)
    {
        Recipe recipe = order.recipe;
        for ( int i = 0 ; i < recipe.ingredients.Count ; i++ )
        {
            if ( !Storage.Instance.IsInStock(recipe.ingredients[i], recipe.ingredientAmount[i]) )
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Takes the ingredients from the Storage class
    /// </summary>
    /// <param name="order"> order reference </param>
    private void TakeIngredients (Order order)
    {
        Recipe recipe = order.recipe;
        float averageQuality = 0;
        for ( int i = 0 ; i < recipe.ingredients.Count ; i++ )
        {
            averageQuality = Storage.Instance.TakeIngredient(recipe.ingredients[i], recipe.ingredientAmount[i]) / (i + 1);
        }
        order.AverageQuality = averageQuality;
    }
    #endregion

    #region public_methods

    #endregion
}

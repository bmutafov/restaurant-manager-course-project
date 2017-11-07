using System.Collections.Generic;
using UnityEngine;

public class Cook : Worker
{
    #region variables
    private int ordersAtATime = 0;

    private List<Order> currentOrders = new List<Order>();
    #endregion

    public Cook (string name, int skill) : base(name, skill)
    {
    }

    #region overrides
    public override void DoWork ()
    {
        Order newOrder = GetNewOrder();
        if ( newOrder != null )
        {
            if ( AreEnoughIngredients(newOrder) )
            {
                Debug.Log("Cooking " + newOrder.recipe.name);
                TakeIngredients(newOrder);
                currentOrders.Add(newOrder);
                newOrder.recipe.CalculateCost();
            }
            else
            {
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
    /// if:
    ///     - current orders who are being cooked are less than cookers ordersAtATime
    ///     - there are orders which have not began cooking yet
    ///     
    /// returns:
    ///     - order reference if any
    ///     - null if conditions are not met
    /// </summary>
    /// <returns></returns>
    private Order GetNewOrder ()
    {
        try
        {
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
    /// Iterates through the List from current orders
    /// 
    /// finds orders which time ordered + prepartion time = current time
    /// 
    /// if found removes them from the list and adds them to the
    /// ready to serve OrderStack
    /// </summary>
    private void CheckForCookedOrders ()
    {
        for ( int i = 0 ; i < currentOrders.Count ; i++ )
        {
            var order = currentOrders[i];
            if ( order.orderData.MinutesPassed(DayCycle.Instance.GameTime) == order.recipe.preparationTime )
            {
                Debug.Log("Order " + order.recipe.name + " cooked! ");
                currentOrders.RemoveAt(i);
                OrderStack.Instance.CookOrder(order);
                i--;
            }
        }
    }

    /// <summary>
    /// Asks the Storage class if there are enough igredients for this order
    /// </summary>
    /// <param name="order"> reference to the order </param>
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

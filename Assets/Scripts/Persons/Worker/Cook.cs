using System.Collections;
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
            currentOrders.Add(newOrder);
        }

        CheckForCookedOrders();
    }

    public override void CalculateWorkloadFromSkill ()
    {
        ordersAtATime = skill;
    }
    #endregion

    #region private_methods
    private Order GetNewOrder ()
    {
        try
        {
            if ( currentOrders.Count < ordersAtATime && OrderStack.Instance.allOrders.FindAll(order => !order.isBeingCooked).Count > 0 )
            {
                Debug.Log("Cooker started cooking an order!");
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
    #endregion

    #region public_methods

    #endregion
}

using System;

public class Order
{

    #region variables
    public Recipe recipe;
    public Customer customer;

    public struct OrderData
    {

        public DateTime timeOrdered;

        public OrderData (DateTime timeOrdered) : this()
        {
            this.timeOrdered = timeOrdered;
        }

        public int MinutesPassed (DateTime timeServed)
        {
            return ( int ) (timeServed - timeOrdered).TotalMinutes;
        }
    }

    public OrderData orderData;
    public bool isBeingCooked = false;
    #endregion

    public Order (Recipe recipe, DateTime timeOrdered)
    {
        this.recipe = recipe;
        orderData = new OrderData(timeOrdered);
    }



}

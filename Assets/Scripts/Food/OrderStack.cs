using System.Collections.Generic;

public class OrderStack : GenericSingletonClass<OrderStack>
{
    public List<Order> allOrders;
    public List<Order> cookedOrders;
    public List<Order> servedOrders;

    private void Start ()
    {
        allOrders = new List<Order>();
        cookedOrders = new List<Order>();
        servedOrders = new List<Order>();
    }

    /// <summary>
    /// Moves Order from allOrders to cookedOrders
    /// </summary>
    /// <param name="order"></param>
    public void CookOrder (Order order)
    {
        allOrders.Remove(order);
        cookedOrders.Add(order);
    }

    /// <summary>
    /// Moves Order from cookedOrders to servedOrders
    /// </summary>
    /// <param name="order"></param>
    public void ServeOrder (Order order)
    {
        cookedOrders.Remove(order);
        servedOrders.Add(order);
		order.customerMono.ReceiveFood(order);
    }
}

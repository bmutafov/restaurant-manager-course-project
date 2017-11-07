using UnityEngine;
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

    private void Update ()
    {

    }

    public void CookOrder (Order order)
    {
        allOrders.Remove(order);
        cookedOrders.Add(order);
    }

    public void ServeOrder (Order order)
    {
        cookedOrders.Remove(order);
        servedOrders.Add(order);
    }
}

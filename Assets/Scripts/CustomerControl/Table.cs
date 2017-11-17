using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {
    private static int lastUsedId = 0;

    private int id;
    public int maxPeople = 4;
    public bool isTaken = false;
    public bool isRound = false;

    public List<CustomerGroup> customerGroup;
    public List<Order> tableOrders;

    public int Id
    {
        get
        {
            return id;
        }
    }

    private void Start ()
    {
        id = lastUsedId++;
    }
}

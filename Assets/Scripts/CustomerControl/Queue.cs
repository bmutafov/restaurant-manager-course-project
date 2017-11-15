using System.Collections.Generic;
using UnityEngine;

public class Queue : GenericSingletonClass<Queue>
{

    public GameObject customerPrefab;
    [Range(0, 1)]
    public float queueLineSpread = 0.5f;
    [Range(0, 2)]
    public float xDistance = 1;


    private List<Customer> customersInQueue;

    private void Start ()
    {
        customersInQueue = new List<Customer>();
        DayCycle.Instance.onMinuteChangedCallback += SpawnCustomers;
    }

    public void AddCustomer (Customer customer)
    {
        customersInQueue.Add(customer);

        Vector3 position = new Vector3((transform.localScale.x / 2) - customersInQueue.Count * xDistance, 1, Random.Range(-queueLineSpread, queueLineSpread));
        GameObject instance = Instantiate(customerPrefab, transform);
        instance.transform.position += position;
        instance.name = "Customer" + customer.Id;
    }

    public void AddCustomers (List<Customer> customers)
    {
        foreach ( Customer customer in customers )
        {
            AddCustomer(customer);
        }
    }

    public void RemoveCustomer (Customer customer)
    {
        customersInQueue.Remove(customer);

        MoveQueueForwards();
    }

    public void RemoveCustomers (List<Customer> customers)
    {
        foreach ( var customer in customers )
        {
            RemoveCustomer(customer);
        }
    }

    public void MoveCustomerToTable (Customer customer, Table table)
    {
        Transform tableTransform = table.transform;
        for ( int i = 0 ; i < tableTransform.childCount ; i++ )
        {
            Transform child = tableTransform.GetChild(i);
            if ( child.childCount == 0 )
            {
                GameObject customerGO = GameObject.Find("Customer" + customer.Id);
                customerGO.transform.parent = child;
                customerGO.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    public void MoveCustomersToTable (List<Customer> customers, Table table)
    {
        foreach ( Customer customer in customers )
        {
            MoveCustomerToTable(customer, table);
        }
    }

    private void MoveQueueForwards ()
    {
        for ( int i = 0 ; i < transform.childCount ; i++ )
        {
            transform.GetChild(i).position += new Vector3(xDistance, 0, 0);
        }
    }

    private void SpawnCustomers ()
    {
        CustomerManager.Instance.GetVisitingNow().ForEach(c => AddCustomers(c.customers));
    }
}

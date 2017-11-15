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

        MoveCustomerToTable(customer);
        MoveQueueForwards();
    }

    public void RemoveCustomers (List<Customer> customers)
    {
        foreach ( var customer in customers )
        {
            RemoveCustomer(customer);
        }
    }

    private void MoveCustomerToTable (Customer customer)
    {
        Destroy(transform.Find("Customer" + customer.Id).gameObject);

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

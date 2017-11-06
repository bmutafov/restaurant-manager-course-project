using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : GenericSingletonClass<CustomerManager>
{
    #region variables
    public List<Customer> allCustomers;
    public int customerPoolSize = 50;

    [Range(0, 1)]
    public float startingVisitPercentage = 0.5f;
    #endregion


    private void Start ()
    {
        if ( !Load.Customers() )
        {
            allCustomers = new List<Customer>();
            GenerateCustomersPool();
        }
    }

    #region customer_generation
    private void GenerateCustomersPool ()
    {
        for ( int i = 0 ; i < customerPoolSize ; i++ )
        {
            Customer customer = new Customer(Generate.Name(), Generate.Wealth(), startingVisitPercentage);
            allCustomers.Add(customer);
            Debug.Log("Customer " + customer.Name + " created!");
        }
    }
    #endregion


}

using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : GenericSingletonClass<CustomerManager>
{
    #region variables_generation
    public AllCustomers allCustomers;
    public int customerPoolSize = 50;

    [Range(0, 1)]
    public float startingVisitPercentage = 0.5f;
    #endregion

    #region variables_visit
    public GameObject spawnNodes;
    public List<CustomerGroup> customerGroups;
    #endregion

    private void Start ()
    {
        if ( !Load.Customers() )
        {
            allCustomers = new AllCustomers();
            GenerateCustomersPool();
        }

        customerGroups = new List<CustomerGroup>();

        DayCycle.Instance.onDayStartedCallback += CustomersNewDay;
    }

    #region customer_generation
    private void GenerateCustomersPool ()
    {
        for ( int i = 0 ; i < customerPoolSize ; i++ )
        {
            Customer customer = new Customer(Generate.Name(), Generate.Wealth(), startingVisitPercentage);
            allCustomers.list.Add(customer);
            Debug.Log("Customer " + customer.Name + " created!");
        }
        Debug.Log(allCustomers.list.Count);
    }
    #endregion

    #region customer_visit
    private void CustomersNewDay ()
    {
        List<Customer> visitingCustomers = GenerateVisitingCustomersForTheDay();
        GenerateGroupsFromCustomers(visitingCustomers);
    }

    /// <summary>
    /// Generates a list for the amount of visiting customers for the day
    /// </summary>
    /// <returns>List</returns>
    private List<Customer> GenerateVisitingCustomersForTheDay ()
    {
        int seatsCount = spawnNodes.transform.childCount;
        int customerMaxCount = seatsCount + ( int ) (seatsCount * 0.5f) + (DayCycle.Instance.closingHour - DayCycle.Instance.openingHour) * seatsCount;

        List<Customer> customerPool = allCustomers.GetRandomCustomers(customerMaxCount);
        List<Customer> visitingCustomers = new List<Customer>();

        for ( int i = 0 ; i < customerMaxCount ; i++ )
        {
            float chance = Random.Range(0, 1);
            if ( chance < customerPool[i].VisitPercentage )
            {
                visitingCustomers.Add(customerPool[i]);
            }
        }

        return visitingCustomers;
    }

    /// <summary>
    /// Splits a customer list into groups of 2-4 people
    /// </summary>
    /// <param name="customers">List of customers to be seperated by groups</param>
    private void GenerateGroupsFromCustomers (List<Customer> customers)
    {
        List<Customer> customerCopy = new List<Customer>(customers);
        while ( customerCopy.Count > 0 )
        {
            int personCount = Random.Range(2, 4);
            if ( personCount > customerCopy.Count )
            {
                personCount = customerCopy.Count;
            }

            List<Customer> cust = new List<Customer>();
            cust.AddRange(customerCopy.GetRange(0, personCount));
            customerCopy.RemoveRange(0, personCount);

            CustomerGroup cg = new CustomerGroup(cust);
            customerGroups.Add(cg);
        }
    }

    #endregion
}

using System.Collections.Generic;
using UnityEngine;

public class AllCustomers
{
    public List<Customer> list;

    public AllCustomers ()
    {
        list = new List<Customer>();
    }

    public List<Customer> GetRandomCustomers(int count)
    {
        List<Customer> results = new List<Customer>();
        for ( int i = 0 ; i < count ; i++ )
        {
            results.Add(list[Random.Range(0, list.Count)]);
        }
        return results;
    }
}

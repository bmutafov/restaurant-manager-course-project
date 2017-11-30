using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllCustomers
{
    public List<Customer> list;

    public AllCustomers ()
    {
        list = new List<Customer>();
    }

    public List<Customer> GetRandomCustomers (int count)
    {
        List<int> addedCustomerIndexes = new List<int>();

        List<Customer> results = new List<Customer>();
        for ( int i = 0 ; i < count ; i++ )
        {
            if ( addedCustomerIndexes.Count == list.Count )
                break;

            int index = 0;
            while ( addedCustomerIndexes.Contains(index) )
                index = Random.Range(0, list.Count);

            addedCustomerIndexes.Add(index);

            results.Add(list[index]);
        }

        Debug.Log("Returning a list of " + results.Count + " customers! ");
        return results;
    }
}

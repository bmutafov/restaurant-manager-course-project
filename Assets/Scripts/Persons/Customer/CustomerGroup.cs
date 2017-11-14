using System;
using System.Collections.Generic;

public class CustomerGroup
{
    public List<Customer> customers;
    public DateTime visitTime;

    public CustomerGroup ()
    {
        customers = new List<Customer>();
    }

    public CustomerGroup (List<Customer> customers)
    {
        this.customers = customers;
    }
}
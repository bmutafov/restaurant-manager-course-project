using UnityEngine;
using System.Collections.Generic;

public class Host : Worker
{
    private int minutesPerGroup;
    private List<CustomerGroup> groupsToPlace;

    private int minutesPassed = 0;

    public Host (string name, int skill) : base(name, skill)
    {
        DayCycle.Instance.onMinuteChangedCallback += FindGroupsByMinute;
        groupsToPlace = new List<CustomerGroup>();
    }

    public override void CalculateWorkloadFromSkill ()
    {
        minutesPerGroup = 12 - skill;
    }

    public override void DoWork ()
    {
        if ( groupsToPlace.Count == 0 || minutesPassed < minutesPerGroup )
            return;


        List<Worker> waiters = RestaurantManager.Instance.Waiters;
        int mostFreeTables = 0;
        Waiter freeWaiter = null;
        Table waiterFreeTable = null;
        foreach ( Waiter thisWaiter in waiters )
        {
            int freeTables = 0;
            Table freeTable = null;
            foreach ( var table in thisWaiter.Tables )
            {
                if ( !table.isTaken )
                {
                    freeTables++;
                    freeTable = table;
                }
            }
            if ( freeTables == 0 )
                continue;

            if ( freeTables > mostFreeTables || (freeTables == mostFreeTables && freeWaiter.Tables.Count > thisWaiter.Tables.Count) )
            {
                mostFreeTables = freeTables;
                freeWaiter = thisWaiter;
                waiterFreeTable = freeTable;
            }
        }
        if ( freeWaiter == null )
            return;

        minutesPassed = 0;

        waiterFreeTable.isTaken = true;

        Debug.Log("Group has been placed on table #" + waiterFreeTable.Id);


        Debug.Log(groupsToPlace[0].visitTime + " seated.");
        Queue.Instance.MoveCustomersToTable(groupsToPlace[0].customers, waiterFreeTable);
        Queue.Instance.RemoveCustomers(groupsToPlace[0].customers);

        groupsToPlace.RemoveAt(0);
    }

    private void FindGroupsByMinute ()
    {
        minutesPassed++;
        var visitingNow = CustomerManager.Instance.GetVisitingNow();
        if ( visitingNow != null )
        {
            groupsToPlace.AddRange(visitingNow);
        }
    }
}

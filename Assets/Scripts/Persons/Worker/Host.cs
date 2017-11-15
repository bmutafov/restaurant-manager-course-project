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
        minutesPerGroup = 20 - skill;
    }

    public override void DoWork ()
    {
        if ( groupsToPlace.Count == 0 || minutesPassed < minutesPerGroup )
            return;

        minutesPassed = 0;

        Debug.Log(groupsToPlace[0].visitTime + " seated.");
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

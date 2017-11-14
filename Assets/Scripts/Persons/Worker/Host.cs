using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Host : Worker
{
    private int minutesPerGroup;
    private List<CustomerGroup> groupsToPlace;

    public Host (string name, int skill) : base(name, skill)
    {
        DayCycle.Instance.onMinuteChangedCallback += FindGroupsByMinute;
    }

    public override void CalculateWorkloadFromSkill ()
    {
        minutesPerGroup = 11 - skill;
    }

    public override void DoWork ()
    {
        if ( groupsToPlace == null )
            return;
    }

    private void FindGroupsByMinute ()
    {
        System.DateTime currentGameTime = DayCycle.Instance.GameTime;

        System.Predicate<CustomerGroup> match = cg => cg.visitTime.Hour == currentGameTime.Hour && cg.visitTime.Minute == currentGameTime.Minute;
        List<CustomerGroup> visitingNow = CustomerManager.Instance.customerGroups.FindAll(match);
        if ( visitingNow != null )
        {
            groupsToPlace.AddRange(visitingNow);
        }
    }
}

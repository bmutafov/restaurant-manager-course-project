using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Host : Worker
{
	#region variables
	[SerializeField]
	private int minutesPerGroup;
	private List<CustomerGroup> groupsToPlace = new List<CustomerGroup>();
	private int minutesPassed = 0;
	#endregion

	public Host ( string name, int skill ) : base(name, skill)
	{
		DayCycle.Instance.onMinuteChangedCallback += FindGroupsByMinute;
		groupsToPlace = new List<CustomerGroup>();
	}

	public Host ( Host host ) : base(host.Name, host.skill)
	{
		DayCycle.Instance.onMinuteChangedCallback += FindGroupsByMinute;
		groupsToPlace = new List<CustomerGroup>();
		SetId = host.Id;
		minutesPerGroup = host.minutesPerGroup;
		salaryPerHour = host.salaryPerHour;
	}

	#region overrides
	public override void CalculateWorkloadFromSkill ()
	{
		minutesPerGroup = 12 - skill;
	}

	public override void DoWork ()
	{
		if ( groupsToPlace.Count == 0 || minutesPassed < minutesPerGroup || !DayCycle.Instance.IsDay)
			return;


		List<Worker> waiters = RestaurantManager.Instance.Waiters;
		int mostFreeTables = 0;
		Waiter freeWaiter = null;
		Table waiterFreeTable = null;
		foreach ( Waiter thisWaiter in waiters )
		{
			int freeTables = 0;
			Table freeTable = null;

			CountWaitersFreeTables(thisWaiter, ref freeTables, ref freeTable);

			if ( freeTables == 0 )
				continue;

			if ( freeTables > mostFreeTables ||
				(freeTables == mostFreeTables && freeWaiter.Tables.Count > thisWaiter.Tables.Count) )
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
		//Queue.Instance.RemoveCustomers(groupsToPlace[0].customers);

		groupsToPlace.RemoveAt(0);
	}
	#endregion

	#region private_methods
	private static void CountWaitersFreeTables ( Waiter thisWaiter, ref int freeTables, ref Table freeTable )
	{
		foreach ( var table in thisWaiter.Tables )
		{
			if ( !table.isTaken )
			{
				freeTables++;
				freeTable = table;
			}
		}
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
	#endregion
}

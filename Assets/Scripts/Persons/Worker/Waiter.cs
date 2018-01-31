using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waiter : Worker
{
	#region variables
	[SerializeField]
	private int serveMinutes = 0;

    private int lastMinuteServed = 0;

    private List<Table> tables = new List<Table>();

	private Order order;
	public Vector3 idlePosition = new Vector3(28f, 0, -5);
	public bool isServing = false;

    public List<Table> Tables
    {
        get
        {
            return tables;
        }
    }

	public Order CurrentOrder
	{
		get
		{
			return order;
		}
	}

	public int ServeMinutes
	{
		get
		{
			return serveMinutes;
		}
	}
    #endregion

    public Waiter (string name, int skill) : base(name, skill)
    {
        DayCycle.Instance.onHourChangedCallback += UpdateLastMinuteServedIfHourChanged;
		tables = new List<Table>();
    }

	public Waiter (Waiter waiter) : base(waiter.Name, waiter.skill)
	{
		DayCycle.Instance.onHourChangedCallback += UpdateLastMinuteServedIfHourChanged;
		tables = new List<Table>();
		SetId = waiter.Id;
		serveMinutes = waiter.serveMinutes;
		salaryPerHour = waiter.salaryPerHour;
	}

	#region overrides
	public override void DoWork ()
    {
        if ( IsTimeToServe() && OrderStack.Instance.cookedOrders.Count > 0 )
        {
			Order orderToServe = OrderStack.Instance.cookedOrders.Find(o => tables.Contains(o.table));
			if ( orderToServe == null ) return;

			isServing = true;
            lastMinuteServed = DayCycle.Instance.GameTime.Minute;
			order = orderToServe;
            OrderStack.Instance.ServeOrder(orderToServe);
        } else
		{
			isServing = false;
		}
    }

    public override void CalculateWorkloadFromSkill ()
    {
        serveMinutes = 13 - skill;
    }
    #endregion

    #region private_methods
    /// <summary>
    /// Checks if ample time has passed since last serving
    /// </summary>
    /// <returns> True if is ready to serve; false if not </returns>
    private bool IsTimeToServe ()
    {
        if ( lastMinuteServed == 0 )
            return true;

		return DayCycle.Instance.GameTime.Minute - lastMinuteServed == serveMinutes;
    }

    private void UpdateLastMinuteServedIfHourChanged ()
    {
        lastMinuteServed = 0;
    }
    #endregion

    #region public_methods
    public void AddTable(Table table)
    {
        Debug.Log("Worker " + Id + " is serving for table number " + table.Id);
        tables.Add(table);
    }

	public void ClearTablesList ()
    {
		if ( tables == null ) tables = new List<Table>();
        tables.Clear();
    }
    #endregion
}

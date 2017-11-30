using System.Collections.Generic;
using UnityEngine;

public class Waiter : Worker
{
    #region variables
    private int serveMinutes = 0;

    private int lastMinuteServed = 0;

    private List<Table> tables = new List<Table>();

    public List<Table> Tables
    {
        get
        {
            return tables;
        }
    }
    #endregion

    public Waiter (string name, int skill) : base(name, skill)
    {
        DayCycle.Instance.onHourChangedCallback += UpdateLastMinuteServedIfHourChanged;
    }

    #region overrides
    public override void DoWork ()
    {
        if ( IsTimeToServe() && OrderStack.Instance.cookedOrders.Count > 0 )
        {
            lastMinuteServed = DayCycle.Instance.GameTime.Minute;
            Order orderToServe = OrderStack.Instance.cookedOrders[0];
            OrderStack.Instance.ServeOrder(orderToServe);
            Debug.Log(orderToServe.recipe.recipeName + " served! Food quality: " + orderToServe.AverageQuality);
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

        if ( DayCycle.Instance.GameTime.Minute - lastMinuteServed == serveMinutes )
        {
            return true;
        }
        return false;
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
        tables.Clear();
    }
    #endregion
}

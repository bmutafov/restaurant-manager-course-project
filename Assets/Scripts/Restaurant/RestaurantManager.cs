using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantManager : GenericSingletonClass<RestaurantManager>
{

    #region variables
    public GameObject spawnParent;

    private Table[] tables;
    private List<Worker> workers;

    public List<Worker> Workers
    {
        get
        {
            return workers;
        }
    }

    public List<Worker> Waiters
    {
        get
        {
            return workers.FindAll(w => w is Waiter);
        }
    }
    #endregion

    private void Start ()
    {
        workers = new List<Worker>();
        tables = spawnParent.GetComponentsInChildren<Table>();
        FindWorkers();
        SeperateWaitersByTables();
    }

    private void FindWorkers ()
    {
        workers.Clear();
        GameObject[] workersObj = GameObject.FindGameObjectsWithTag("Worker");
        for ( int i = 0 ; i < workersObj.Length ; i++ )
        {
            workers.Add(workersObj[i].GetComponent<WorkerMono>().worker);
        }
    }

    public void SeperateWaitersByTables ()
    {
        List<Worker> waiters = Waiters;
        foreach(Waiter w in waiters)
        {
            w.ClearList();
        }

        int currentTable = 0;
        for ( int i = 0 ; currentTable < tables.Length ; i++ )
        {
            foreach ( Waiter worker in waiters )
            {
                worker.AddTable(tables[currentTable++]);
                if ( currentTable >= tables.Length )
                    break;
            }
        }
    }
}

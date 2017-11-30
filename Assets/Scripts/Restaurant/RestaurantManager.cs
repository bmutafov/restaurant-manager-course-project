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
		set
		{
			workers = value;
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
        //FindWorkers();
        SeperateWaitersByTables();
		InstantateWorker(new Host("Ivaylo Dimitrov", 9));
    }

	private void Awake ()
	{
		InstantateWorker(new Host("Pesho", 12));
	}

	private void Update ()
	{
		if(Input.GetKeyDown("h"))
		{
			
		}
	}

	public void SpawnWorkers ()
	{
		Debug.Log('a');
		foreach ( var worker in workers )
		{
			InstantateWorker(worker);
		}
	}

	public void InstantateWorker ( Worker worker )
	{
		var obj = Instantiate(new GameObject());
		obj.AddComponent<WorkerMono>().worker = worker;
		obj.name = worker.Name;
		obj.tag = "Worker";
		workers.Add(worker);
		Debug.Log(workers.Count);
	}

	public void FireWorker ( WorkerMono instance )
	{
		workers.Remove(instance.worker);
		Destroy(instance.gameObject);
	}

	internal void FindWorkers ()
    {
        workers.Clear();
		List<WorkerMono> workersObj = new List<WorkerMono>();
		workersObj.AddRange(FindObjectsOfType<WorkerMono>());
        for ( int i = 0 ; i < workersObj.Count ; i++ )
        {
            workers.Add(workersObj[i].GetComponent<WorkerMono>().worker);
        }
    }

    public void SeperateWaitersByTables ()
    {
        List<Worker> waiters = Waiters;
		if ( waiters == null || waiters.Count == 0) return;

        foreach(Waiter w in waiters)
        {
            w.ClearTablesList();
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

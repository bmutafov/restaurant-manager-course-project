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

	public bool HasEveryWorker
	{
		get
		{
			return workers.Exists(h => h is Host)
				&& workers.Exists(w => w is Waiter)
				&& workers.Exists(c => c is Cook);
		}
	}
	#endregion

	#region unity_methods
	private void Start ()
	{
		workers = new List<Worker>();
		tables = spawnParent.GetComponentsInChildren<Table>();
		//FindWorkers();
		SeperateWaitersByTables();
	}

	private void Update ()
	{
		if ( Input.GetKeyDown("h") )
		{
			SaveInformation si = new SaveInformation();
			si.Save();
		}
	}
	#endregion

	#region public_methods

	/// <summary>
	/// Spawns a new instance of worker for every data in workers list; avoid using
	/// </summary>
	public void SpawnWorkers ()
	{
		foreach ( var worker in workers )
		{
			InstantateWorker(worker);
		}
	}

	/// <summary>
	/// Instantiate a worker object and all its components
	/// </summary>
	/// <param name="worker"></param>
	public void InstantateWorker ( Worker worker )
	{
		var obj = new GameObject();
		obj.AddComponent<WorkerMono>().worker = worker;
		obj.name = worker.Name;
		obj.tag = "Worker";
		workers.Add(worker);
		Debug.Log(workers.Count);
	}

	/// <summary>
	/// Fires a worker, delete all its data
	/// </summary>
	/// <param name="instance"></param>
	public void FireWorker ( WorkerMono instance )
	{
		workers.Remove(instance.worker);
		Destroy(instance.gameObject);
	}

	/// <summary>
	/// Makes the waiters be responsible for an equal amount of tables
	/// </summary>
	public void SeperateWaitersByTables ()
	{
		List<Worker> waiters = Waiters;
		if ( waiters == null || waiters.Count == 0 ) return;

		ClearWaitersTables(waiters);

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


	#endregion

	#region private_methods
	private static void ClearWaitersTables ( List<Worker> waiters )
	{
		foreach ( Waiter w in waiters )
		{
			w.ClearTablesList();
		}
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
	#endregion

}

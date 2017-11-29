using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Staff : GenericSingletonClass<Staff>
{
	public List<Worker> workers;

	private List<GameObject> workersInScene;

	private void Start ()
	{
		workers = new List<Worker>();
		
		workersInScene = new List<GameObject>();
	}

	public void SpawnWorkers()
	{
		workersInScene.Clear();
		foreach ( var worker in workers )
		{
			InstantateWorker(worker);
		}
	}

	public void InstantateWorker (Worker worker)
	{
		var obj = Instantiate(new GameObject());
		obj.AddComponent<WorkerMono>().worker = worker;
		obj.name = worker.Name;
		obj.tag = "Worker";
		workers.Add(worker);
		workersInScene.Add(obj);
	}

	internal void FireWorker ( WorkerMono instance )
	{
		workers.Remove(instance.worker);
		workersInScene.Remove(instance.gameObject);
		Destroy(instance.gameObject);
	}
}

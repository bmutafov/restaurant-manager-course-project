using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentWorkersUI : MonoBehaviour
{

	public GameObject prefab;
	public Transform container;

	public void ShowAll ()
	{
		List<WorkerMono> workers = new List<WorkerMono>();
		FindAndAddWorkers(workers);
		foreach ( var worker in workers )
		{
			var instance = Instantiate(prefab, container).transform;
			UpdateText(worker, instance);
			if ( worker.worker.GetType().ToString().Equals("Host") )
			{
				instance.Find("FireButton").gameObject.SetActive(false);
			}
			else
			{
				instance.Find("FireButton")
							.GetComponent<Button>()
							.onClick
							.AddListener(() =>
							RestaurantManager.Instance.FireWorker(worker));
			}
		}
	}

	private static void UpdateText ( WorkerMono worker, Transform instance )
	{
		UI.UpdateChildTextMeshText(instance, "Name", worker.worker.Name);
		UI.UpdateChildTextMeshText(instance, "Position", worker.worker.GetType().ToString());
		UI.UpdateChildTextMeshText(instance, "Salary", worker.worker.salaryPerHour.ToString());
		UI.UpdateChildTextMeshText(instance, "Skill", worker.worker.skill + "/10");
	}

	private static void FindAndAddWorkers ( List<WorkerMono> workers )
	{
		workers.AddRange(FindObjectsOfType<WorkerMono>());
		workers.Sort(delegate ( WorkerMono w1, WorkerMono w2 )
		{
			string worker1type = w1.worker.GetType().ToString();
			string worker2type = w2.worker.GetType().ToString();
			if ( worker1type.Equals(worker2type) ) return 0;
			else if ( worker1type.Length == worker2type.Length ) return worker1type[0] < worker2type[0] ? 1 : -1;
			else if ( worker1type.Length > worker2type.Length ) return 1;
			else return -1;
		});
	}

	public void ClearAll()
	{
		for ( int i = 0 ; i < container.childCount ; i++ )
		{
			Destroy(container.GetChild(i).gameObject);
		}
	}
}

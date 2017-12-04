using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWorkersUI : MonoBehaviour
{
	#region variables
	public GameObject prefab;
	public Transform container;
	public int offersEveryXDays = 2;
	public List<Sprite> avatars;

	private List<Worker> workerApplicants;
	private int dayPassed = 0;
	#endregion

	#region unity_methods
	private void Awake ()
	{
		workerApplicants = new List<Worker>();
		dayPassed = offersEveryXDays - 1;
		GenerateNewOffers();
		DayCycle.Instance.onDayChangedCallback += GenerateNewOffers;
	}
	#endregion

	#region public_methods
	public void ShowAll ()
	{
		ClearAll();
		foreach ( var worker in workerApplicants )
		{
			var instance = Instantiate(prefab, container).transform;
			UpdateInstanceInfo(worker, instance);
			instance.Find("HireButton").GetComponent<Button>().onClick.AddListener(
				() => RestaurantManager.Instance.InstantateWorker(worker));
		}
	}

	public void ClearAll()
	{
		for ( int i = 0 ; i < container.childCount ; i++ )
		{
			Destroy(container.GetChild(i).gameObject);
		}
	}
	#endregion

	#region private_methods
	private static void UpdateInstanceInfo ( Worker worker, Transform instance )
	{
		instance.Find("Avatar").GetChild(0).GetComponent<Image>().sprite = worker.avatar;
		UI.UpdateChildTextMeshText(instance, "Name", worker.Name.Replace(" ", "<br>"));
		UI.UpdateChildTextMeshText(instance, "Position", worker.GetType().ToString());
		UI.UpdateChildTextMeshText(instance, "Skill", worker.skill.ToString());
		UI.UpdateChildTextMeshText(instance, "Salary", worker.salaryPerHour.ToString());
	}

	private void GenerateNewOffers ()
	{
		if ( ++dayPassed == offersEveryXDays )
		{
			workerApplicants = new List<Worker>();
			for ( int i = 0 ; i < 4 ; i++ )
			{
				Worker worker = null;
				switch ( i % 2 )
				{
					case 0:
						worker = new Waiter(Generate.Name(), Generate.WorkerSkill());
						break;
					case 1:
						worker = new Cook(Generate.Name(), Generate.WorkerSkill());
						break;
				}
				worker.avatar = avatars[Random.Range(0, avatars.Count)];
				workerApplicants.Add(worker);
			}
			dayPassed = 0;
		}
	}
	#endregion
}

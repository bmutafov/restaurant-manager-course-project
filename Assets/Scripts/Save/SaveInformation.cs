using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SaveInformation
{

	#region variables
	private static readonly string filename = "allSave.json";

	[SerializeField]
	private AllCustomers allCustomers;
	[SerializeField]
	private List<Host> hosts;
	[SerializeField]
	private List<Cook> cooks;
	[SerializeField]
	private List<Waiter> waiters;
	[SerializeField]
	private List<IngredientGroup> products;
	[SerializeField]
	private List<RecipeManager.ActiveRecipe> activeRecipes;
	[SerializeField]
	private int dayNumber;
	[SerializeField]
	private float budget;
	[SerializeField]
	private int lastPersonId;
	#endregion

	#region unity_methods
	#endregion

	#region public_methods
	public void Save ()
	{
		CollectInfo();
		using ( FileStream fs = new FileStream(filename, FileMode.Create) )
		{
			using ( StreamWriter writer = new StreamWriter(fs) )
			{
				writer.Write(JsonUtility.ToJson(this, true));
			}
			fs.Close();
		}
	}

	public void Load ()
	{
		using ( StreamReader r = new StreamReader(filename) )
		{
			string json = r.ReadToEnd();
			SaveInformation loadInfo = JsonUtility.FromJson<SaveInformation>(json);

			CustomerManager.Instance.allCustomers = loadInfo.allCustomers;

			DayCycle.Instance.LoadDay(loadInfo.dayNumber);

			Budget.Instance.LoadFunds(loadInfo.budget);

			loadInfo.waiters
				.ForEach(w => RestaurantManager.Instance.InstantateWorker(w));

			loadInfo.hosts
				.ForEach(w => RestaurantManager.Instance.InstantateWorker(w));

			loadInfo.cooks
				.ForEach(w => RestaurantManager.Instance.InstantateWorker(w));

			Storage.Instance.products = loadInfo.products;

			loadInfo.activeRecipes
				.ForEach(a => RecipeManager.Instance.AddActiveRecipe(a));

			Person.LastIdUsed = lastPersonId;
		}
	}

	public void CollectInfo ()
	{
		allCustomers = CustomerManager.Instance.allCustomers;
		dayNumber = DayCycle.daysPassedSinceStart;
		budget = Budget.Instance.Funds;
		waiters = RestaurantManager.Instance.Workers.OfType<Waiter>().ToList();
		hosts = RestaurantManager.Instance.Workers.OfType<Host>().ToList();
		cooks = RestaurantManager.Instance.Workers.OfType<Cook>().ToList();
		products = Storage.Instance.products;
		activeRecipes = RecipeManager.Instance.ActiveRecipes;
		lastPersonId = Person.LastIdUsed;
	}
	#endregion

	#region private_methods

	#endregion
}

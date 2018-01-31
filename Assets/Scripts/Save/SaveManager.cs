using UnityEngine;

public class SaveManager : GenericSingletonClass<SaveManager>
{

	#region variables
	public bool loadOnStart = true;
	private SaveInformation saveInformation;
	#endregion

	#region unity_methods
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
	private void Awake ()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);
		saveInformation = new SaveInformation();
	}

	private void OnLevelWasLoaded ( int level )
	{
		if(level == 1)
		{
			DayCycle.Instance.onDayChangedCallback += saveInformation.Save;
			if ( loadOnStart )
			{
				saveInformation.Load();
			}
			else
			{
				RestaurantManager.Instance.InstantateWorker(new Host("Ivaylo Dimitrov", 9));
				Budget.Instance.AddFunds(Budget.Instance.startingMoney);
			}
		}
	}
	#endregion

	#region public_methods
	public void Save()
	{
		saveInformation.Save();
	}
	#endregion

	#region private_methods

	#endregion
}

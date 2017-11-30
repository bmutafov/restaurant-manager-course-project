using System.IO;
using UnityEngine;

static public class Save
{

    /// <summary>
    /// AutoSave Function
    /// Subscribed to onDayChangedCallback
    /// </summary>
    public static void OnDayChangeAutoSave ()
    {
        if ( !Customers() )
        {
            Debug.LogError("Could not create save for customers on auto save!");
        }
        if ( !Day() )
        {
            Debug.LogError("Could not create save for DayCycle on auto save!");
        }
        if ( !PersonID() )
        {
            Debug.LogError("Could not create save for personID on auto save!.");
        }
        if ( !AllStorage() )
        {
            Debug.LogError("Could not create save for AllStorage on auto save!.");
        }
		if ( !ActiveRecipes() )
		{
			Debug.LogError("Could not create save for ActiveRecipes on auto save!.");
		}
	}

    public static bool Customers ()
    {
        try
        {
            DoSave(fileName: FilesInfo.customers, str: JsonUtility.ToJson(CustomerManager.Instance.allCustomers));
            return true;
        }
        catch ( System.Exception )
        {
            return false;
        }
    }

    public static bool Day ()
    {
        try
        {
            PlayerPrefs.SetInt(FilesInfo.day, DayCycle.daysPassedSinceStart);
            return true;
        }
        catch ( System.Exception )
        {
            return false;
        }
    }

    public static bool PersonID ()
    {
        try
        {
            PlayerPrefs.SetInt(FilesInfo.personID, Person.LastIdUsed);
            return true;
        }
        catch ( System.Exception )
        {
            return false;
        }
    }

    public static bool AllStorage ()
    {
        try
        {
            DoSave(fileName: FilesInfo.storage, str: JsonHelper.ToJson(Storage.Instance.products.ToArray(), false));
            return true;
        }
        catch ( System.Exception )
        {
            return false;
        }
    }

	public static bool ActiveRecipes ()
	{
		try
		{
			DoSave(fileName: FilesInfo.recipes, str: JsonHelper.ToJson(RecipeManager.Instance.ActiveRecipes.ToArray(), false));
			return true;
		}
		catch ( System.Exception )
		{
			return false;
		}
	}


	private static void BudgetFunds()
	{
		PlayerPrefs.SetFloat("Budget", Budget.Instance.Funds);
	}

    private static void DoSave (string fileName, string str)
    {
        using ( FileStream fs = new FileStream(fileName + ".json", FileMode.OpenOrCreate) )
        {
            using ( StreamWriter writer = new StreamWriter(fs) )
            {
                writer.Write(str);
            }
        }
    }
}

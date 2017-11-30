using System.Collections.Generic;
using System.IO;
using UnityEngine;

static public class Load
{
    public static bool Customers ()
    {
        try
        {
            using ( StreamReader r = new StreamReader(FilesInfo.customers + ".json") )

            {
                string json = r.ReadToEnd();
                CustomerManager.Instance.allCustomers = JsonUtility.FromJson<AllCustomers>(json);
            }
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
            DayCycle.daysPassedSinceStart = PlayerPrefs.GetInt(FilesInfo.day);
            return true;
        }
        catch ( System.Exception )
        {
            return false;
        }
    }

    public static void PersonID ()
    {
        Person.LastIdUsed = PlayerPrefs.GetInt(FilesInfo.personID);
    }

    public static bool AllStorage()
    {
        try
        {
            using ( StreamReader r = new StreamReader(FilesInfo.storage + ".json") )

            {
                string json = r.ReadToEnd();
                Storage.Instance.products.AddRange(JsonHelper.FromJson<IngredientGroup>(json));
            }
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
			using ( StreamReader r = new StreamReader(FilesInfo.recipes + ".json") )

			{
				string json = r.ReadToEnd();
				RecipeManager.Instance.LoadActiveRecipes(JsonHelper.FromJson<RecipeManager.ActiveRecipe>(json));
			}
			return true;
		}
		catch ( System.Exception )
		{
			return false;
		}
	}

	public static float BudgetFunds()
	{
		if(PlayerPrefs.HasKey("Budget"))
		{
			return PlayerPrefs.GetFloat("Budget");
		}
		throw new System.Exception();
	}
}

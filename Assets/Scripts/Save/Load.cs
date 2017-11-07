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
}

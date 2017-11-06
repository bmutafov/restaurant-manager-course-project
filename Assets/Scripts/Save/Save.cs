using System.IO;
using UnityEngine;

static public class Save
{
    public static bool Customers ()
    {
        try
        {
            string str = JsonUtility.ToJson(CustomerManager.Instance.allCustomers);
            using ( FileStream fs = new FileStream(FilesInfo.customers + ".json", FileMode.OpenOrCreate) )
            {
                using ( StreamWriter writer = new StreamWriter(fs) )
                {
                    writer.Write(str);
                }
            }
            return true;
        }
        catch ( System.Exception )
        {
            return false;
        }
    }
}

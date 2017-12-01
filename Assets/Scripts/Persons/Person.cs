using UnityEngine;

[System.Serializable]
public abstract class Person
{
    private static int lastIdUsed = 0;

    private static bool loaded = false;

    #region variables
    [SerializeField] private int _id;
    [SerializeField] private string _name;

    public int Id
    {
        get
        {
            return _id;
        }
    }

	protected int SetId
	{
		set
		{
			_id = value;
		}
	}

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public static int LastIdUsed
    {
        get
        {
            return lastIdUsed;
        }
        set
        {
            if ( !loaded )
            {
                lastIdUsed = value;
                loaded = true;
            }
        }
    }
    #endregion

    public Person (string name)
    {
        _id = lastIdUsed++;
        _name = name;
    }
}

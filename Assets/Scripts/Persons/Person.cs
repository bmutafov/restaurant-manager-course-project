using UnityEngine;

[System.Serializable]
public class Person
{
    private static int lastIdUsed = 0;

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
    #endregion

    public Person (string name)
    {
        _id = lastIdUsed++;
        _name = name;
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Food/Ingridient")]
public class Ingredient : ScriptableObject
{

    new public string name;
    public float price;
    public int expireTime;

    public override string ToString ()
    {
        return "Name: " + name + " | Price: " + price + " | Base expire time: " + expireTime;
    }
}

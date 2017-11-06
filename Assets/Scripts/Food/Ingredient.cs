using UnityEngine;

[CreateAssetMenu(menuName = "Food/Ingridient")]
public class Ingredient : ScriptableObject
{

    new public string name;
    public float price;
    public int expireTime;
}

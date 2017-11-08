using UnityEngine;

[CreateAssetMenu(menuName = "Food/Ingridient")]
[System.Serializable]
public class Ingredient : ScriptableObject
{

    public string ingredientName;
    public float price;
    public int expireTime;
    public IngredientType type;

    public override string ToString ()
    {
        return "Name: " + ingredientName + " | Price: " + price + " | Base expire time: " + expireTime;
    }
}

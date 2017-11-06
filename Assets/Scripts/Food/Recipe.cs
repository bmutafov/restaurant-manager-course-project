using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Recipe")]
public class Recipe : ScriptableObject
{

    #region variables
    new public string name;
    public List<Ingredient> ingredients;
    public int preparationTime;

    private float cost = 0;

    public float Cost
    {
        get
        {
            return cost;
        }
    }
    #endregion

    public void CalculateCost ()
    {
        foreach ( var ingredient in ingredients )
        {
            cost += ingredient.price;
        }
    }
}

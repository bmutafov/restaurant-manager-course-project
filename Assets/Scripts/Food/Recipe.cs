using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Recipe")]
public class Recipe : ScriptableObject
{

    #region variables
    new public string name;
    public List<Ingredient> ingredients;
    public List<float> ingredientAmount;
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

    /// <summary>
    /// Calculates the production cost of the recipe
    /// Ingredient Quality is not included
    /// 
    /// Writes the result in variable Cost
    /// </summary>
    public void CalculateCost ()
    {
        foreach ( var ingredient in ingredients )
        {
            cost += (ingredient.price * ingredientAmount[ingredients.IndexOf(ingredient)]);
        }
    }
}

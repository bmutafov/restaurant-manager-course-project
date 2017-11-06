using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Recipe")]
public class Recipe : MonoBehaviour
{

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

    public void CalculateCost ()
    {
        foreach ( var ingredient in ingredients )
        {
            cost += ingredient.price;
        }
    }
}

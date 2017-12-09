using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Recipe")]
public class Recipe : ScriptableObject
{

	#region variables
	public string recipeName;
	public List<Ingredient> ingredients;
	[SerializeField] public List<float> ingredientAmount;
	public int preparationTime;

	private float cost = 0;
	private bool costCalculated = false;

	public float Cost
	{
		get
		{
			CalculateCost();
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
		cost = 0;
		foreach ( var ingredient in ingredients )
		{
			cost += (ingredient.price * ingredientAmount[ingredients.IndexOf(ingredient)]);
		}
	}
}

using UnityEngine;
using System.Collections.Generic;

public class RecipeManager : GenericSingletonClass<RecipeManager>
{
	public class ActiveRecipe
	{
		private Recipe recipe;
		private float price;

		public ActiveRecipe ( Recipe recipe, float price )
		{
			this.recipe = recipe;
			this.price = price;
		}

		public float Price
		{
			get
			{
				return price;
			}

			set
			{
				price = value;
			}
		}

		public Recipe Recipe
		{
			get
			{
				return recipe;
			}

			set
			{
				recipe = value;
			}
		}
	}

	public List<Recipe> allRecipes;
	private List<ActiveRecipe> activeRecipes;

	public List<ActiveRecipe> ActiveRecipes
	{
		get
		{
			return activeRecipes;
		}
	}

	private void Start ()
	{
		activeRecipes = new List<ActiveRecipe>();
		Load.ActiveRecipes();
		foreach(var rec in allRecipes)
		{
			ActiveRecipe ar = new ActiveRecipe(rec, 5);
			activeRecipes.Add(ar);
		}
	}

	/// <summary>
	/// Selects a random recipe from the list
	/// 
	/// and returns it
	/// </summary>
	/// <returns></returns>
	public Recipe GetRandomRecipe ()
	{
		int randomIndex = Random.Range(0, allRecipes.Count);
		return allRecipes[randomIndex];
	}

	#region active_recipes_methods
	public void AddActiveRecipe ( Recipe recipe, float price )
	{
		if ( activeRecipes.Exists(r => r.Recipe == recipe) ) return;

		ActiveRecipe newRecipe = new ActiveRecipe(recipe, price);
		activeRecipes.Add(newRecipe);
	}

	public void DeleteActiveRecipe ( Recipe recipe )
	{
		ActiveRecipe recipeToDelete = activeRecipes.Find(r => r.Recipe == recipe);
		if ( recipeToDelete == null ) return;
		activeRecipes.Remove(recipeToDelete);
	}

	public void UpdateRecipePrice ( Recipe recipe, float newPrice )
	{
		ActiveRecipe recipeToEdit = activeRecipes.Find(r => r.Recipe == recipe);
		recipeToEdit.Price = newPrice;
	}

	public void LoadActiveRecipes( ActiveRecipe[] list )
	{
		activeRecipes.AddRange(list);
	}
	#endregion
}

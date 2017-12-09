using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

	/// <summary>
	/// Returns a list from current active recipes
	/// </summary>
	public List<ActiveRecipe> ActiveRecipes
	{
		get
		{
			return activeRecipes;
		}
	}

	/// <summary>
	/// Returns a list from recipes which haven't been added to the active recipes list
	/// </summary>
	public List<Recipe> InactiveRecipes
	{
		get
		{
			return allRecipes.Except(activeRecipes.Select(r => r.Recipe).ToList()).ToList();
		}
	}

	protected override void Awake ()
	{
		base.Awake();
		activeRecipes = new List<ActiveRecipe>();

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
	/// <summary>
	/// Adds a recipe to the active recipes list
	/// </summary>
	/// <param name="activeRecipe"></param>
	public void AddActiveRecipe ( ActiveRecipe activeRecipe )
	{
		if ( activeRecipes.Exists(r => r.Recipe == activeRecipe.Recipe) ) return;

		activeRecipes.Add(activeRecipe);
	}

	/// <summary>
	/// Adds a recipe to the active recipes list
	/// </summary>
	/// <param name="recipe"></param>
	/// <param name="price"></param>
	public void AddActiveRecipe ( Recipe recipe, float price )
	{
		if ( activeRecipes.Exists(r => r.Recipe == recipe) ) return;

		ActiveRecipe newRecipe = new ActiveRecipe(recipe, price);
		activeRecipes.Add(newRecipe);
	}


	/// <summary>
	/// Deletes from active recipes where the recipes matches the argument
	/// </summary>
	/// <param name="recipe"></param>
	public void DeleteActiveRecipe ( Recipe recipe )
	{
		ActiveRecipe recipeToDelete = activeRecipes.Find(r => r.Recipe == recipe);
		if ( recipeToDelete == null ) return;
		activeRecipes.Remove(recipeToDelete);
	}

	/// <summary>
	/// Change the price of an existing active recipe
	/// </summary>
	/// <param name="recipe"></param>
	/// <param name="newPrice"></param>
	public void UpdateRecipePrice ( Recipe recipe, float newPrice )
	{
		ActiveRecipe recipeToEdit = activeRecipes.Find(r => r.Recipe == recipe);
		recipeToEdit.Price = newPrice;
	}

	/// <summary>
	/// Loading function
	/// </summary>
	/// <param name="list"></param>
	public void LoadActiveRecipes ( ActiveRecipe[] list )
	{
		activeRecipes.AddRange(list);
	}
	#endregion
}

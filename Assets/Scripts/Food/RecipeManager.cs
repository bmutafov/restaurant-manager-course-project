using UnityEngine;
using System.Collections.Generic;

public class RecipeManager : GenericSingletonClass<RecipeManager>
{
    public List<Recipe> allRecipes;


    /// <summary>
    /// Selects a random recipe from the list
    /// 
    /// and returns it
    /// </summary>
    /// <returns></returns>
    public Recipe GetRandomRecipe()
    {
        int randomIndex = Random.Range(0, allRecipes.Count);
        return allRecipes[randomIndex];
    }
}

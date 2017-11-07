using UnityEngine;
using System.Collections.Generic;

public class RecipeManager : GenericSingletonClass<RecipeManager>
{
    public List<Recipe> allRecipes;

    public Recipe GetRandomRecipe()
    {
        int randomIndex = Random.Range(0, allRecipes.Count);
        return allRecipes[randomIndex];
    }
}

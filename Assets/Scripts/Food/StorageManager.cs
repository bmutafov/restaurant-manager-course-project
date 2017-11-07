﻿using System.Collections.Generic;
using UnityEngine;

public class StorageManager : GenericSingletonClass<StorageManager>
{

    public List<Ingredient> allIngredients;

    private void Update ()
    {
        if ( Input.GetKeyDown("b") )
        {
            BuyIngredients("Cheese", 200, 0.65f);
            BuyIngredients("Tomato", 2, 0.36f);
        }
        if ( Input.GetKeyDown("o") )
        {
            var order = new Order(RecipeManager.Instance.GetRandomRecipe(), DayCycle.Instance.GameTime);
            Debug.Log(order.recipe.name);
            OrderStack.Instance.allOrders.Add(order);
        }
    }

    public void BuyIngredients (string name, float quantity, float quality)
    {
        try
        {
            // Find the ingredient reference
            Ingredient ingredientToBuy = allIngredients.Find(ingr => ingr.name == name);

            // Check if there's an existing group of that ingredient with the same expiery date
            System.Predicate<IngredientGroup> match = ingGr => 
                ingGr.Ingredient.name == name 
                && ingGr.ExpireTime == ingredientToBuy.expireTime 
                && ingGr.Quality == quality;
            IngredientGroup existingGroup = Storage.Instance.products.Find(match);

            // If yes add the quantity
            // Else create new ingredient group
            if ( existingGroup != null )
            {
                existingGroup.Quantity += quantity;
            }
            else
            {
                IngredientGroup ingredientGroup = new IngredientGroup(ingredientToBuy, quantity)
                {
                    Quality = quality
                };
                Storage.Instance.products.Add(ingredientGroup);
            }
        }
        catch ( System.Exception e )
        {
            Debug.Log("Error buying igredients! " + e.Message);
        }
    }
}

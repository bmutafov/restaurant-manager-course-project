using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : GenericSingletonClass<StorageManager>
{

    public List<Ingredient> allIngredients;

    private void Update()
    {
        if(Input.GetKeyDown("d"))
        {
            BuyIngredients("Onion", 500);
            Debug.Log(Storage.Instance.products[0].Ingredient.name);
        }
    }

    public void BuyIngredients(string name, float quantity)
    {
        Ingredient ingredientToBuy = allIngredients.Find(ingr => ingr.name == name);
        IngredientGroup ingredientGroup = new IngredientGroup(ingredientToBuy, quantity);
        Storage.Instance.products.Add(ingredientGroup);
    }
}

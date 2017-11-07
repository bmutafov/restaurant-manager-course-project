using System;
using System.Collections.Generic;
using UnityEngine;

public class Storage : GenericSingletonClass<Storage>
{
    public List<IngredientGroup> products;


    private void Start()
    {
        products = new List<IngredientGroup>();
        DayCycle.Instance.onDayChangedCallback += ChangeProductExpireTime;
    }

    private void ChangeProductExpireTime()
    {
        if ( products.Count == 0 )
            return;

        List<IngredientGroup> remainingProducts = new List<IngredientGroup>();
        foreach (var product in products)
        {
            if (--product.ExpireTime > 0)
            {
                remainingProducts.Add(product);
            }
        }
        products.Clear();
        products = remainingProducts;
    }
}

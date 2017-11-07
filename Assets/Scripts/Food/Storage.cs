using System.Collections.Generic;

public class Storage : GenericSingletonClass<Storage>
{
    public List<IngredientGroup> products;

    private void Start()
    {
        products = new List<IngredientGroup>();
        DayCycle.Instance.onDayChangedCallback += ChangeProductExpireTime;
    }


    /// <summary>
    /// Checks if the ingredient is available in storage
    /// </summary>
    /// <param name="ingredient"> Reference to the ingredient </param>
    /// <param name="amount"> the amount of ingredient needed </param>
    /// <returns>true if in stock ; false if not</returns>
    public bool IsInStock(Ingredient ingredient, float amount)
    {
        float availableAmount = 0;
        foreach(var ingr in products.FindAll(stock => stock.Ingredient == ingredient) )
        {
            availableAmount += ingr.Quantity;
        }
        return availableAmount >= amount; 
    }

    /// <summary>
    /// Same as IsInStock() but this reduces the available ingredients in stock
    /// </summary>
    /// <param name="ingredient"> reference to the ingredient </param>
    /// <param name="amount"> the amount to be taken </param>
    public void TakeIngredient(Ingredient ingredient, float amount)
    {
        List<IngredientGroup> ingredGroup = products.FindAll(stock => stock.Ingredient == ingredient);
        float amountTaken = 0;
        for ( int i = 0 ; amountTaken < amount ; i++ )
        {
            amountTaken = ingredGroup[i].ReduceQuantity(amount - amountTaken);
        }
    }

    /// <summary>
    /// Subscribed to onDayChanged callback
    /// 
    /// Reduces the expire time for the ingredients in store by 1
    /// for each day passed
    /// </summary>
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

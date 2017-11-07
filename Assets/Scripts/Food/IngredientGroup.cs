/// <summary>
/// Used to store ingredients with 
/// different expiery dates and qualitiyes
/// </summary>

[System.Serializable]
public class IngredientGroup
{
    #region variables
    private Ingredient ingredient;
    private float quantity;
    private int expireTime;

    public Ingredient Ingredient
    {
        get
        {
            return ingredient;
        }

        set
        {
            ingredient = value;
        }
    }

    public float Quantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }
    }

    public int ExpireTime
    {
        get
        {
            return expireTime;
        }

        set
        {
            expireTime = value;
        }
    }
    #endregion

    public IngredientGroup (Ingredient ingredient, float quantity)
    {
        this.ingredient = ingredient;
        this.quantity = quantity;
        expireTime = ingredient.expireTime;
    }

    public virtual float ReduceQuantity(float amount)
    {
        if ( amount <= Quantity )
        {
            Quantity -= amount;
        } else
        {
            amount -= Quantity;
            Quantity = 0;
        }
        return amount;
    }
}

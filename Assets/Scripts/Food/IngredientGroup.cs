/// <summary>
/// Used to store ingredients with 
/// different expiery dates and qualitiyes
/// </summary>
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

    public IngredientGroup (Ingredient ingredient, float quantity, int expireTime)
    {
        this.ingredient = ingredient;
        this.quantity = quantity;
        this.expireTime = expireTime;
    }
}

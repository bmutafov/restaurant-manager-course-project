using UnityEngine;
using System;

/// <summary>
/// Used to store ingredients with 
/// different expiery dates and qualitiyes
/// </summary>
[System.Serializable]
public class IngredientGroup
{
    #region variables
    [SerializeField] private Ingredient ingredient;
    [SerializeField] private float quantity;
    [SerializeField] private int expireTime;
    [SerializeField] private float quality = 0;

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

    public float Amount
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

    public float Quality
    {
        get
        {
            return quality;
        }

        set
        {
            quality = (float) System.Math.Round(Mathf.Clamp01(value), 2);
        }
    }
    #endregion

    public IngredientGroup (Ingredient ingredient, float quantity)
    {
        this.ingredient = ingredient;
        this.quantity = quantity;
        expireTime = ingredient.expireTime;
    }

    public float ReduceQuantity (float amount)
    {
        if ( amount <= Amount )
        {
            Amount -= amount;
        }
        else
        {
            amount -= Amount;
            Amount = 0;
        }
        return amount;
    }
}

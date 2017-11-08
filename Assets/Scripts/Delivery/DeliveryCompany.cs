using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Delivery/Company")]
public class DeliveryCompany : DeliverySource
{
    public class DailyOffer
    {
        public Ingredient ingredient;
        private float quality;
        private float amount;

        public DailyOffer (Ingredient ingredient, float quality, float amount)
        {
            this.ingredient = ingredient;
            Quality = quality;
            Amount = amount;
        }

        public float Quality
        {
            get
            {
                return quality;
            }

            set
            {
                quality = (float) System.Math.Round(value, 2);
            }
        }

        public float Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = ( float ) System.Math.Round(value, 2);
            }
        }
    }

    public DailyOffer dailyOffer;

    public override void GenerateDaily ()
    {
        // choose ingredient type
        var ingredientType = ingredientTypes[Random.Range(0, ingredientTypes.Length)];

        // find all ingredients of type
        List<Ingredient> ingredientsOfType = StorageManager.Instance.allIngredients.FindAll(ingr => ingr.type == ingredientType);
        if(ingredientsOfType.Count == 0)
        {
            dailyOffer = null;
            return;
        }

        // get specific ingredient of type
        Ingredient randomIngredient = ingredientsOfType[Random.Range(0, ingredientsOfType.Count)];

        //create daily offer
        dailyOffer = new DailyOffer
        (
            ingredient: randomIngredient,
            amount: Random.Range(dailyMinAmount, dailyMaxAmount),
            quality: averageQuality + Random.Range(-0.3f, 0.3f)
        );
    }

    public override void DisplayOffer ()
    {
        if ( dailyOffer != null )
            Debug.Log("Ingredient: " + dailyOffer.ingredient.name + " | Quality: " + dailyOffer.Quality + " | Max Amount: " + dailyOffer.Amount);
        else
            Debug.Log("No available offer for today.");
    }
}

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

		public DailyOffer ( Ingredient ingredient, float quality, float amount )
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
				quality = ( float ) System.Math.Round(value, 2);
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

	#region variables
	public DailyOffer dailyOffer;
	public List<DailyOffer> allOffers = new List<DailyOffer>();
	#endregion

	#region overrides
	public override void GenerateDaily ()
	{
		for ( int i = 0 ; i < ingredientTypes.Length ; i++ )
		{
			// choose ingredient type
			var ingredientType = ingredientTypes[i];

			// find all ingredients of type
			List<Ingredient> ingredientsOfType = StorageManager.Instance.allIngredients.FindAll(ingr => ingr.type == ingredientType);
			if ( ingredientsOfType.Count == 0 )
			{
				dailyOffer = null;
				return;
			}

			foreach ( Ingredient ingredient in ingredientsOfType )
			{
				//create daily offer
				DailyOffer offer = new DailyOffer
				(
					ingredient: ingredient,
					amount: Random.Range(dailyMinAmount, dailyMaxAmount),
					quality: averageQuality + Random.Range(-0.3f, 0.3f)
				);
				allOffers.Add(offer);
			}
		}
	}

	public override void DisplayOffer ()
	{
		if ( dailyOffer != null )
			Debug.Log("Ingredient: " + dailyOffer.ingredient.ingredientName + " | Quality: " + dailyOffer.Quality + " | Max Amount: " + dailyOffer.Amount);
		else
			Debug.Log("No available offer for today.");
	}
	#endregion
}

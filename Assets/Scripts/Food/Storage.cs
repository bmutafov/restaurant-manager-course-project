using System.Collections.Generic;
using UnityEngine;

public class Storage : GenericSingletonClass<Storage>
{
	#region variables
	[HideInInspector]
	public List<IngredientGroup> products;
	#endregion

	#region unity_methods
	private void Start ()
	{
		if ( !SaveManager.Instance.loadOnStart )
			products = new List<IngredientGroup>();
		DayCycle.Instance.onDayChangedCallback += ChangeProductExpireTime;
	}
	#endregion

	#region public_methods
	/// <summary>
	/// Checks if the ingredient is available in storage
	/// </summary>
	/// <param name="ingredient"> Reference to the ingredient </param>
	/// <param name="amount"> the amount of ingredient needed </param>
	/// <returns>true if in stock ; false if not</returns>
	public bool IsInStock ( Ingredient ingredient, float amount )
	{
		float availableAmount = 0;
		foreach ( var ingr in products.FindAll(stock => stock.Ingredient == ingredient) )
		{
			availableAmount += ingr.Amount;
		}
		return availableAmount >= amount;
	}

	/// <summary>
	/// Takes ingredients from the storage
	/// </summary>
	/// <param name="ingredient"> reference to the ingredient </param>
	/// <param name="amount"> the amount to be taken </param>
	/// <returns> average quality of the ingredients used </returns>
	public float TakeIngredient ( Ingredient ingredient, float amount )
	{
		List<IngredientGroup> ingredGroup = products.FindAll(stock => stock.Ingredient == ingredient);
		float amountTaken = 0, avarageQuality = 0;
		for ( int i = 0 ; amountTaken < amount ; i++ )
		{
			avarageQuality = ingredGroup[i].Quality / (i + 1);
			amountTaken = ingredGroup[i].ReduceQuantity(amount - amountTaken);
		}
		return avarageQuality;
	}

	/// <summary>
	/// Deletes an ingredient group
	/// </summary>
	/// <param name="group">Reference to the group</param>
	public void DeleteIngredientGroup ( IngredientGroup group )
	{
		if ( products.Contains(group) )
			products.Remove(group);
	}
	#endregion

	#region private_methods
	/// <summary>
	/// Must be subscribed to onDayChanged callback.
	/// 
	/// Reduces the expire time for the ingredients in store by 1
	/// for each day passed
	/// </summary>
	private void ChangeProductExpireTime ()
	{
		if ( products.Count == 0 )
			return;

		List<IngredientGroup> remainingProducts = new List<IngredientGroup>();
		foreach ( var product in products )
		{
			if ( --product.ExpireTime > 0 )
			{
				remainingProducts.Add(product);
			}
		}
		products.Clear();
		products = remainingProducts;
	}
	#endregion
}

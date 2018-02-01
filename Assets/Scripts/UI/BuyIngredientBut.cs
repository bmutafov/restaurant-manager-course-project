using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyIngredientBut : MonoBehaviour
{

	private DeliveryCompany.DailyOffer offer;
	private Button button;

	private void Start ()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(BuyIngredient);

		offer = transform.GetComponentInParent<OfferUI>().Offer;
	}


	/// <summary>
	/// Called upon button press on the UI
	/// </summary>
	private void BuyIngredient ()
	{
		// check for empty amount
		string amountString = transform.parent.GetChild(3).GetComponent<TMP_InputField>().text;
		if ( amountString == string.Empty )
		{
			UI.Instance.OpenErrorScreen("Please specify the amount of " + offer.ingredient.ingredientName + " you want to buy!");
			return;
		}

		float amount = float.Parse(amountString);

		// check for amount greater than available
		if ( amount > offer.Amount )
		{
			UI.Instance.OpenErrorScreen("The amount you want to purchase (" + amount + ") is more than the delivery company can offer (" + offer.Amount + ")!");
		}

		// check if there's enough money to complete transaction
		else if ( amount * offer.Price > Budget.Instance.Funds )
		{
			UI.Instance.OpenErrorScreen("You don't have enough money for this purchase!");
		}

		// success
		else
		{
			UI.Instance.OpenSuccessScreen("You have succesfully bought " + amount + " " + offer.ingredient.ingredientName + "!");
			FindObjectOfType<OpenShopBut>().offersUI.SetActive(false);
			StorageManager.Instance.BuyIngredients(offer.ingredient.name, amount, offer.Quality);
			offer.Amount -= amount;
			Budget.Instance.WithdrawFunds(amount * offer.Price);
		}

	}
}

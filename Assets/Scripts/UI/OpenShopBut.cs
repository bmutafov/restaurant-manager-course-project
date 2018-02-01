using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenShopBut : MonoBehaviour
{
	#region variables
	[Header("UI Panels")]
	public GameObject shopUI;
	public GameObject offersUI;

	[Header("List containers")]
	public Transform shopContainer;
	public Transform offersContainer;

	[Header("Prefabs")]
	public GameObject companyInListPrefab;
	public GameObject offerPrefab;

	private Button button;
	#endregion

	#region unity_methods
	private void Start ()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(
			() => OpenShop());
	}
	#endregion

	private void OpenShop ()
	{
		shopUI.SetActive(!shopUI.activeSelf);
		ClearList();
		PopulateCompanies();
	}

	private void PopulateCompanies ()
	{
		foreach ( DeliveryCompany company in ActiveDeliverySources.Instance.deliverySources )
		{
			List<DeliveryCompany.DailyOffer> offer = company.allOffers;
			InstantiateCompany(company.name, offer);
		}
	}

	public void ClearList ()
	{
		for ( int i = 0 ; i < shopContainer.childCount ; i++ )
		{
			Destroy(shopContainer.GetChild(i).gameObject);
		}
	}


	/// <summary>
	/// Instantiates a prefab in the shop with info for the company
	/// </summary>
	private void InstantiateCompany ( string name, List<DeliveryCompany.DailyOffer> offer )
	{
		// check if offer is available and instantiate
		if ( offer == null ) return;
		GameObject company = Instantiate(companyInListPrefab, shopContainer);

		// set gameobject name
		company.name = name;

		// update texts on the UI
		// -> name
		UI.ChildText(company.transform, 0, name);
		// -> ingredient types
		UI.ChildText(company.transform, 1, IngredientTypesForCompany(offer));
		// -> add event listener to the button
		company.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => InstantiateOffer(name, offer));
	}


	/// <summary>
	/// Returns a string for the avaiable ingredient types (abbreviations) which the company provide
	/// </summary>
	private static string IngredientTypesForCompany ( List<DeliveryCompany.DailyOffer> offer )
	{
		string typesString = string.Empty;
		bool first = true;
		foreach ( var inOffer in offer )
		{
			string ingrAbbr = inOffer.ingredient.type.ToString().Substring(0, 3);
			if ( !typesString.Contains(ingrAbbr) )
			{
				typesString += (!first ? "/" : string.Empty) + "<b>" + ingrAbbr + "</b>";
				first = false;
			}
		}

		return typesString;
	}



	/// <summary>
	/// Spawns an offer for a company
	/// </summary>
	private void InstantiateOffer ( string name, List<DeliveryCompany.DailyOffer> allOffers )
	{
		// destroys previous offers spawned
		for ( int i = 0 ; i < offersContainer.childCount ; i++ )
		{
			Destroy(offersContainer.GetChild(i).gameObject);
		}

		// -> set the window to active
		offersUI.SetActive(true);

		// spawns the new ones
		foreach ( var offer in allOffers )
		{
			// -> spawn and attach offer to carry info
			GameObject ingredient = Instantiate(offerPrefab, offersContainer);
			ingredient.GetComponent<OfferUI>().Offer = offer;

			// -> update child TMPro components with the respective info
			UI.ChildText(ingredient.transform, 0, offer.ingredient.name);
			UI.ChildText(ingredient.transform, 1, offer.Price.ToString() + "$");
			UI.ChildText(ingredient.transform, 2, offer.Quality.ToString());
			ingredient.transform.Find("MaxAmount").GetComponent<TextMeshProUGUI>().text = offer.Amount.ToString();
		}
	}
}

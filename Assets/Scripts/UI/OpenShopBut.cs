using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenShopBut : MonoBehaviour {
	[Header("UI Panels")]
	public GameObject shopUI;
	public GameObject companyUI;
	[Header("List containers")]
	public Transform container;
	public Transform companyContainer;
	[Header("Prefabs")]
	public GameObject companyInListPrefab;
	public GameObject offerPrefab;

	private Button button; 

	private void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(
			() => OpenShop());
	}

	private void OpenShop ()
	{
		shopUI.SetActive(!shopUI.activeSelf);
		ClearList();
		PopulateCompanies();
	}

	private void PopulateCompanies ()
	{
		foreach(DeliveryCompany company in ActiveDeliverySources.Instance.deliverySources)
		{
			DeliveryCompany.DailyOffer offer = company.dailyOffer;
			SpawnCompanyInfoPrefab(company.name, offer);
		}
	}

	public void ClearList ()
	{
		for ( int i = 0 ; i < container.childCount ; i++ )
		{
			Destroy(container.GetChild(i).gameObject);
		}
	}

	private void SpawnCompanyInfoPrefab ( string name, DeliveryCompany.DailyOffer offer )
	{
		if ( offer == null ) return;
		GameObject company = Instantiate(companyInListPrefab, container);
		company.name = name;
		UI.UpdateChildTextMeshText(company.transform, 0, name);
		UI.UpdateChildTextMeshText(company.transform, 1, offer.ingredient.type.ToString());
		company.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => CompanyOfferInfo(name, offer));
	}

	private void CompanyOfferInfo ( string name, DeliveryCompany.DailyOffer offer )
	{
		for ( int i = 0 ; i < companyContainer.childCount ; i++ )
		{
			Destroy(companyContainer.GetChild(0).gameObject);
		}
		companyUI.SetActive(true);
		GameObject ingredient = Instantiate(offerPrefab, companyContainer);
		ingredient.GetComponent<OfferUI>().Offer = offer;
		UI.UpdateChildTextMeshText(ingredient.transform, 0, offer.ingredient.name);
		UI.UpdateChildTextMeshText(ingredient.transform, 1, offer.ingredient.price.ToString() + "$");
		UI.UpdateChildTextMeshText(ingredient.transform, 2, offer.Quality.ToString());
		ingredient.transform.Find("MaxAmount").GetComponent<TextMeshProUGUI>().text = "Max: " + offer.Amount.ToString();
	}
}

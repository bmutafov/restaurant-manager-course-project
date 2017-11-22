using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferUI : MonoBehaviour {

	private DeliveryCompany.DailyOffer offer;

	public DeliveryCompany.DailyOffer Offer
	{
		get
		{
			return offer;
		}

		set
		{
			offer = value;
		}
	}
}

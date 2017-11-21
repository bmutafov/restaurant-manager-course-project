using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatePrice : MonoBehaviour
{

	private float price;
	private TextMeshProUGUI text;

	private void Start ()
	{
		price = float.Parse(transform.parent.Find("Price").GetComponent<TextMeshProUGUI>().text.Replace(@"$", string.Empty));
		text = transform.GetComponentInChildren<TextMeshProUGUI>();
	}

	public void CalculatePriceDynamic ( string amount )
	{
		try
		{
			text.text = (float.Parse(amount) * price).ToString() + "$";
		}
		catch ( System.Exception e )
		{
			text.text = "#ERR";
		}
	}
}

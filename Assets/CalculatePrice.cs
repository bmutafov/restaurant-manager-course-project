using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatePrice : MonoBehaviour
{

	private float price;
	private TextMeshProUGUI text;
	private TMP_InputField amountField;

	private void Start ()
	{
		amountField = transform.parent.Find("Amount").GetComponent<TMP_InputField>();
		price = float.Parse(transform.parent.Find("Price").GetComponent<TextMeshProUGUI>().text.Replace(@"$", string.Empty));
		text = transform.GetComponentInChildren<TextMeshProUGUI>();
	}

	public void CalculatePriceDynamic ( string amount )
	{
		try
		{
			text.text = System.Math.Round(float.Parse(amount) * price, 2).ToString() + "$";
		}
		catch ( System.Exception e )
		{
			text.text = "0$";
		}
	}

	public void DetectMaxAmount ( string text )
	{
		if ( text == "" ) text = "0";
		float amount = float.Parse(text);
		int charsDecimals = text.Contains(".") ? text.Substring(text.IndexOf(".")).Length - 1 : 0;
		if ( amount < 0 )
		{
			amountField.text = "0";
		}
		else if ( charsDecimals > 2 )
		{
			amountField.text = text.Substring(0, text.Length - charsDecimals + 2);
		}
	}
}

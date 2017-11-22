using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BudgetUI : MonoBehaviour
{
	public TextMeshProUGUI textComponent;
	// Use this for initialization
	private void Start ()
	{
		textComponent = GetComponent<TextMeshProUGUI>();
		Budget.Instance.onBudgetChangedCallback += UpdateText;
	}

	private void UpdateText ()
	{
		textComponent.text = Budget.Instance.Funds + "$";
	}
}

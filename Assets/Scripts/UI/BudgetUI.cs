using TMPro;
using UnityEngine;

public class BudgetUI : MonoBehaviour
{
	private TextMeshProUGUI textComponent;

	private void Start ()
	{
		textComponent = GetComponent<TextMeshProUGUI>();
		Budget.Instance.onBudgetChangedCallback += UpdateText;
		UpdateText();
	}

	private void UpdateText ()
	{
		textComponent.text = Budget.Instance.Funds + "$";
	}
}

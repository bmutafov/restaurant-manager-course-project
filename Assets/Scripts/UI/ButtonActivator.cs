using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
	private Button button;

	private void Start ()
	{
		button = GetComponent<Button>();
		DayCycle.Instance.onDayChangedCallback += Activate;
		DayCycle.Instance.onDayStartedCallback += Deactivate;
	}

	private void Activate ()
	{
		button.interactable = true;
	}

	private void Deactivate ()
	{
		button.interactable = false;
	}
}

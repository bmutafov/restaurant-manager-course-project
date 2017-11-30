using TMPro;
using UnityEngine;

public class DayUIUpdater : MonoBehaviour
{
	[SerializeField]
	private GameObject dateText;

	private TextMeshProUGUI TMPtext;
	private TextMeshProUGUI dateTMP;

	private void Start ()
	{
		TMPtext = GetComponent<TextMeshProUGUI>();
		dateTMP = dateText.GetComponent<TextMeshProUGUI>();
		DayCycle.Instance.onMinuteChangedCallback += UpdateUIText;
	}

	private void UpdateUIText ()
	{
		TMPtext.text = DayCycle.Instance.GameTime.ToString("HH:mm");
		dateTMP.text = DayCycle.Instance.GameTime.ToShortDateString();
	}
}

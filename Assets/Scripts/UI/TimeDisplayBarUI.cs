using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayBarUI : MonoBehaviour
{

	public bool isActive = true;

	private Slider slider;

	private float CurrentTime
	{
		get
		{
			return DayCycle.Instance.GameTime.Hour + (DayCycle.Instance.GameTime.Minute / 60f);
		}
	}

	private void Start ()
	{
		slider = GetComponent<Slider>();
		if ( isActive )
			DayCycle.Instance.onMinuteChangedCallback += UpdateFill;
	}

	private void UpdateFill ()
	{
		float value = (CurrentTime - DayCycle.Instance.openingHour) / DayCycle.Instance.WorkingHours;
		slider.value = value;
	}
}

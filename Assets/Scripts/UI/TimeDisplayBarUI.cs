using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayBarUI : MonoBehaviour
{
	#region variables
	public bool isActive = true;

	private Slider slider;
	#endregion

	#region unity_methods
	private void Start ()
	{
		slider = GetComponent<Slider>();
		if ( isActive )
			DayCycle.Instance.onMinuteChangedCallback += UpdateFill;
	}
	#endregion

	#region private_methods
	private float CurrentTime
	{
		get
		{
			return DayCycle.Instance.GameTime.Hour + (DayCycle.Instance.GameTime.Minute / 60f);
		}
	}

	private void UpdateFill ()
	{
		float value = (CurrentTime - DayCycle.Instance.openingHour) / DayCycle.Instance.WorkingHours;
		slider.value = value;
	}
	#endregion
}

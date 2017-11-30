using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

	#region variables
	public bool loadOnStart = true;
	public SaveInformation saveInformation;
	#endregion

	#region unity_methods
	private void Awake ()
	{
		saveInformation = new SaveInformation();
		DayCycle.Instance.onDayChangedCallback += saveInformation.Save;
	}

	private void Start ()
	{
		if ( loadOnStart ) saveInformation.Load();
		DayCycle.Instance.onDayChangedCallback.Invoke();
	}
	#endregion

	#region public_methods

	#endregion

	#region private_methods

	#endregion
}

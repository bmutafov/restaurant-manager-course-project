using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{

	#region variables
	private GameObject outlineEffect;
	public ChangePrefab.Type type;
	#endregion

	#region unity_methods
	private void Start ()
	{
		outlineEffect = transform.GetChild(0).gameObject;
		outlineEffect.SetActive(false);
	}

	private void OnMouseEnter ()
	{
		outlineEffect.SetActive(true);
		Debug.Log("Enter");
	}

	private void OnMouseExit ()
	{
		outlineEffect.SetActive(false);
		Debug.Log("Exit");
	}

	private void OnMouseOver ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			OpenEditMenu();
		}
	}
	#endregion

	#region public_methods

	#endregion

	#region private_methods
	private void OpenEditMenu()
	{
		ChangePrefab.Instance.EditObject(gameObject);
		ChangePrefab.Instance.Change(type);
	}
	#endregion
}

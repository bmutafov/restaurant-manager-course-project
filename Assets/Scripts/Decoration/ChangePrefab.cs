using System.Collections.Generic;
using UnityEngine;

public class ChangePrefab : MonoBehaviour
{
	public enum Type
	{
		Floor,
		Wall
	}

	#region variables
	private Type currentType = Type.Floor;
	private int currentIndex = 0;
	private GameObject currentSpawned;
	private GameObject editObject;
	private bool isEditMode = false;

	public GameObject editMenu;
	public Transform decorationsParent;
	public List<GameObject> floorPrefabs;
	public List<GameObject> wallPrefabs;
	public bool IsEditMode
	{
		get
		{
			return isEditMode;
		}
	}
	#endregion

	#region unity_methods
	private void Start ()
	{
		currentSpawned = transform.GetChild(0).GetChild(0).gameObject;
	}
	#endregion

	#region public_methods
	public void Change ( Type newType )
	{
		if ( currentType == newType ) return;
		if ( newType == Type.Floor ) transform.GetChild(0).eulerAngles = Vector3.zero;
		else transform.GetChild(0).eulerAngles = new Vector3(0, 90, 0);
		currentType = newType;
		ChangeModel(true);
	}

	public void ChangeModel ( bool asc )
	{
		Destroy(currentSpawned);
		List<GameObject> objectPool = GetObjectPool();
		Validation(asc, objectPool.Count);
		currentSpawned = Instantiate(objectPool[currentIndex], transform.GetChild(0));
		currentSpawned.transform.localPosition = Vector3.zero;
	}

	private List<GameObject> GetObjectPool ()
	{
		List<GameObject> objectPool = new List<GameObject>();
		switch ( currentType )
		{
			case Type.Floor: objectPool = floorPrefabs; break;
			case Type.Wall: objectPool = wallPrefabs; break;
		}

		return objectPool;
	}

	public void EditObject ( GameObject obj )
	{
		if ( editMenu.activeSelf ) return;
		editObject = obj;
		editMenu.SetActive(true);
	}

	public void ChooseNewModel ()
	{
		List<GameObject> objectPool = GetObjectPool();
		var newObject = Instantiate(objectPool[currentIndex], editObject.transform.position, Quaternion.identity, decorationsParent);
		editObject.transform.GetChild(0).SetParent(newObject.transform);
		Destroy(editObject);
	}

	public void SetEditMode(bool value)
	{
		isEditMode = value;
	}
	#endregion

	#region private_methods
	private void Validation ( bool asc, int objectPoolCount )
	{
		if ( asc )
		{
			currentIndex++;
		}
		else
		{
			currentIndex--;
		}
		if ( currentIndex >= objectPoolCount )
		{
			currentIndex = 0;
		}
		if ( currentIndex < 0 )
		{
			currentIndex = objectPoolCount - 1;
		}
	}

	#endregion
}

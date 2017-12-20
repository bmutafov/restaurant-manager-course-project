using cakeslice;
using UnityEngine;
using UnityEngine.EventSystems;

public class Decoration : MonoBehaviour
{

	#region variables
	private ChangePrefab changePrefab;
	private GameObject outlineEffect;
	public ChangePrefab.Type type;
	#endregion

	#region unity_methods
	private void Start ()
	{
		SpawnHighlightCube();
		changePrefab = FindObjectOfType<ChangePrefab>();
		if ( changePrefab == null ) Destroy(this);
	}

	private void OnMouseEnter ()
	{
		if ( changePrefab.IsEditMode && !EventSystem.current.IsPointerOverGameObject() )
		{
			outlineEffect.SetActive(true);
		}
	}

	private void OnMouseExit ()
	{
		if ( outlineEffect.activeSelf )
		{
			outlineEffect.SetActive(false);
		}
	}

	private void OnMouseOver ()
	{
		if ( Input.GetMouseButtonDown(0) )
		{
			OpenEditMenu();
		}
	}
	#endregion

	#region public_methods

	#endregion

	#region private_methods
	private void OpenEditMenu ()
	{
		if ( !changePrefab.IsEditMode || EventSystem.current.IsPointerOverGameObject() ) return;
		changePrefab.EditObject(gameObject);
		changePrefab.Change(type);
	}

	private void SpawnHighlightCube ()
	{
		outlineEffect = GameObject.CreatePrimitive(PrimitiveType.Cube);
		outlineEffect.transform.parent = transform;
		outlineEffect.transform.localScale = GetComponent<Renderer>().bounds.size;
		outlineEffect.transform.position = GetComponent<Renderer>().bounds.center;
		outlineEffect.AddComponent<OutlineRender>().color = 3;
		Destroy(outlineEffect.GetComponent<BoxCollider>());
		outlineEffect.GetComponent<MeshRenderer>().enabled = false;
		outlineEffect.SetActive(false);
	}
	#endregion
}

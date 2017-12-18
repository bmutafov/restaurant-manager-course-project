using cakeslice;
using UnityEngine;
using UnityEngine.EventSystems;

public class Decoration : MonoBehaviour
{

	#region variables
	private GameObject outlineEffect;
	public ChangePrefab.Type type;
	#endregion

	#region unity_methods
	private void Start ()
	{
		SpawnHighlightCube();
	}

	private void OnMouseEnter ()
	{
		if ( ChangePrefab.Instance.IsEditMode && !EventSystem.current.IsPointerOverGameObject() )
			outlineEffect.SetActive(true);
	}

	private void OnMouseExit ()
	{
		outlineEffect.SetActive(false);
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
		if ( !ChangePrefab.Instance.IsEditMode || EventSystem.current.IsPointerOverGameObject() ) return;
		ChangePrefab.Instance.EditObject(gameObject);
		ChangePrefab.Instance.Change(type);
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

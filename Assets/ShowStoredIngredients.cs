using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStoredIngredients : MonoBehaviour
{
	public GameObject ingredientPrefab;
	public Transform container;

	public void ShowAll ()
	{
		foreach ( var ingrGroup in Storage.Instance.products )
		{
			Transform instance = Instantiate(ingredientPrefab, container).transform;
			UI.UpdateChildTextMeshText(instance, "Name", ingrGroup.Ingredient.ingredientName);
			UI.UpdateChildTextMeshText(instance, "Amount", ingrGroup.Amount.ToString());
			UI.UpdateChildTextMeshText(instance, "ExpiresIn", ingrGroup.ExpireTime.ToString());
			instance
				.Find("DeleteButton")
				.GetComponent<Button>()
				.onClick
				.AddListener(() =>
				Storage.Instance.DeleteIngredientGroup(ingrGroup));
		}
	}

	public void ClearAll()
	{
		for ( int i = 0 ; i < container.childCount ; i++ )
		{
			Destroy(container.GetChild(i).gameObject);
		}
	}
}

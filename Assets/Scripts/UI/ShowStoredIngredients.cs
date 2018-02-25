using UnityEngine;
using UnityEngine.UI;

public class ShowStoredIngredients : MonoBehaviour
{
	public GameObject ingredientPrefab;
	public Transform container;
	public GameObject noContentText;

	public void ShowAll ()
	{
		ClearAll();
		noContentText.SetActive(Storage.Instance.products.Count == 0);
		foreach ( var ingrGroup in Storage.Instance.products )
		{
			Transform instance = Instantiate(ingredientPrefab, container).transform;
			UpdateTexts(ingrGroup, instance);
			instance
				.Find("DeleteButton")
				.GetComponent<Button>()
				.onClick
				.AddListener(() =>
				Storage.Instance.DeleteIngredientGroup(ingrGroup));
		}
	}

	private static void UpdateTexts ( IngredientGroup ingrGroup, Transform instance )
	{
		UI.ChildText(instance, "Name", ingrGroup.Ingredient.ingredientName);
		UI.ChildText(instance, "Amount", ingrGroup.Amount.ToString());
		UI.ChildText(instance, "ExpiresIn", ingrGroup.ExpireTime.ToString() + (ingrGroup.ExpireTime > 1 ? " days" : " day"));
		UI.ChildText(instance, "Quality", ingrGroup.Quality.ToString());
	}

	public void ClearAll()
	{
		for ( int i = 0 ; i < container.childCount ; i++ )
		{
			Destroy(container.GetChild(i).gameObject);
		}
	}
}

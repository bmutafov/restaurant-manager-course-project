using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class RecipesUI : MonoBehaviour
{

	#region variables
	public Button rightButton;
	public Button leftButton;
	public RectTransform container;
	public GameObject recipePrefab;
	public int slideSpeed = 3;

	private int currentFirstShown = 0;
	private Vector3 nextPosition;
	private bool isMoving = false;
	#endregion

	#region unity_methods
	private void Start ()
	{
		SpawnRecipes(active: false);
		rightButton.onClick.AddListener(() => MoveRight());
		leftButton.onClick.AddListener(() => MoveLeft());
	}

	private void OnEnable ()
	{
		StartCoroutine(ButtonActiveManager(new WaitForSeconds(1f)));
	}

	private void Update ()
	{
		if ( isMoving ) AnimateToPosition();
	}
	#endregion

	#region public_methods
	public void ChangeShownRecipesToActive ( bool active )
	{
		SpawnRecipes(active);
	}

	public void ClearRecipesOnScreen ()
	{
		for ( int i = 0 ; i < container.childCount ; i++ )
		{
			Destroy(container.transform.GetChild(i).gameObject);
		}
	}
	#endregion

	#region private_methods
	private void AnimateToPosition ()
	{
		if ( Vector3.Distance(container.position, nextPosition) < 1 )
		{
			container.position = nextPosition;
			isMoving = false;
		}
		container.position = Vector3.Lerp(container.position, nextPosition, Time.deltaTime * slideSpeed);
	}

	private void MoveRight ()
	{
		if ( currentFirstShown + 3 > container.childCount || isMoving ) return;
		nextPosition = (container.position - new Vector3(container.sizeDelta.x, 0f, 0));
		isMoving = true;
		currentFirstShown += 2;
	}

	private void MoveLeft ()
	{
		if ( currentFirstShown == 0 || isMoving ) return;
		nextPosition = (container.position + new Vector3(container.sizeDelta.x, 0f, 0));
		isMoving = true;
		currentFirstShown -= 2;
	}

	private System.Collections.IEnumerator ButtonActiveManager ( WaitForSeconds wait )
	{
		while ( gameObject.activeSelf )
		{
			rightButton.interactable = (currentFirstShown + 2 < container.childCount);
			leftButton.interactable = currentFirstShown != 0;

			yield return wait;
		}

		yield return null;
	}

	private void SpawnRecipes ( bool active )
	{
		ClearRecipesOnScreen();
		List<Recipe> recipes = active ? RecipeManager.Instance.ActiveRecipes.Select(r => r.Recipe).ToList() : RecipeManager.Instance.InactiveRecipes;
		foreach ( Recipe recipe in recipes )
		{
			var obj = Instantiate (recipePrefab, container).transform;
			UI.ChildText(obj, "Name", recipe.recipeName);
			UI.ChildText(obj, "EstimatedCost", "Estimated cost: <b>" + recipe.Cost + "$</b>");
			BulletPoint.BulletPoint list = obj.Find("List").GetComponent<BulletPoint.BulletPoint> ();
			for ( int i = 0 ; i < recipe.ingredients.Count ; i++ )
			{
				list.AddBulletPoint(recipe.ingredients[i].ingredientName + " <i><color=#FFB8B2>x" + recipe.ingredientAmount[i] + "</color></i>");
			}
			Button button = obj.Find("Button").GetComponent<Button>();
			if ( active )
			{
				obj.Find("Price").gameObject.SetActive(false);
				UI.ChildText(button.transform, 0, "Remove");
				button.onClick.AddListener(() =>
				{
					RecipeManager.Instance.DeleteActiveRecipe(recipe);
					Destroy(obj.gameObject);
				});
			}
			else
			{
				button.onClick
					.AddListener(() =>
					{
						float price = 0;
						try
						{
							price = float.Parse(obj.Find("Price").GetComponent<TMPro.TMP_InputField>().text);
						}
						catch ( System.Exception )
						{
							UI.Instance.OpenErrorScreen("The entered price is invalid");
							return;
						}
						RecipeManager.ActiveRecipe addRecipe = new RecipeManager.ActiveRecipe(recipe, price);
						RecipeManager.Instance.AddActiveRecipe(addRecipe);
						Destroy(obj.gameObject);
					});
			}
		}
	}
	#endregion
}
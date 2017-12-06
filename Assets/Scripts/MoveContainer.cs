using System.Collections;
using System.Collections.Generic;
using BulletPoint;
using UnityEngine;
using UnityEngine.UI;
public class MoveContainer : MonoBehaviour
{

	#region variables
	public Button rightButton;
	public Button leftButotn;
	public RectTransform container;
	public GameObject recipePrefab;

	private int currentFirstShown = 0;
	private Vector3 nextPosition;
	private bool isMoving = false;
	#endregion

	#region unity_methods
	private void Start ()
	{
		SpawnRecipes();
		rightButton.onClick.AddListener(() => MoveRight());
		leftButotn.onClick.AddListener(() => MoveLeft());
	}

	private void Update ()
	{
		if(isMoving) AnimateToPosition();
	}
	#endregion

	#region public_methods

	#endregion

	#region private_methods
	private void AnimateToPosition ()
	{
		if(Vector3.Distance(container.position, nextPosition) < 1)
		{
			container.position = nextPosition;
			isMoving = false;
		}
		container.position = Vector3.Lerp(container.position, nextPosition, Time.deltaTime * 3);
	}

	private void MoveRight ()
	{
		if ( currentFirstShown + 2 > container.childCount || isMoving) return;
		nextPosition = (container.position - new Vector3(container.sizeDelta.x, 0f, 0));
		isMoving = true;
		currentFirstShown += 2;
	}

	private void MoveLeft ()
	{
		if ( currentFirstShown == 0 || isMoving) return;
		nextPosition = (container.position + new Vector3(container.sizeDelta.x, 0f, 0));
		isMoving = true;
		currentFirstShown -= 2;
	}

	private void SpawnRecipes ()
	{
		foreach ( Recipe recipe in RecipeManager.Instance.InactiveRecipes )
		{
			var obj = Instantiate (recipePrefab, container).transform;
			UI.ChildText(obj, "Name", recipe.recipeName);
			BulletPoint.BulletPoint list = obj.Find("List").GetComponent<BulletPoint.BulletPoint> ();
			foreach ( Ingredient ingredient in recipe.ingredients )
			{
				list.AddBulletPoint(ingredient.ingredientName);
			}
			obj.Find("Button").GetComponent<Button>()
				.onClick
				.AddListener(() => 
				RecipeManager.Instance.AddActiveRecipe
				(new RecipeManager.ActiveRecipe(recipe, recipe.Cost)));
		}
	}
	#endregion
}
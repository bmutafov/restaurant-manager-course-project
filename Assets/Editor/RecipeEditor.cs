using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
		Recipe myScript = ( Recipe ) target;

		if ( GUILayout.Button("Set name") )
		{
			myScript.recipeName = myScript.name;
		}
		if ( GUILayout.Button("Match ingredients lists count") )
		{
			while(myScript.ingredientAmount.Count < myScript.ingredients.Count)
			{
				myScript.ingredientAmount.Add(1);
			}
			while( myScript.ingredientAmount.Count > myScript.ingredients.Count )
			{
				myScript.ingredientAmount.RemoveAt(myScript.ingredientAmount.Count - 1);
			}
		}
	}

}

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ingredient))]
[CanEditMultipleObjects]
public class IngredientEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		List<Ingredient> scripts = new List<Ingredient>();
		scripts.AddRange(targets.Cast<Ingredient>());

		EditorGUILayout.BeginHorizontal();
			SetNames(scripts);
		EditorGUILayout.EndHorizontal();
	}

	private void SetNames ( List<Ingredient> scripts )
	{
		if ( GUILayout.Button("Set name") )
		{
			foreach ( var myScript in scripts )
			{
				myScript.ingredientName = myScript.name;
				EditorUtility.SetDirty(myScript);
			}
		}
	}
}

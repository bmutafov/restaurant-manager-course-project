using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RecipeManager))]
public class RecipeManagerEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

		EditorGUILayout.BeginHorizontal();
			RecipeManager myScript = ( RecipeManager ) target;
			if ( GUILayout.Button("Clear list") )
			{
				myScript.allRecipes.Clear();
			}
			if ( GUILayout.Button("Find All Objects") )
			{
				myScript.allRecipes.Clear();
				myScript.allRecipes.AddRange(Instances.GetAllInstances<Recipe>());
			}
		EditorGUILayout.EndHorizontal();
	}
}

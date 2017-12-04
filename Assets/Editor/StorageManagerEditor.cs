using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StorageManager))]
public class StorageManagerEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

		EditorGUILayout.BeginHorizontal();
			StorageManager myScript = ( StorageManager ) target;
			if ( GUILayout.Button("Clear list") )
			{
				myScript.allIngredients.Clear();
			}
			if ( GUILayout.Button("Find All Objects") )
			{
				myScript.allIngredients.Clear();
				myScript.allIngredients.AddRange(Instances.GetAllInstances<Ingredient>());
			}
		EditorGUILayout.EndHorizontal();
	}
}
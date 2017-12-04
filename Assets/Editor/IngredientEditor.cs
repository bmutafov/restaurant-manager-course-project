using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ingredient))]
public class IngredientEditor : Editor
{

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        Ingredient myScript = ( Ingredient ) target;
		EditorGUILayout.BeginHorizontal();
        if ( GUILayout.Button("Set name") )
        {
            myScript.ingredientName = myScript.name;
			EditorUtility.SetDirty(this);
        }
		EditorGUILayout.EndHorizontal();
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{

    public override void OnInspectorGUI ()
    {
        Recipe myScript = ( Recipe ) target;

        serializedObject.Update();

        EditorGUILayout.Space();

        SerializedProperty tProp = serializedObject.GetIterator();
        if ( tProp.NextVisible(true) )
        {
            do
            {
                // Just did this to mimic the normal inspector
                EditorGUI.BeginDisabledGroup(tProp.name == "Recipe");

                // If we hit our field, do our special draw
                if ( tProp.name == "ingredientAmount" )
                {
                    ShowArrayProperty(tProp.Copy());
                    EditorGUILayout.Space();
                }

                // Otherwise normal draw (include the children)
                else
                {
                    EditorGUILayout.PropertyField(tProp, true);
                }

                EditorGUI.EndDisabledGroup();
            }
            // Skip the children (the draw will handle it)
            while ( tProp.NextVisible(false) );
        }

        EditorGUILayout.Space();

        if ( GUILayout.Button("Set name") )
        {
            myScript.recipeName = myScript.name;
        }

        if ( myScript.ingredientAmount.Count != myScript.ingredients.Count )
        {
            while(myScript.ingredientAmount.Count > myScript.ingredients.Count)
            {
                myScript.ingredientAmount.RemoveAt(myScript.ingredientAmount.Count - 1);
            }
            List<float> amountOfOne = new List<float>();
            for ( int i = myScript.ingredientAmount.Count ; i < myScript.ingredients.Count ; i++ )
            {
                amountOfOne.Add(1);
            }
            myScript.ingredientAmount.AddRange(amountOfOne);
        }

        serializedObject.ApplyModifiedProperties();
    }

    public void ShowArrayProperty (SerializedProperty list)
    {
        try
        {
            EditorGUILayout.PropertyField(list);

            EditorGUI.indentLevel += 1;
            for ( int i = 0 ; i < list.arraySize ; i++ )
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i),
                new GUIContent((( Recipe ) target).ingredients[i].ingredientName));
            }
            EditorGUI.indentLevel -= 1;
        }
        catch ( System.Exception e )
        {

        }
    }
}

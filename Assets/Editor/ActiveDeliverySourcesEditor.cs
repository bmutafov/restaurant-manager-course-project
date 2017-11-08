using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveDeliverySources))]
public class ActiveDeliverySourcesEditor : Editor
{

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        ActiveDeliverySources myScript = ( ActiveDeliverySources ) target;
        if ( GUILayout.Button("Clear list") )
        {
            myScript.deliverySources.Clear();
        }
        if ( GUILayout.Button("Find All Objects") )
        {
            myScript.deliverySources.Clear();
            myScript.deliverySources.AddRange(Instances.GetAllInstances<DeliverySource>());
        }
    }
}

using UnityEditor;
using UnityEngine;

namespace BulletPoint
{
	[CustomEditor(typeof(BulletPoint))]
	public class BulletPointEditor : Editor
	{

		#region variables
		private string newItemText;
		#endregion

		#region unity_methods
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			BulletPoint bulletPointList = target as BulletPoint;

			if ( GUILayout.Button("Update All Points", GUILayout.Height(25)) )
			{
				foreach ( BulletPointItem item in bulletPointList.GetComponentsInChildren<BulletPointItem>() )
				{
					item.Color = bulletPointList.textColor;
					item.Size = bulletPointList.textSize;
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Add new items", EditorStyles.boldLabel);

			EditorGUILayout.LabelField("To add a new bullet point write the content in the text area below and press the \"Add to list\" button");
			EditorStyles.label.wordWrap = true;
			EditorStyles.textArea.wordWrap = true;

			newItemText = EditorGUILayout.TextArea(newItemText, GUILayout.MinHeight(100));

			if ( GUILayout.Button("Add to list", GUILayout.Height(25)) )
			{
				bulletPointList.AddBulletPoint(newItemText);
				Debug.Log(newItemText);
			}
			EditorGUILayout.HelpBox("The added bullet point will use the color and size as typed above.", MessageType.Warning);

		}
		#endregion

		#region public_methods

		#endregion

		#region private_methods

		#endregion
	}
}

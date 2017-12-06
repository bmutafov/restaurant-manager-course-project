using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletPoint
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(TextMeshProUGUI))]
	[RequireComponent(typeof(ContentSizeFitter))]
	public class BulletPointItem : MonoBehaviour
	{

		#region variables
		private ContentSizeFitter contentSizeFitter;
		private TextMeshProUGUI textField;

		private char circleBullet = '•';

		public string Text
		{
			get
			{
				return textField.text;
			}

			set
			{
				textField.text = circleBullet.ToString() + " " + value;
			}
		}

		public Color Color
		{
			get
			{
				return textField.color;
			}
			set
			{
				textField.color = value;
			}
		}

		public float Size
		{
			get
			{
				return textField.fontSize;
			}
			set
			{
				textField.fontSize = value;
			}
		}
		#endregion

		#region unity_methods
		private void OnEnable ()
		{
			contentSizeFitter = GetComponent<ContentSizeFitter>();
			textField = GetComponent<TextMeshProUGUI>();

			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		}
		#endregion

		#region public_methods
		#endregion

		#region private_methods

		#endregion
	}
}
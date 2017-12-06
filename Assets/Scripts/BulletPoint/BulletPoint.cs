using UnityEngine;
using UnityEngine.UI;

namespace BulletPoint
{

	[ExecuteInEditMode]
	[RequireComponent(typeof(VerticalLayoutGroup))]
	[RequireComponent(typeof(ContentSizeFitter))]
	public class BulletPoint : MonoBehaviour
	{

		#region variables
		[Header("Text style")]
		public Color textColor = Color.black;
		public float textSize = 16;

		private VerticalLayoutGroup verticalGroup;
		private ContentSizeFitter contentSizeFitter;
		private GameObject itemPrefab;
		#endregion

		#region unity_methods
		private void OnEnable ()
		{
			verticalGroup = GetComponent<VerticalLayoutGroup>();
			contentSizeFitter = GetComponent<ContentSizeFitter>();

			verticalGroup.childControlWidth = true;
			verticalGroup.childControlHeight = false;

			verticalGroup.childForceExpandHeight = false;
			verticalGroup.childForceExpandWidth = true;

			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

			FindPrefab();
		}
		#endregion

		#region public_methods
		public BulletPointItem AddBulletPoint ( string text )
		{
			FindPrefab();

			var item = Instantiate(itemPrefab, transform);
			item.transform.parent = transform;

			BulletPointItem bulletPointItem = item.AddComponent<BulletPointItem>();
			bulletPointItem.Text = text;
			bulletPointItem.Color = textColor;
			bulletPointItem.Size = textSize;
			return bulletPointItem;
		}

		#endregion

		#region private_methods
		private void FindPrefab ()
		{
			if ( itemPrefab == null )
			{
				itemPrefab = ( GameObject ) Resources.Load("itemPrefab", typeof(GameObject));
			}
		}
		#endregion
	}
}

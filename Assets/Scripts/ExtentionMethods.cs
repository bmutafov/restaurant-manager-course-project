using UnityEngine;

namespace ExtensionMethods
{
	public static class TransformExtentions
	{
		public static Transform FirstChild ( this Transform trans )
		{
			return trans.childCount > 0 ? trans.GetChild(0) : null;
		}
	}
}
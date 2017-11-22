using TMPro;
using UnityEngine;

public class UI : GenericSingletonClass<UI>
{
	public GameObject successScreen;
	public GameObject errorScreen;
    /// <summary>
    /// Moves a RectTransform to a gameObjects position
    /// </summary>
    /// <param name="toMove">UI's GameObject</param>
    /// <param name="position">Position of the GameObject</param>
    /// <param name="x">X offset</param>
    /// <param name="y">Y offset</param>
    public static void MoveUIToGameObjectPosition (GameObject toMove, Vector3 position, float x, float y)
    {
        Vector3 screenPos = GameObject.Find("MainCamera").GetComponent<Camera>().WorldToScreenPoint(position);
        screenPos = new Vector2(screenPos.x + x, screenPos.y + y);
        toMove.GetComponent<RectTransform>().position = screenPos;
    }

    public static void UpdateChildTextMeshText (Transform transformToUpdate, int index, string newText)
    {
        var obj = transformToUpdate;
        if ( index >= 0 )
        {
            obj = transformToUpdate.GetChild(index);
        }
        obj.GetComponent<TextMeshProUGUI>().text = newText;
    }

	public void OpenSuccessScreen (string message)
	{
		successScreen.SetActive(true);
		successScreen.transform.Find("Message").GetComponent<TextMeshProUGUI>().text = message;
	}

	public void OpenErrorScreen ( string message )
	{
		errorScreen.SetActive(true);
		errorScreen.transform.Find("Message").GetComponent<TextMeshProUGUI>().text = message;
	}
}
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static void MoveUIWithObject (GameObject toMove, Vector3 position, float x, float z)
    {
        Vector3 screenPos = GameObject.Find("MainCamera").GetComponent<Camera>().WorldToScreenPoint(position);
        screenPos = new Vector2(screenPos.x + x, screenPos.y + z);
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
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayUIUpdater : MonoBehaviour
{

    private TextMeshProUGUI TMPtext;

    private void Start ()
    {
        TMPtext = GetComponent<TextMeshProUGUI>();
        DayCycle.Instance.onMinuteChangedCallback += UpdateUIText;
    }

    private void UpdateUIText ()
    {
        TMPtext.text = DayCycle.Instance.GameTime.ToString("HH:mm");
    }
}

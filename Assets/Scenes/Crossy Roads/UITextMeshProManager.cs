using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextMeshProManager : MonoBehaviour
{
    public TextMeshProUGUI leG;

    public TextMeshProUGUI Toucher;
    public TextMeshProUGUI prevenir;

    public void UpdateLapText(string message)
    {
        leG.text = message;
    }

    public void UpdateTouchText(string message)
    {
        Toucher.text = message;
    }

    public void UpdatePrevText(string message)
    {
        prevenir.text = message;
    }
}

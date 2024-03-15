using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public void Z_TestClick()
    {
        TextMeshProUGUI text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(text.text + " Button Clicked");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompanionMessages : MonoBehaviour
{

    [SerializeField] private TMP_Text messageTextBox;
    [SerializeField] private bool DEVTOOLsendMessage; //DEV TOOL
    [SerializeField] private bool DEVTOOLkillMessage; //DEV TOOL

    // Start is called before the first frame update
    void Start()
    {
        DEVTOOLsendMessage = false; //DEV TOOL
        DEVTOOLkillMessage = false; //DEV TOOL
        messageTextBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DEVTOOLsendMessage)
        {
            showMessage("Hehe I am messaging u. Better not get scared. Gooo gooo gaga.", "Commanding Officer Robinson");
            DEVTOOLsendMessage = false;
        }

        if (DEVTOOLkillMessage)
        {
            hideMessage();
            DEVTOOLkillMessage = false;
        }
    }

    public void showMessage(string message, string sender)
    {
        messageTextBox.text = "< " + sender.ToUpper() + " >\n" + message;
        messageTextBox.enabled = true;
    }

    public void hideMessage()
    {
        messageTextBox.enabled = false;
    }
}

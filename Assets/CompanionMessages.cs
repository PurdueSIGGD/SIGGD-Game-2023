using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
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
            showMessage("Hehe I am messaging u. Better not get scared. Gooo gooo ga ga.", "GIBBAH", false);
            DEVTOOLsendMessage = false;
        }

        if (DEVTOOLkillMessage)
        {
            hideMessage();
            DEVTOOLkillMessage = false;
        }
    }

    public IEnumerator showMessage(string message, string sender, bool slow)
    {
        messageTextBox.text = "< " + sender.ToUpper() + " >\n";
        messageTextBox.enabled = true;
        //StartCoroutine(typeMessage(message, slow));
        string[] messageWords = message.Split(" ");
        foreach (string word in messageWords)
        {
            if (slow)
            {
                yield return new WaitForSeconds(0.7f);
            }
            if ((messageTextBox.text.LastIndexOf(".") == messageTextBox.text.Length - 2) ||
                (messageTextBox.text.LastIndexOf("?") == messageTextBox.text.Length - 2) ||
                (messageTextBox.text.LastIndexOf("!") == messageTextBox.text.Length - 2))
            {
                yield return new WaitForSeconds(0.4f);
            }
            else if (messageTextBox.text.LastIndexOf(",") == messageTextBox.text.Length - 2)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(Random.Range(0.125f, 0.25f));
            messageTextBox.text += word + " ";
        }
    }

    public void hideMessage()
    {
        messageTextBox.enabled = false;
    }

    /*public IEnumerator typeMessage(string message, bool slow)
    {
        string[] messageWords = message.Split(" ");
        foreach (string word in messageWords)
        {
            if (slow)
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(Random.Range(0.1f, 0.35f));
            messageTextBox.text += word + " ";
        }
    }*/
}

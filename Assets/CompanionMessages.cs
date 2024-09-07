using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CompanionMessages : MonoBehaviour
{

    [SerializeField] private GameObject messageBackground;
    [SerializeField] private TMP_Text messageTextBox;
    [SerializeField] private GameObject bigMessageBackground;
    [SerializeField] private TMP_Text bigMessageTextBox;
    [SerializeField] private AudioSource messageSound;
    [SerializeField] private AudioSource slowMessageSound;
    [SerializeField] private bool DEVTOOLsendMessage; //DEV TOOL
    [SerializeField] private bool DEVTOOLkillMessage; //DEV TOOL

    // Start is called before the first frame update
    void Start()
    {
        DEVTOOLsendMessage = false; //DEV TOOL
        DEVTOOLkillMessage = false; //DEV TOOL
        messageBackground.SetActive(false);
        messageTextBox.enabled = false;
        bigMessageBackground.SetActive(false);
        bigMessageTextBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DEVTOOLsendMessage)
        {
            StartCoroutine(showMessage("Hehe I am messaging u. Better not get scared. Gooo gooo ga ga.", "GIBBAH", false));
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
        messageBackground.SetActive(true);
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
                (messageTextBox.text.LastIndexOf("!") == messageTextBox.text.Length - 2) ||
                (messageTextBox.text.LastIndexOf(":") == messageTextBox.text.Length - 2))
            {
                yield return new WaitForSeconds(0.4f);
            }
            else if (messageTextBox.text.LastIndexOf(",") == messageTextBox.text.Length - 2)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(Random.Range(0.125f, 0.25f));
            messageTextBox.text += word + " ";
            if (slow)
            {
                slowMessageSound.Play();
            }
            else
            {
                messageSound.Play();
            }
        }
    }

    public IEnumerator showMessage(string message, string sender, bool slow, bool big)
    {
        if (!big)
        {
            yield return showMessage(message, sender, slow);
        }

        bigMessageTextBox.text = "< " + sender.ToUpper() + " >\n";
        bigMessageBackground.SetActive(true);
        bigMessageTextBox.enabled = true;
        //StartCoroutine(typeMessage(message, slow));
        string[] messageWords = message.Split(" ");
        foreach (string word in messageWords)
        {
            if (slow)
            {
                yield return new WaitForSeconds(0.7f);
            }
            if ((bigMessageTextBox.text.LastIndexOf(".") == bigMessageTextBox.text.Length - 2) ||
                (bigMessageTextBox.text.LastIndexOf("?") == bigMessageTextBox.text.Length - 2) ||
                (bigMessageTextBox.text.LastIndexOf("!") == bigMessageTextBox.text.Length - 2) ||
                (bigMessageTextBox.text.LastIndexOf(":") == bigMessageTextBox.text.Length - 2))
            {
                yield return new WaitForSeconds(0.4f);
            }
            else if (bigMessageTextBox.text.LastIndexOf(",") == bigMessageTextBox.text.Length - 2)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(Random.Range(0.125f, 0.25f));
            bigMessageTextBox.text += word + " ";
            /*if (slow)
            {
                slowMessageSound.Play();
            }
            else
            {
                messageSound.Play();
            }*/
        }
    }

    public void hideMessage()
    {
        messageBackground.SetActive(false);
        messageTextBox.enabled = false;
    }

    public void hideMessage(bool big)
    {
        if (!big)
        {
            hideMessage();
            return;
        }
        bigMessageBackground.SetActive(false);
        bigMessageTextBox.enabled = false;
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

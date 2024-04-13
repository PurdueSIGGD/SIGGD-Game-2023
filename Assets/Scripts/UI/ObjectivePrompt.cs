using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivePrompt : MonoBehaviour
{

    [SerializeField] private TMP_Text promptTextBox;

    // Start is called before the first frame update
    void Start()
    {
        promptTextBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPrompt(string promptMessage)
    {
        promptTextBox.text = promptMessage.ToUpper();
        promptTextBox.enabled = true;
    }

    public void hidePrompt()
    {
        promptTextBox.enabled = false;
    }
}

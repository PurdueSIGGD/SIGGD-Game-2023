using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{

    [SerializeField] private TMP_Text promptTextBox;
    [SerializeField] private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        promptTextBox.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.rotation = mainCamera.transform.rotation;
    }

    public void showPrompt(string promptMessage)
    {
        promptTextBox.text = promptMessage;
        promptTextBox.enabled = true;
    }

    public void hidePrompt()
    {
        promptTextBox.enabled = false;
    }
}

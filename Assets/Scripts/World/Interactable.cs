using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    [SerializeField] public string promptMessage;
    [SerializeField] public InteractPrompt interactPrompt;

    public bool isUsed;
    public bool playerInRange;

    // Start is called before the first frame update
    public virtual void Start()
    {
        isUsed = false;
        playerInRange = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isUsed)
        {
            playerInRange = true;
            interactPrompt.showPrompt(promptMessage);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            interactPrompt.hidePrompt();
        }
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (playerInRange && !isUsed && Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
    }


    public virtual void interact()
    {
        if (!isUsed)
        {
            interactPrompt.hidePrompt();
            isUsed = true;
        }
    }

}

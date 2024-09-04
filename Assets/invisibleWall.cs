using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibleWall : MonoBehaviour
{

    [SerializeField] public TutorialDirector tutorialDirector;
    [SerializeField] public InteractPrompt interactPrompt;
    [SerializeField] public Collider boxCollider;
    [SerializeField] public int startTime;
    [SerializeField] public int endTime;

    public bool isBlocking = false;
    private string messagePrompt = "Stand By";

    // Start is called before the first frame update
    void Start()
    {
        isBlocking = false;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialDirector.tutorialProgress >= startTime && 
            tutorialDirector.tutorialProgress < endTime &&
            !isBlocking)
        {
            isBlocking = true;
            boxCollider.enabled = true;
        }

        if ((tutorialDirector.tutorialProgress < startTime ||
            tutorialDirector.tutorialProgress >= endTime) &&
            isBlocking)
        {
            isBlocking = false;
            boxCollider.enabled = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(prompt());
        }
    }


    private IEnumerator prompt()
    {
        interactPrompt.showPrompt(messagePrompt);
        yield return new WaitForSeconds(3f);
        interactPrompt.hidePrompt();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSearchTrigger : MonoBehaviour
{

    [SerializeField] public TutorialDirector tutorialDirector;
    public Collider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = GetComponent<Collider>();
        triggerCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialDirector.lightSearchState == sequenceState.READY && !triggerCollider.enabled)
        {
            triggerCollider.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == null) return;

        if ((other.gameObject.tag == "Player") &&
            (tutorialDirector.lightSearchState == sequenceState.READY))
        {
            tutorialDirector.lightSearchTrigger = true;
            triggerCollider.enabled = false;
        }
    }
}

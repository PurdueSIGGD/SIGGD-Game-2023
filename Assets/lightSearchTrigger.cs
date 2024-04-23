using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSearchTrigger : MonoBehaviour
{

    [SerializeField] public TutorialDirector tutorialDirector;
    //public Collider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        //triggerCollider = GetComponent<Collider>();
        //triggerCollider.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialDirector.lightSearchTrigger.sequenceState == sequenceState.READY && !GetComponent<Collider>().enabled)//!triggerCollider.enabled)
        {
            //triggerCollider.enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == null) return;

        if ((other.gameObject.tag == "Player") &&
            (tutorialDirector.lightSearchTrigger.sequenceState == sequenceState.READY))
        {
            tutorialDirector.lightSearchTrigger.triggered = true;
            //triggerCollider.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}

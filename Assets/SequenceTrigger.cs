using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTrigger : MonoBehaviour
{
    //[SerializeField] public TutorialDirector tutorialDirector;
    public sequenceState sequenceState;
    public bool triggered;
    //public Collider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        //triggerCollider = GetComponent<Collider>();
        //triggerCollider.enabled = false;
        GetComponent<Collider>().enabled = false;

        sequenceState = sequenceState.WAITING;
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sequenceState == sequenceState.READY && !GetComponent<Collider>().enabled)//!triggerCollider.enabled)
        {
            //triggerCollider.enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == null) return;

        if ((other.gameObject.tag == "Player") &&
            (sequenceState == sequenceState.READY) &&
            (!triggered))
        {
            //tutorialDirector.lightSearchTrigger = true;
            //triggerCollider.enabled = false;
            triggered = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}

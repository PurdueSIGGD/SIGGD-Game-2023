using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCallback : MonoBehaviour
{
    // -- Fields --
    GameObject enterCallback;
    string enterName;
    GameObject exitCallback;
    string exitName;

    // -- Override Methods --

    private void OnTriggerEnter(Collider other)
    {
        enterCallback.BroadcastMessage(enterName, other);
    }

    private void OnTriggerExit(Collider other)
    {
        exitCallback.BroadcastMessage(exitName, other);
    }

    // -- Instance Fields --
    public void SetEnterCallback(GameObject callback, string name)
    {
        this.enterCallback= callback;
        this.enterName= name;
    }

    public void SetExitCallback(GameObject callback, string name)
    {
        this.exitCallback= callback;
        this.exitName= name;
    }
}

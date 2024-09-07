using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjState
{
    public abstract void EnterState(MonoBehaviour context);
    public abstract void UpdateState(MonoBehaviour context);

    public abstract void OnTriggerEnter(MonoBehaviour context, Collider collider);
}

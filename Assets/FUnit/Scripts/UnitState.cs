using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState
{
    public abstract void EnterState(Unit context);
    public abstract void UpdateState(Unit context);

    public abstract void OnTriggerEnter(Unit context, Collider other);

}

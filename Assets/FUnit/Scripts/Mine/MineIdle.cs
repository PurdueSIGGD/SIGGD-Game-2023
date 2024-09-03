using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        MineFSM mine = (MineFSM)context;
        SphereCollider collider = mine.GetComponent<SphereCollider>();
        collider.radius = mine.config.triggerRadius;
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        MineFSM mine = (MineFSM)context;
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            mine.SwitchState(MineFSM.triggerState);
        }
    }

    public override void UpdateState(Unit context)
    {
        return;
    }
}

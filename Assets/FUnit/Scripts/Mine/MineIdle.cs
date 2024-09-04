using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        MineFSM mine = (MineFSM)context;
        mine.personal.armed = false;
        mine.StartCoroutine(enterIdle(mine));
        //SphereCollider collider = mine.GetComponent<SphereCollider>();
        //collider.radius = mine.config.triggerRadius;
        
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        MineFSM mine = (MineFSM)context;
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && mine.personal.armed)
        {
            mine.SwitchState(MineFSM.triggerState);
        }
    }

    public override void UpdateState(Unit context)
    {
        return;
    }

    public IEnumerator enterIdle(Unit context)
    {
        MineFSM mine = (MineFSM)context;
        mine.personal.armed = false;
        SphereCollider collider = mine.GetComponent<SphereCollider>();
        collider.radius = mine.config.triggerRadius;
        yield return new WaitForSeconds(0.2f);
        mine.personal.armed = true;
    }
}

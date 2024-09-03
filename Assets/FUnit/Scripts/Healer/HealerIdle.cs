using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        healerContext.animator.ResetTrigger("Pulse");
        healerContext.pulseTime = healerContext.cooldown;
        healerContext.player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void UpdateState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        if (healerContext.pulseTime == 0)
        {
            healerContext.SwitchState(healerContext.pulseState);
        }
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }
}

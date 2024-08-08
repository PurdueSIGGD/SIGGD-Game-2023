using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
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
}

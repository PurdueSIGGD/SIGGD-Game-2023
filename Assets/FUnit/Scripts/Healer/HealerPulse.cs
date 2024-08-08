using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerPulse : UnitState
{
    public override void EnterState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        healerContext.animator.SetTrigger("Pulse");
        healerContext.player.GetComponent<HealthPoints>().healEntity(healerContext.healAmount);
        healerContext.player.GetComponent<LightResource>().addLight(healerContext.lightAmount);

        healerContext.pulseTime = 1;
    }

    public override void UpdateState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        if (healerContext.pulseTime == 0)
        {
            healerContext.SwitchState(healerContext.idleState);
        }
    }
}

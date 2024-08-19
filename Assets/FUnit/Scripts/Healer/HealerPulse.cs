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

        healerContext.pulseTime = healerContext.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public override void UpdateState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        if (healerContext.pulseTime == 0)
        {
            healerContext.SwitchState(healerContext.idleState);
        }
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }
}

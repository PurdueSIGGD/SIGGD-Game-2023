using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        healerContext.animator.ResetTrigger("Pulse");
        if (healerContext.pulsesCount == healerContext.totalPulses)
        {
            //healerContext.healthPoints.damageEntity(1000f);
            healerContext.StartCoroutine(killHealer(healerContext, 0.5f));
        }
        healerContext.pulsesCount++;
        if (healerContext.pulsesCount != 1 )
        {
            healerContext.pulseTime = healerContext.cooldown;
        }
        else
        {
            healerContext.pulseTime = 0f;
        }
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


    IEnumerator killHealer(HealerFSM context, float wait)
    {
        HealerFSM healer = (HealerFSM)context;
        yield return new WaitForSeconds(wait);
        healer.healthPoints.damageEntity(1000f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerPulse : UnitState
{
    public override void EnterState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        healerContext.animator.SetTrigger("Pulse");
        healerContext.StartCoroutine(heal(healerContext, 0.3f));
        //healerContext.player.GetComponent<HealthPoints>().healEntity(healerContext.healAmount);
        //healerContext.player.GetComponent<LightResource>().addLight(healerContext.lightAmount);

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

    IEnumerator heal(Unit context, float wait)
    {
        yield return new WaitForSeconds(wait);
        HealerFSM healer = (HealerFSM)context;
        healer.player.GetComponent<HealthPoints>().healEntity(healer.healAmount);
        healer.player.GetComponent<LightResource>().addLight(healer.lightAmount);
    }
}

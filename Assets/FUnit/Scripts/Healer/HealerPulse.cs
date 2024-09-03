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
        HealerFSM healer = (HealerFSM)context;
        float healing = (healer.pulsesCount == 1) ? healer.initialHealAmount : healer.healAmount;
        healer.healSound.pitch += 0.3f;
        healer.healSound.volume = (healer.pulsesCount == 1) ? 0.4f : 0.1f;
        healer.healSound.Play();
        yield return new WaitForSeconds(wait);
        //float healing = (healer.pulsesCount == 2) ? healer.initialHealAmount : healer.healAmount;
        Debug.Log("initialHealAmount: " + healer.initialHealAmount + "  |  healing: " + healing + "  |  pulsedCount: " + healer.pulsesCount);
        healer.player.GetComponent<HealthPoints>().healEntity(healing);
        healer.player.GetComponent<LightResource>().addLight(healer.lightAmount);
    }
}

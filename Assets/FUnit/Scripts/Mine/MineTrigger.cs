using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : UnitState
{
    public override void EnterState(Unit context)
    {
        MineFSM mine = (MineFSM)context;
        mine.config.animator.SetTrigger("Trigger");
        float time = mine.config.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        context.StartCoroutine(Trigger(context, time));
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }

    public override void UpdateState(Unit context)
    {
        return;
    }

    IEnumerator Trigger(Unit context, float wait)
    {
        yield return new WaitForSeconds(wait);
        MineFSM mine = (MineFSM)context;
        mine.SwitchState(MineFSM.boomState);
    }
}

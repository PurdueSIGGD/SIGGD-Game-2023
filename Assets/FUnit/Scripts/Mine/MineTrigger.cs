using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : UnitState
{
    public override void EnterState(Unit context)
    {
        MineFSM mine = (MineFSM)context;
        mine.config.animator.SetTrigger("Trigger");
        context.StartCoroutine(Trigger(context, 0.750f));
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

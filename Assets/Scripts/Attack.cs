using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype for possible attack state
public class Attack : EnemyState
{
    public override void StateStart()
    {
        Debug.Log("Attack Start");
    }

    public override void StateStop()
    {
        Debug.Log("Attack Stop");
    }

    public override void StateUpdate()
    {
        Debug.Log("Attack");
        controller.SwitchState(controller.idleState);
    }
}

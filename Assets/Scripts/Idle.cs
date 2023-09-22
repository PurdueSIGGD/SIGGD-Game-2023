using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype for possible idle state
public class Idle : EnemyState
{
    public override void StateStart()
    {
        Debug.Log("Idle Start");
    }

    public override void StateStop()
    {
        Debug.Log("Idle Stop");
    }

    public override void StateUpdate()
    {
        Debug.Log("Idle");
        controller.SwitchState(controller.attackState);
    }
}

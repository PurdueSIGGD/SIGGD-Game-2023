using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    public override void StateTick()
    {
        Debug.Log("Idle Tick");
    }
}

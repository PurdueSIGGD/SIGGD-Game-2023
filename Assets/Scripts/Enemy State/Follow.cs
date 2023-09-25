using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : EnemyState
{
    public override void StateStart()
    {
        Debug.Log("Follow Start");
    }

    public override void StateStop()
    {
        Debug.Log("Follow Stop");
    }

    public override void StateUpdate()
    {

    }

    public override void StateTick()
    {
        Debug.Log("Follow Tick");
    }
}

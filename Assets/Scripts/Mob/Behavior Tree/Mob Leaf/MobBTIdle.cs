using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTIdle : BTLeafNode
{
    public MobBTIdle(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        Debug.Log("Idle Evaluate");
        navigationController.behavior = MobNavigationController.NavBehavior.Idle;
        return (BTResult.Running, this);
    }

    public override void NodeUpdate()
    {
        return;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTFollow : BTLeafNode
{
    private MobNavigationController navigationController;

    public MobBTFollow(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.navigationController = gameObject?.GetComponent<MobNavigationController>();
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        Debug.Log("Follow Evaluate");
        navigationController.behavior = MobNavigationController.NavBehavior.Pursue;
        return (BTResult.Running, this);
    }

    public override void NodeUpdate()
    {
        return;
    }
}
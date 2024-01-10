using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTIsVisible : BTLeafNode
{
    private MobTargetingController targetingController;

    public MobBTIsVisible(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.targetingController = gameObject?.GetComponent<MobTargetingController>();
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        Debug.Log("IsVisible Evaluate");
        return (BTResult.Success, this);
    }

    public override void NodeUpdate()
    {
        return;
    }
}
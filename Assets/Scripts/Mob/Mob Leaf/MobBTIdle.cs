using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTIdle: BTLeafNode
{
    private MobNavigationController mobNavigationController;
    private MobTargetingController mobTargetingController;

    public MobBTIdle(BTCompositeNode parent) : base(parent)
    {
        GameObject gObj = parent.gameObject;
        mobNavigationController = gObj.GetComponent<MobNavigationController>();
        mobTargetingController = gObj.GetComponent<MobTargetingController>();
    }

    public MobBTIdle(BTDecoratorNode parent) : base(parent)
    {
        GameObject gObj = parent.gameObject;
        mobNavigationController = gObj.GetComponent<MobNavigationController>();
        mobTargetingController = gObj.GetComponent<MobTargetingController>();
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        mobNavigationController.behavior = MobNavigationController.NavBehavior.Idle;

        return (BTResult.Running, this);
    }
}
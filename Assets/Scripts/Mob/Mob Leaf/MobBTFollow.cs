using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTFollow : BTLeafNode
{
    private MobNavigationController mobNavigationController;
    private MobTargetingController mobTargetingController;

    public MobBTFollow(BTCompositeNode parent) : base(parent)
    {
        GameObject gObj = parent.gameObject;
        mobNavigationController = gObj.GetComponent<MobNavigationController>();
        mobTargetingController = gObj.GetComponent<MobTargetingController>();
    }

    public MobBTFollow(BTDecoratorNode parent) : base(parent)
    {
        GameObject gObj = parent.gameObject;
        mobNavigationController = gObj.GetComponent<MobNavigationController>();
        mobTargetingController = gObj.GetComponent<MobTargetingController>();
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        mobNavigationController.isActive = true;
        mobNavigationController.behavior = MobNavigationController.NavBehavior.Pursue;

        return (BTResult.Running, this);
    }
}
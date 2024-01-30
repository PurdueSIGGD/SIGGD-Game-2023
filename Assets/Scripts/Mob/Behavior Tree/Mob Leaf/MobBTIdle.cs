using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTIdle: BTAction
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
        return (BTResult.Running, this);
    }

    public override void NodeUpdate()
    {
        mobNavigationController.behavior = MobNavigationController.NavBehavior.Idle;
    }

    public override void StartRunning()
    {

    }

    public override void StopRunning()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTDash : BTLeafNode
{
    private MobNavigationController mobNavigationController;
    private MobTargetingController mobTargetingController;

    public MobBTDash(BTCompositeNode parent) : base(parent)
    {
        GameObject gObj = parent.gameObject;
        mobNavigationController = gObj.GetComponent<MobNavigationController>();
        mobTargetingController = gObj.GetComponent<MobTargetingController>();
    }

    public MobBTDash(BTDecoratorNode parent) : base(parent)
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
        mobNavigationController.isActive = true;
        mobNavigationController.behavior = MobNavigationController.NavBehavior.Dash;
    }

    public override void StartRunning()
    {

    }

    public override void StopRunning()
    {

    }
}
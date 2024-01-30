using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTTargetInRange : BTConditionalNode
{
	private MobNavigationController mobNavigationController;
	private MobTargetingController mobTargetingController;

	private float minRange;
	private float maxRange;

	public MobBTTargetInRange(BTCompositeNode parent, float minRange, float maxRange) : base(parent)
	{
		GameObject gObj = parent.gameObject;
		mobNavigationController = gObj.GetComponent<MobNavigationController>();
		mobTargetingController = gObj.GetComponent<MobTargetingController>();

		this.minRange = minRange;
		this.maxRange = maxRange;
	}

	public MobBTTargetInRange(BTDecoratorNode parent, float minRange, float maxRange) : base(parent)
	{
		GameObject gObj = parent.gameObject;
		mobNavigationController = gObj.GetComponent<MobNavigationController>();
		mobTargetingController = gObj.GetComponent<MobTargetingController>();

        this.minRange = minRange;
        this.maxRange = maxRange;
    }

	public override (BTResult, BTLeafNode) Evaluate()
	{
		if (
			mobTargetingController.target != null
			&& minRange <= mobTargetingController.distanceToTarget
			&& mobTargetingController.distanceToTarget <= maxRange
			)
		{
			return (BTResult.Success, null);
		}

        return (BTResult.Failure, null);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PiranhaPursueBehavior : Behavior
{
	private GameObject gameObject;
	private PiranhaNavigationController navigationController;

	private PiranhaEnemyGoal goal;

	public PiranhaPursueBehavior(GameObject gameObject)
	{
		this.gameObject = gameObject;
		this.navigationController = gameObject.GetComponent<PiranhaNavigationController>();
	}

	protected override bool EvaluatePreConditions()
	{
		return true;
	}
	
	public override float InitializeAndGetPriority()
	{
		if (!EvaluatePreConditions()) {
			return -1.0f;
		}

		return 1.0f;
	}

	public override void StartBehavior()
	{
		Debug.Log("Pursue Start");
	}

	public override void StopBehavior()
	{
		Debug.Log("Pursue Stop");
		goal = null;
	}

	public override BehaviorResult BehaviorFixedUpdate()
	{
		Transform targetTransform = goal.targetTransform;
		
		if (navigationController.Pursue(targetTransform) == true)
		{
			return BehaviorResult.Success;
		}
		else
		{
			return BehaviorResult.Failure;
		}
	}

	private bool isActive;
	public override bool IsActive()
	{
		return isActive;
	}
}
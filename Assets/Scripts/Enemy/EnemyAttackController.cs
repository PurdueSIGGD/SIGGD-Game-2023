using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
	[SerializeField] private float cooldownTime;
	private float currentCooldown = 0;

	[SerializeField] private float attackDistance; // The radius around the entity within which attacks will be made. Attacks will also be made at this distance from the entity.
	[SerializeField] private float attackRadius;

	private EnemyTargetingController targetingController;
	private Transform selfTransform;
	private LayerMask targetMask;

	void Start()
	{
		targetingController = GetComponent<EnemyTargetingController>();
		selfTransform = GetComponent<Transform>();
		targetMask = LayerMask.GetMask("Player"); // TODO add units to this mask
	}

	void FixedUpdate()
	{
		if (currentCooldown > 0) currentCooldown -= Time.fixedDeltaTime;
		if (currentCooldown > 0) return;

		if (!targetingController.target) 
		{ 
			return; 
		}
	
		Transform targetTransform = targetingController.target.transform;
		Vector3 targetDirection = targetTransform.position - selfTransform.position;

		float distance = targetDirection.magnitude;
		if (distance > attackDistance)
		{
			return;
		}

		// Spawn Attack Collision Check
		Vector3 normalizedDirection = Vector3.Normalize(targetDirection) * attackDistance; // Change to always be at target position?
		Collider[] hitTargets = Physics.OverlapSphere(selfTransform.position + normalizedDirection, attackRadius, targetMask, QueryTriggerInteraction.Ignore);
		foreach (Collider targetCollider in hitTargets)
		{
			GameObject target = targetCollider.gameObject;
			Debug.Log("Enemy Attack hit target" + target);
			// Do stuff
		}

		currentCooldown = cooldownTime;
	}
}
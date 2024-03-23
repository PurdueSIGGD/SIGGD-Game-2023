using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackController : MonoBehaviour
{
	[SerializeField] private float cooldownTime;
	private float currentCooldownStart;
	
	//[SerializeField] private float attackDistance; // The radius around the entity within which attacks will be made. Attacks will also be made at this distance from the entity.
	//[SerializeField] private float attackRadius;
	private SphereCollider attackCollider;
	private float DAMAGE;

	private EnemyTargetingController targetingController;
	private Transform selfTransform;
	private LayerMask targetMask;
	private bool onCooldown;

	void Start()
	{
		targetingController = GetComponent<EnemyTargetingController>();
		selfTransform = GetComponent<Transform>();
		//targetMask = LayerMask.GetMask("Player"); // TODO add units to this mask
		attackCollider = this.GetComponent<SphereCollider>();
		onCooldown = false;
		currentCooldownStart = Time.time;
	}

	void FixedUpdate()
	{
		// if (currentCooldown > 0) currentCooldown -= Time.fixedDeltaTime;
		// if (currentCooldown > 0) return;


		if (onCooldown && (Time.time - currentCooldownStart > cooldownTime)) {
			onCooldown = false;
		}
		// if (!targetingController.target) 
		// { 
		// 	return; 
		// }
	
		//Transform targetTransform = targetingController.target.transform;
		//Vector3 targetDirection = targetTransform.position - selfTransform.position;

		// float distance = targetDirection.magnitude;
		// if (distance > attackDistance)
		// {
		// 	return;
		// }

		// Spawn Attack Collision Check
		// Vector3 normalizedDirection = Vector3.Normalize(targetDirection) * attackDistance; // Change to always be at target position?
		// Collider[] hitTargets = Physics.OverlapSphere(selfTransform.position + normalizedDirection, attackRadius);
		// foreach (Collider targetCollider in hitTargets)
		// {
		// 	GameObject target = targetCollider.gameObject;
		// 	if (target.tag != "Player") continue; // << Bandaid; Layermask in OverlapSphere not working
		// 	Debug.Log("Enemy Attack hit target: " + target);
		// 	// Do stuff
		// }

		// currentCooldown = cooldownTime;
	}

	private void OnTriggerEnter(Collider col) {
		if (onCooldown == false) {
			currentCooldownStart = Time.time;
			HealthPoints enemyHealthPoint
		}
	}
}
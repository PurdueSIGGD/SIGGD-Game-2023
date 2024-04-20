using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackController : MonoBehaviour
{
	[SerializeField] private float cooldownTime;
	private float currentCooldownStart;
	
	//[SerializeField] private float attackDistance; // The radius around the entity within which attacks will be made. Attacks will also be made at this distance from the entity.
	//[SerializeField] private float attackRadius;
	private SphereCollider attackCollider;
	[SerializeField] private float DAMAGE;

	private bool onCooldown;

	private List<GameObject> overlapping;	

	void Start()
	{
		attackCollider = this.GetComponent<SphereCollider>();
		onCooldown = false;
		currentCooldownStart = Time.time;
		overlapping = new List<GameObject>();
	}

	void FixedUpdate()
	{
		// if (currentCooldown > 0) currentCooldown -= Time.fixedDeltaTime;
		// if (currentCooldown > 0) return;

		if (onCooldown == false) {
			onCooldown = true;
			currentCooldownStart = Time.time;

			foreach (GameObject obj in overlapping) {
				Debug.Log("obj");
				HealthPoints healhscript = obj.GetComponent<HealthPoints>();
				if (healhscript) {
					Debug.Log("damage");
					healhscript.damageEntity(DAMAGE);
				}
			}
		}

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
		if (overlapping.Contains(col.gameObject) == false) {
			overlapping.Add(col.gameObject);
		}
	}

	private void OnTriggerExit(Collider col) {
		if (overlapping.Contains(col.gameObject)) {
			overlapping.Remove(col.gameObject);
		}
	}
}
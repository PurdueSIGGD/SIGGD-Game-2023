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
	[SerializeField] private float DAMAGE;

	private bool onCooldown;

	private List<GameObject> overlapping;	

	void Start()
	{
		onCooldown = false;
		currentCooldownStart = Time.time;
		overlapping = new List<GameObject>();
	}

	void FixedUpdate()
	{

		if (onCooldown == false) {
			onCooldown = true;
			currentCooldownStart = Time.time;

			foreach (GameObject obj in overlapping) {
				if (obj == null) {
					overlapping.Remove(obj);
				}
				else {
					HealthPoints healhscript = obj.GetComponent<HealthPoints>();
					if (healhscript) {
						healhscript.damageEntity(DAMAGE);
					}
				}
			}
		}

		if (onCooldown && (Time.time - currentCooldownStart > cooldownTime)) {
			onCooldown = false;
		}

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
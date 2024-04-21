using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
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
		if (onCooldown == false && overlapping.Count > 0) {
			onCooldown = true;
			currentCooldownStart = Time.time;
			List<GameObject> toRemove = new List<GameObject>();
			foreach (GameObject obj in overlapping) {
				if (obj == null) {
					toRemove.Add(obj);
				}
				else {
					HealthPoints healhscript = obj.GetComponent<HealthPoints>();
					if (healhscript) {
						Damg(healhscript, DAMAGE);
					}
				}
			}
			foreach (GameObject obj in toRemove) {
				overlapping.Remove(obj);
			}
		}

		if (onCooldown && (Time.time - currentCooldownStart > cooldownTime)) {
			onCooldown = false;
		}

	}

	public virtual void Damg(HealthPoints point, float dmg) {
		point.damageEntity(dmg);
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
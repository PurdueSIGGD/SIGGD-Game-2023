using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenMelee : MonoBehaviour
{
    [SerializeField] private float finalMultiplier;
    [SerializeField] private float initialWait;
    [SerializeField] private float intermediateWait;
    [SerializeField] private float recoverTime;
    [SerializeField] private float cooldownTime;
	private float currentCooldownStart;private bool onCooldown;
	private List<GameObject> overlapping;
    [SerializeField] private float DAMAGE;

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
			StartCoroutine(Combo());
		}

		if (onCooldown && (Time.time - currentCooldownStart > cooldownTime)) {
			onCooldown = false;
		}

	}
    public virtual void Damg(float dmgs) {
		List<GameObject> toRemove = new List<GameObject>();
		foreach (GameObject obj in overlapping) {
			if (obj == null) {
				toRemove.Add(obj);
			}
			else {
				HealthPoints healhscript = obj.GetComponent<HealthPoints>();
				if (healhscript) {
					healhscript.damageEntity(dmgs);
				}
			}
		}
		foreach (GameObject obj in toRemove) {
			overlapping.Remove(obj);
		}
	}

    IEnumerator Combo() {
        GameObject this_parent = this.gameObject.transform.parent.gameObject;
        this_parent.GetComponent<SirenNav>().enabled = false;
        yield return new WaitForSeconds(initialWait);
        Damg(DAMAGE);
        yield return new WaitForSeconds(intermediateWait);
        Damg(DAMAGE);
        yield return new WaitForSeconds(intermediateWait);
        Damg(DAMAGE * finalMultiplier);
        this_parent.GetComponent<SirenAttack>().ShutItDown();  //shuts down grab attack when siren finishes melee
        yield return new WaitForSeconds(recoverTime);
        this_parent.GetComponent<SirenNav>().enabled = true;
        yield return null;
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

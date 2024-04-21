using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAttack : MonoBehaviour
{
    private Transform thisEnemy;
        
    private LayerMask turretMask;
    private LayerMask targetMask;

    [SerializeField] private float turretDetectionRadius;
    [SerializeField] private float pulseRadius;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float pulseDamage;
    private float currentCooldown;

    void Start()
    {
        thisEnemy = gameObject.transform;

        turretMask = LayerMask.NameToLayer("Unit");
        targetMask = turretMask | LayerMask.NameToLayer("Player");

        currentCooldown = 0.0f;
    }

    void PulseUpdate()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown < attackCooldown) return;
        currentCooldown -= attackCooldown;

        Collider[] potentialTargets = Physics.OverlapSphere(thisEnemy.position, turretDetectionRadius, ~turretMask);
        if (potentialTargets.Length == 0) return;

        Attack();
    }

    void Attack()
    {
        // TODO Spawn SFX

        Debug.Log("Pulse");

        Collider[] hitTargets = Physics.OverlapSphere(thisEnemy.position, turretDetectionRadius, ~targetMask);
        foreach (Collider c in hitTargets)
        {
            GameObject target = c.gameObject;
            if (target == null) continue;

            Debug.Log(target);

            HealthPoints health = target.GetComponent<HealthPoints>();
            if (health == null) continue;

            health.damageEntity(pulseDamage);
        }
    }

    void FixedUpdate()
    {
        PulseUpdate();
    }
}

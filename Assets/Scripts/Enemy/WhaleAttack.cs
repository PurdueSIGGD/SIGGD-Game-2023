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
    [SerializeField] private AudioSource pulseSound;
    [SerializeField] private MeshRenderer sphere;

    private float currentCooldown;

    void Start()
    {
        thisEnemy = gameObject.transform;

        turretMask = LayerMask.NameToLayer("Unit");
        targetMask = turretMask | LayerMask.NameToLayer("Player");

        currentCooldown = 0.0f;
        if (sphere != null )
        {

        sphere.enabled = false;
        }
    }

    void PulseUpdate()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown < attackCooldown) return;
        currentCooldown -= attackCooldown;

        bool doAttack = false;
        Collider[] potentialTargets = Physics.OverlapSphere(thisEnemy.position, turretDetectionRadius, ~turretMask);
        foreach (Collider c in potentialTargets)
        {
            GameObject target = c.gameObject;
            if (!((target.tag == "Player") || (target.tag == "Unit"))) continue;
            doAttack = true;
        }

        if (doAttack) Attack();
    }

    void Attack()
    {
        StartCoroutine(ColliderRoutine());
        Collider[] hitTargets = Physics.OverlapSphere(thisEnemy.position, pulseRadius, ~targetMask);
        foreach (Collider c in hitTargets)
        {
            GameObject target = c.gameObject;

            //Debug.Log(target.tag);
            if (target == null) continue;
            if (!((target.tag == "Player") || (target.tag == "Unit")))
            {
                continue;
            }

            HealthPoints health = target.GetComponent<HealthPoints>();
            if (health == null) continue;

            health.damageEntity(pulseDamage);
        }
        
    }

    IEnumerator ColliderRoutine()
    {
        if (sphere != null)
        {

        sphere.enabled = true;
        }
        pulseSound.Play();

        yield return new WaitForSeconds(0.2f);

        if (sphere != null)
        {

        sphere.enabled = false;
        }
        
        
        yield break;
    }

    void FixedUpdate()
    {
        PulseUpdate();
    }
}

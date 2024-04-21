using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Torpedoer : Unit
{
    // -- Serialize Field --

    [Header("Unit Fields")]

    [SerializeField]
    float fireCooldown;

    [SerializeField]
    float range;

    [SerializeField]
    GameObject bulletPoint;

    [SerializeField]
    LayerMask projMask;

    [Header("Projectile Fields")]

    [SerializeField]
    GameObject projPrefab;

    [SerializeField]
    float projDuration;

    [SerializeField]
    float projDamage;

    [SerializeField]
    float projSpeed;

    [SerializeField]
    float projDmgRadius;

    [SerializeField]
    float projKnockback;

    // -- Private Fields --
    GameObject target;
    bool canFire;


    // -- Behavior --

    protected override void Start()
    {
        base.Start();
        canFire = true;
    }

    void Update()
    {
        Aim();
        if (target != null && canFire)
        {
            Fire();
        }
    }

    void Aim()
    {
        // Look at target
        this.target = FindTarget();
        if (target != null)
        {
            this.transform.LookAt(target.transform.position);
        }
    }

    GameObject FindTarget()
    {
        // Get objects in range
        Collider[] hits = Physics.OverlapSphere(this.transform.position, range, projMask);

        // Find closest target
        float minDist = float.PositiveInfinity;
        GameObject target = null;

        foreach (Collider hit in hits)
        {
            float dist = Mathf.Abs((hit.gameObject.transform.position - this.transform.position).magnitude);
            if (dist < minDist)
            {
                minDist = dist;
                target = hit.gameObject;
            }
        }

        return target;
    }

    void Fire()
    {
        if (target != null)
        {
            var bullet = Instantiate(projPrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<Torpedo>();
            bullet.target = target;
            bullet.duration = projDuration;
            bullet.damage = projDamage;
            bullet.speed = projSpeed;
            bullet.dmgRadius = projDmgRadius;
            bullet.knockback = projKnockback;
            bullet.enemyMask = projMask;

            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}

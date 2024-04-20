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


    [Header("Projectile Fields")]

    [SerializeField]
    GameObject projPrefab;

    [SerializeField]
    float projDuration;

    [SerializeField]
    float projHeight;

    [SerializeField]
    float projDamage;

    [SerializeField]
    float projDmgRadius;

    [SerializeField]
    float projKnockback;

    [SerializeField]
    LayerMask projMask;

    // -- Private Fields --
    GameObject target;
    bool canFire;


    // -- Behavior --

    protected override void Start()
    {
        base.Start();

        // Fetch Components of Unit
        this.movement = GetComponent<Stationary>();
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

    public void Aim()
    {
        // Look at target
        this.target = FindTarget();
        if (target != null)
        {
            this.transform.LookAt(target.transform.position);
        }
    }

    public GameObject FindTarget()
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

    public void Fire()
    {
        if (target != null)
        {
            var bullet = Instantiate(projPrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<Torpedo>();
            bullet.target = target;
            bullet.duration = projDuration;
            bullet.height = projHeight;
            bullet.damage = projDamage;
            bullet.dmgRadius = projDmgRadius;
            bullet.knockback = projKnockback;
            bullet.enemyMask = projMask;

            StartCoroutine(Cooldown());
        }
    }

    public IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}

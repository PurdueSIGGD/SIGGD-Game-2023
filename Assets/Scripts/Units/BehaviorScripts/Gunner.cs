using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Unit
{
    // -- Serialize Field --

    [Header("Unit Fields")]

    [SerializeField]
    float fireCooldown;

    [SerializeField]
    float range;

    [SerializeField]
    float burstCount;

    [SerializeField]
    float burstDuration;

    [SerializeField]
    GameObject gunObj;

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

    // -- Private Variables --
    GameObject target;
    bool canFire;
    bool isBurst;
    float burstTime;

    // -- Behavior --
    void Start()
    {
        canFire = true;
        burstTime = burstDuration / burstCount;
        isBurst = false;
    }

    void Update()
    {
        Aim();
        if (target != null && canFire && !isBurst)
        {
            StartCoroutine(Burst());
        }
    }

    void Aim()
    {
        // Look at target
        this.target = FindTarget();
        if (target != null)
        {
            //this.transform.LookAt(target.transform.position);
            var dir = target.transform.position - transform.position;
            gunObj.GetComponent<DirectionalSprite>().lookDirectionOverride = dir;
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

    public void Fire()
    {
        if (target != null)
        {
            var bullet = Instantiate(projPrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.target = target.transform.position;
            bullet.duration = projDuration;
            bullet.damage = projDamage;
            bullet.speed = projSpeed;
        }
    }

    IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }

    IEnumerator Burst()
    {
        isBurst = true;
        for (int i = 0; i < burstCount; i++)
        {
            Fire();
            yield return new WaitForSeconds(burstTime);
        }
        isBurst = false;
        StartCoroutine(Cooldown());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner1 : Unit
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
    GameObject gunObj;

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

    // -- Behavior --
    protected override void Start()
    {
        base.Start();
        canFire = true;
    }

    void Update()
    {
        Debug.Log("LOL1");
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
            //bulletPoint.transform.LookAt();
            var dir = target.transform.position - transform.position;
            Debug.Log("LOL2");
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

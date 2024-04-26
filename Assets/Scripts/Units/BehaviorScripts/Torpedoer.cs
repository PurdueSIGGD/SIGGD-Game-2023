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
    int clusterCount;

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

    [SerializeField]
    float projDmgRadius;

    [SerializeField]
    float projKnockback;

    [Header("Cluster Bomb Fields")]

    [SerializeField]
    GameObject clusterPrefab;

    [SerializeField]
    float clusterDuration;

    [SerializeField]
    float clusterDamage;

    [SerializeField]
    float clusterHeight;

    [SerializeField]
    float clusterDmgRadius;

    [SerializeField]
    float clusterKnockback;


    // -- Private Fields --
    GameObject target;
    GameObject[] targets;
    bool canFire;


    // -- Behavior --

    protected override void Start()
    {
        base.Start();
        canFire = true;
        targets = new GameObject[clusterCount + 1];
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
        FindTarget();
        if (this.target != null)
        {
            //this.transform.LookAt(target.transform.position);
            var dir = target.transform.position - transform.position;
            gunObj.GetComponent<DirectionalSprite>().lookDirectionOverride = dir;
        }
    }

    void FindTarget()
    {
        // Get objects in range
        Collider[] hits = Physics.OverlapSphere(this.transform.position, range, projMask);

        // Find closest targets

        /*float minDist = float.PositiveInfinity;
        GameObject target = null;

        foreach (Collider hit in hits)
        {
            float dist = Mathf.Abs((hit.gameObject.transform.position - this.transform.position).magnitude);
            if (dist < minDist)
            {
                minDist = dist;
                target = hit.gameObject;
            }
        }*/

        if (hits.Length > 0) return;

        for (int i = 0; i < clusterCount + 1; i++) {
            float minDist = float.PositiveInfinity;
            GameObject closest = null;
            int minIdx = -1;

            for (int c = 0; c < hits.Length; c++)
            {
                Collider hit = hits[c];
                if (hit == null) continue;

                float dist = Mathf.Abs((hit.gameObject.transform.position - this.transform.position).magnitude);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = hit.gameObject;
                    minIdx = c;
                }
            }

            hits[minIdx] = null;
            targets[i] = closest;
        }

        this.target = targets[0];
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

        for (int i = 1; i < clusterCount + 1; i++)
        {
            GameObject clusterTarget = targets[i];
            if (clusterTarget == null) continue;

            var cluster = Instantiate(clusterPrefab, this.transform.position, Quaternion.identity).GetComponent<Cluster>();
            cluster.target = clusterTarget;
            cluster.duration = clusterDuration;
            cluster.damage = clusterDamage;
            cluster.height = clusterHeight;
            cluster.dmgRadius = clusterDmgRadius;
            cluster.knockback = clusterKnockback;
            cluster.enemyMask = projMask;
        }
    }

    IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
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

    void Start()
    {
        canFire = true;
        targets = new GameObject[clusterCount];
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
        List<Collider> hits = Physics.OverlapSphere(this.transform.position, range, projMask).ToList();

        // If no hits, return
        if (hits == null || hits.Count <= 0) return;

        // Set targets array
        int targetCount = (hits.Count > clusterCount) ? clusterCount : hits.Count;
        targets = new GameObject[targetCount];

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

        // Loop through hits until either no targets or cluster count exceeded
        while ((targetCount - 1) >= 0) {
            // Minimum distance of potential targets
            float minDist = float.PositiveInfinity;
            // The closest object
            GameObject closest = null;
            // Index in hits (-1 means unassigned)
            int minIdx = -1;

            // Loop through hits
            for (int c = 0; c < hits.Count; c++)
            {
                // Get the collider
                Collider hit = hits[c];

                // Get the distance
                float dist = Mathf.Abs((hit.gameObject.transform.position - this.transform.position).magnitude);
                // Replace closest object
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = hit.gameObject;
                    minIdx = c;
                }
            }

            // Remove found value, set the target to current
            hits.RemoveAt(minIdx);
            targets[targetCount - 1] = closest;
            // Decrement target count
            targetCount--;
        }

        // Set target to the first of targets array
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

        for (int i = 1; i < targets.Length; i++)
        {
            GameObject clusterTarget = targets[i];

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

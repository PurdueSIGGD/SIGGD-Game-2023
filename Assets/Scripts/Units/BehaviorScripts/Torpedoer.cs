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

    [SerializeField]
    Animator rocketAnimator;

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
    GameObject primaryTarget;
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
        // aim at primary target
        Aim();

        // fire at all targets if can fire
        if (primaryTarget != null && canFire)
        {
            Fire();
        }
    }

    void Aim()
    {
        // look at target
        FindTargets();
        if (this.primaryTarget != null)
        {
            var dir = primaryTarget.transform.position - transform.position;
            gunObj.GetComponent<DirectionalSprite>().lookDirectionOverride = dir;
        }
    }

    void FindTargets()
    {
        // get objects in range
        List<Collider> hits = Physics.OverlapSphere(this.transform.position, range, projMask).ToList();

        // if no hits, return
        if (hits == null || hits.Count <= 0) return;

        // set all targets
        for (int i = 0; i < targets.Length; i++)
        {
            GameObject closestTarget = null;
            float closestDist = float.PositiveInfinity;
            int closestIdx = -1;

            // find closest collider
            for (int c = 0; c < hits.Count; c++)
            {
                Collider hit = hits[c];
                if (hit == null) continue;

                float dist = Vector3.Distance(hit.gameObject.transform.position, this.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestTarget = hit.gameObject;
                    closestIdx = c;
                }
            }

            // set current target to closest and remove target from hits
            targets[i] = closestTarget;
            if (closestIdx >= 0) hits.RemoveAt(closestIdx);
        }

        // set primary target
        primaryTarget = targets[0];
    }

    void Fire()
    {
        // fire at primary target if not null
        if (primaryTarget != null)
        {
            // play animation
            rocketAnimator.SetTrigger("Fire");

            var bullet = Instantiate(projPrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<Torpedo>();
            bullet.target = primaryTarget;
            bullet.duration = projDuration;
            bullet.damage = projDamage;
            bullet.speed = projSpeed;
            bullet.dmgRadius = projDmgRadius;
            bullet.knockback = projKnockback;
            bullet.enemyMask = projMask;

            StartCoroutine(Cooldown());
        }

        // fire at cluster targets if not null
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

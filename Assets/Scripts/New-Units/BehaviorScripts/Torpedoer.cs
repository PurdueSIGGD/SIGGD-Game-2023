using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Stationary))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Torpedoer : Attack
{
    // -- Serialize Field --
    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float fireCooldown;

    [SerializeField]
    float height;

    [SerializeField]
    float duration;

    [SerializeField]
    GameObject bulletPoint;

    // -- Private Fields --
    GameObject target;
    bool canFire;

    // -- Override Methods --

    void Start()
    {
        // Fetch Components of Unit
        this.RB = GetComponent<Rigidbody>();
        this.Collider = GetComponent<BoxCollider>();
        this.Movement = GetComponent<Stationary>();
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
        Collider[] hits = Physics.OverlapSphere(this.transform.position, Range, mask);

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
            var bullet = Instantiate(projectilePrefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<Torpedo>();
            bullet.SetTarget(target);
            bullet.SetDuration(duration);
            bullet.SetHeight(height);

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

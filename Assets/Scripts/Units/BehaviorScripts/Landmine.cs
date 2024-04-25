using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine1 : Unit
{
    // -- Serialize Fields --

    [Header("Unit Fields")]

    [SerializeField]
    float triggerRadius;

    [SerializeField]
    float blastRadius;

    [SerializeField]
    float blastDmg;

    [SerializeField]
    float knockbackForce;

    [SerializeField]
    LayerMask blastMask;

    // -- Behavior --
    protected override void Start()
    {
        base.Start();
        SetupDetonator();
    }

    void SetupDetonator()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = triggerRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, blastRadius, blastMask);
            foreach (Collider enemy in colliders)
            {
                Vector3 explosionPoint = this.transform.position - Vector3.up;
                explosionPoint.y = 0;
                enemy.GetComponent<KinematicReset>().Knockback();
                enemy.GetComponent<Rigidbody>().AddExplosionForce(knockbackForce, explosionPoint, blastRadius, knockbackForce / 3);
                enemy.GetComponent<HealthPoints>().damageEntity(blastDmg);
            }
            Destroy(this.gameObject);
        }
    }
}

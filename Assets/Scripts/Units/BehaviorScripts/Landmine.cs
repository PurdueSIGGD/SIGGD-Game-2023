using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Unit
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
    float triggerDelay;

    [SerializeField]
    LayerMask blastMask;

    [SerializeField]
    Animator mineAnimator;

    [SerializeField]
    GameObject detonator;

    // -- Private Fields --
    bool triggered;

    // -- Behavior --
    void Start()
    {
        SetupDetonator();
    }

    void Update()
    {
        if (triggered)
        {
            AnimatorStateInfo state = mineAnimator.GetCurrentAnimatorStateInfo(0);
            if (state.normalizedTime > triggerDelay && state.IsName("Landmine1"))
            {
                Boom();
            }
        }
    }

    void SetupDetonator()
    {
        SphereCollider collider = detonator.GetComponent<SphereCollider>();
        collider.radius = triggerRadius;
        triggered = false;
    }

    public void Detonate()
    {
        mineAnimator.SetTrigger("BoomTime");
        triggered = true;
        Debug.Log("TRIGGER!");
    }

    void Boom()
    {
        Debug.Log("BOOM!");
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

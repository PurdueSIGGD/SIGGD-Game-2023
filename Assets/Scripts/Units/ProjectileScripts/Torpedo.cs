using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
//using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.EventTrigger;

public class Torpedo : MonoBehaviour
{
    // -- Fields Set by Unit --

    [NonSerialized]
    public GameObject target;

    [NonSerialized]
    public float duration;

    [NonSerialized]
    public float damage;

    [NonSerialized]
    public float speed;

    [NonSerialized]
    public float dmgRadius;

    [NonSerialized]
    public float knockback;

    [NonSerialized]
    public LayerMask enemyMask;

    // -- Private Fields --
    float time;
    Vector3 direction;

    // -- Behavior --
    void Start()
    {
        // Initialize fields
        direction = (target.transform.position - this.transform.position).normalized;
    }

    void Update()
    {
        // look at target direction
        if (target != null)
        {
            var dir = target.transform.position - transform.position;
            this.GetComponentInChildren<DirectionalSprite>().lookDirectionOverride = dir;
        }

        time += Time.deltaTime;

        // destroy gameobject after DURATION time has passed
        if (time > duration)
        {
            Destroy(this.gameObject);
        }

        // apply velocity and update position 
        Vector3 pos = this.transform.position;
        pos += direction * speed * Time.deltaTime;
        this.transform.position = pos;

        // update direction
        if (target != null)
        {
            direction = (target.transform.position - this.transform.position).normalized;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, dmgRadius, enemyMask);
        foreach (Collider enemy in colliders)
        {
            Vector3 explosionPoint = this.transform.position - Vector3.up;
            explosionPoint.y = 0;
            enemy.GetComponent<KinematicReset>().Knockback();
            enemy.GetComponent<Rigidbody>().AddExplosionForce(knockback, explosionPoint, dmgRadius, knockback / 3);
            enemy.GetComponent<HealthPoints>().damageEntity(damage);
        }
        Destroy(this.gameObject);
    }

}

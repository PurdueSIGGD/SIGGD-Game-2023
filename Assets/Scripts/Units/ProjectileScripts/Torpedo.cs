using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.PlayerSettings;
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 explosionPoint = this.transform.position - Vector3.up;
            explosionPoint.y = 0;
            other.gameObject.GetComponent<HealthPoints>().damageEntity(damage);
            other.GetComponent<KinematicReset>().Knockback();
            other.GetComponent<Rigidbody>().AddExplosionForce(knockback, explosionPoint, dmgRadius, knockback / 3);
            other.GetComponent<HealthPoints>().damageEntity(damage);
        }
        Destroy(this.gameObject);
    }

    /*
    void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.Pow(time / duration, 2);

        if (target != null)
        {
            endPos = target.transform.position;
        }
        Vector3 pos = Vector3.Lerp(startPos, endPos, t);
        pos.y = Func(t, height) + startPos.y;

        this.transform.rotation = Quaternion.LookRotation(pos - this.transform.position);

        this.transform.position = pos;

        // Set Boom On
        if (t > 0.5f)
        {
            canBoom = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBoom)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, dmgRadius, enemyMask);
            Debug.Log(colliders.Length);
            foreach (Collider enemy in colliders)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 explosionPoint = this.transform.position - Vector3.up;
                explosionPoint.y = 0;
                enemy.GetComponent<KinematicReset>().Knockback();
                enemy.GetComponent<Rigidbody>().AddExplosionForce(knockback, explosionPoint, dmgRadius, knockback / 3);
                enemy.GetComponent<HealthPoints>().damageEntity(damage);
            }
            Destroy(this.gameObject);
        }
    }

    // -- Functions --

    float Func(float time, float scale)
    {
        float y = -4 * Mathf.Pow((time - 0.5f), 2) + 1;
        return scale * y;
    }  
    */
}

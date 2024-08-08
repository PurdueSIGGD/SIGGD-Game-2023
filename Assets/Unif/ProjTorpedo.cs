using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjTorpedo : MonoBehaviour
{
    // Serialize Fields

    [SerializeField]
    GameObject target;

    [SerializeField]
    public float duration;

    [SerializeField]
    float damage;

    [SerializeField]
    float speed;

    [SerializeField]
    float dmgRadius;

    // Private Variables
    float time;
    Vector3 direction;

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
            if (other.GetComponent<KinematicReset>() != null)
            {
                other.GetComponent<KinematicReset>().Knockback();
            }
            else
            {
                Debug.LogWarning("'Other' has no KinematicReset component!");
            }
            other.GetComponent<HealthPoints>().damageEntity(damage);
        }
        Destroy(this.gameObject);
    }

}

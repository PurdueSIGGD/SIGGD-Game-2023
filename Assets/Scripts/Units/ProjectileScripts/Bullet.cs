using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // -- Fields Set by Unit --
    [NonSerialized]
    public Vector3 target;

    [NonSerialized]
    public float duration;

    [NonSerialized]
    public float damage;

    [NonSerialized]
    public float speed;

    // -- Private Fields --
    float time;
    Vector3 direction;

    // -- Behavior --
    void Start()
    {
        direction = (target - this.transform.position).normalized;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthPoints>().damageEntity(damage);
        }
        Destroy(this.gameObject);
    }
}

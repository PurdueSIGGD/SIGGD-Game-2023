using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_HitDetect : MonoBehaviour
{
    Collider hitCollider;
    void Awake()
    {
        hitCollider = GetComponent<SphereCollider>();
        hitCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.tag == "Player")
        {
            Debug.Log("Player Collided");
        }
    }
}

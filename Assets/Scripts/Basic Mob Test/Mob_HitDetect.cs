using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_HitDetect : MonoBehaviour
{
    Collider hitCollider;
    void Awake()
    {
        hitCollider = GetComponent<Collider>();
        hitCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Player Collided");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private bool includesPlayer;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(damageAmount);
            collision.gameObject.transform.GetComponent<EnemyHealth>().ProcessDamage(damageAmount);
        }
        if (includesPlayer && collision.gameObject.tag == "Player")
        {
            // TODO: process damage (or healing if damage amount is negative) for player
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Unit
{
    // -- Serialize Fields --

    [SerializeField]
    float healAmount;

    [SerializeField]
    float healCooldown;

    // -- Private Fields --
    GameObject player;
    HealthPoints playerHealth;

    // -- Override Methods --
    void Start()
    {
        // Initialize fields
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthPoints>();

        // Start Healing
        StartCoroutine(Heal());
    }


    // -- Coroutines --

    IEnumerator Heal()
    {
        while(true)
        {
            yield return new WaitForSeconds(healAmount);
            playerHealth.healEntity(healAmount);
            Debug.Log("Healed!");
        }
    }
}

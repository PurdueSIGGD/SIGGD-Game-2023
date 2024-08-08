using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Unit
{
    // -- Serialize Fields --
    [Header("Unit Fields")]

    [SerializeField]
    float healAmount;

    [SerializeField]
    float healCooldown;

    [SerializeField]
    float lightAmount;

    [SerializeField]
    float lightCooldown;

    // -- Private Fields --
    private GameObject player;

    // -- Behavior --
    void Start()
    {
        // Initialize fields
        player = GameObject.FindGameObjectWithTag("Player");

        // Start Healing
        StartCoroutine(Heal());
    }


    // -- Coroutines --

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(healCooldown);
            player.GetComponent<HealthPoints>().healEntity(healAmount);
            player.GetComponent<LightResource>().addLight(lightAmount);
        }
    }
}

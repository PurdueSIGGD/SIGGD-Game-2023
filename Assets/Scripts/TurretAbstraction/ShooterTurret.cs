using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTurret : Unit
{
    // Range
    int range = 20;

    // Player ref
    GameObject player;

    // Collider for range detection
    SphereCollider sC;

    // Is targeting
    bool isTargeting;

    // Enemy mask
    LayerMask mask;

    private void Awake()
    {
        // Add a range collider
        sC = gameObject.AddComponent<SphereCollider>();
        sC.radius = range;
        sC.isTrigger = true;

        // Find Player
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        // Mask
        mask = LayerMask.NameToLayer("Enemy") | LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        // Locate nearest enemy
        GameObject enemy = LocateClosestEnemy();

        while (TargetAlive(enemy))
        {

        }
    }

    private GameObject LocateClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Sort enemies by distance
        for (int i = 1; i < enemies.Length - 1; i++) {
            GameObject current = enemies[i];

            int j = i - 1;
            while (j >= 0 && GetDistToEnemy(enemies[j]) > GetDistToEnemy(current))
            {
                enemies[j + 1] = enemies[j];
                j--;
            }
        }

        // With sorted array, find closest free enemy
        for (int i = 0; i < enemies.Length; i++)
        {
            // Check if no hit between player and enemy
            if (!Physics.Raycast(player.transform.position, enemies[i].transform.position, out RaycastHit hit, range, ~mask))
            {
                return enemies[i];
            }
        }

        // No available enemy found, turret is inactive
        return null;
    }

    private float GetDistToEnemy(GameObject enemy)
    {
        return Vector3.Magnitude(player.transform.position - enemy.transform.position);
    }

    private bool TargetAlive(GameObject enemy)
    {
        return false;
    }

    private void Shoot(GameObject enemy)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] targets = { other.gameObject };
    }
}

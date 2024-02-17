using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roamer : UnitMovement
{
    private GameObject Player;

    public NavMeshAgent NavMesh;

    private int range;

    private bool attacking = false;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        range = 10;
    }

    private void CheckDist()
    {
        float playerDist = Vector3.Magnitude((gameObject.transform.position - Player.transform.position));
        if (playerDist > range)
        {
            if (attacking)
            {
                attacking = !attacking;
            }
            // Go to 2 units away frem the player in the proper direction
            NavMesh.SetDestination(Player.transform.position + ((gameObject.transform.position - Player.transform.position) / playerDist) * 2);
        }
    }

    private void Update()
    {
        CheckDist();
    }
}

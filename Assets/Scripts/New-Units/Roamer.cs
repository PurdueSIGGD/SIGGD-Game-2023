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
    }

    private void CheckDist()
    {
        float playerDist = Vector3.Magnitude((gameObject.transform.position - Player.transform.position));
        if (playerDist > range)
        {
            if (attacking)
            {
                // STOP!
            }
            NavMesh.SetDestination(Player.transform.position);
        }
    }

    private void Update()
    {
        CheckDist();
    }
}

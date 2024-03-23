using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roamer : UnitMovement
{
    private GameObject Player;

    //private RoamerTestAttack attack;

    private NavMeshAgent NavMesh;

    private int range;

    private bool attacking = true;

    GameObject Target = null;

    private void Awake()
    {
        
        NavMesh = gameObject.GetComponent<NavMeshAgent>();
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
                Debug.Log("Halting attack");
                attacking = !attacking;
            }
            // Go to 2 units away frem the player in the proper direction
            NavMesh.SetDestination(Player.transform.position + ((gameObject.transform.position - Player.transform.position) / playerDist) * 2);
        } else if (!attacking && playerDist <= 3)
        {
            Debug.Log("Attacking again");
            attacking = true;
        }
    }

    private void Update()
    {
        CheckDist();
        if (attacking)
        {
            if (Target == null || Vector3.Magnitude(Target.transform.position - Player.transform.position) > range)
            {
                //Debug.Log("Finding Target...");
                Target = FindTarget();
                if (Target != null)
                {
                    NavMesh.destination = Target.transform.position;
                }
            }
            else
            {
                NavMesh.destination = Target.transform.position;
                //Debug.Log(NavMesh.remainingDistance);
                if (NavMesh.remainingDistance < 1.2)
                {
                    Destroy(Target);
                }
            }
        }
    }

    private GameObject FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, range);
        if (hitColliders.Length != 0)
        {
            float dist = range + 1;
            Collider closest = null;
            foreach (Collider col in hitColliders)
            {
                //Debug.Log(col.gameObject.name);
                if (Vector3.Magnitude(col.transform.position - gameObject.transform.position) < dist && col.gameObject.tag == "Enemy")
                {
                    closest = col;
                    dist = Vector3.Magnitude(col.transform.position - gameObject.transform.position);
                }
            }
            if (closest == null)
            {
                return null;
            }
            else
            {
                return closest.gameObject;
            }
        } else
        {
            return null;
        }
    }
}

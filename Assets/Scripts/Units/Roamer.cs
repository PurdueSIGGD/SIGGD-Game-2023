using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roamer : UnitMovement
{
    // -- Serialize Fields --

    private NavMeshAgent NavMesh;

    private bool attacking = true;

    GameObject Target = null;

    // -- Behavior --
    private void Start()
    {
        // Define attack, detect, mandatory return ranges
        assignRanges(5, 10);
        // Move speed modifier
        moveSpeedModifier = 10;

        NavMesh = gameObject.GetComponent<NavMeshAgent>();
        NavMesh.speed = moveSpeedModifier;
    }

    private void CheckDist()
    {
        if (playerDist() > detectRange)
        {
            if (attacking && Target == null)
            {
                attacking = !attacking;
                //Debug.Log("Halting attack");
            }
        }
        else if (!attacking && playerDist() <= attackRange)
        {
            attacking = true;
            //Debug.Log("Resuming attack");
        }
    }

    private void Update()
    {
        CheckDist();
        if (attacking)
        {
            if (Target == null)
            {
                Target = FindTarget();
                if (Target != null)
                {
                    NavMesh.destination = Target.transform.position;
                }
            }
            else
            {
                /*NavMesh.destination = Target.transform.position;

                if (NavMesh.remainingDistance < attackRange)
                {
                    Destroy(Target);
                }*/
            }
        }
        else
        {
            // Go to 2 units away frem the player in the proper direction
            NavMesh.SetDestination(Player.transform.position);
        }
    }

    private GameObject FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, detectRange);
        if (hitColliders.Length != 0)
        {
            float dist = detectRange + 1;
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
        }
        else
        {
            return null;
        }
    }

}

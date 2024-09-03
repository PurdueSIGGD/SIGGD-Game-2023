using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
public class Roamer : UnitMovement
{
    // -- Serialize Fields --

    private NavMeshAgent NavMesh;

    private bool attacking = true;

    GameObject Target = null;

    private float playerDistanceModifier = 0f;

    private Vector2 playerAngleModifier;

    private float attackingMoveSpeedModifier;

    // -- Behavior --
    private void Start()
    {
        // Define attack, detect, mandatory return ranges
        assignRanges(20, 25);
        // Move speed modifier
        moveSpeedModifier = 10f;
        attackingMoveSpeedModifier = 8f;

        NavMesh = gameObject.GetComponent<NavMeshAgent>();
        NavMesh.speed = moveSpeedModifier;

        playerDistanceModifier = Random.Range(-1f, 4f);
        playerAngleModifier = Random.insideUnitCircle.normalized;
    }

    private void CheckDist()
    {
        if (playerDist() > detectRange)
        {
            if (attacking && Target == null)
            {
                attacking = false;
                playerDistanceModifier = Random.Range(-1f, 4f);
                playerAngleModifier = Random.insideUnitCircle.normalized;
                //Debug.Log("Halting attack");
            }
            else if (attacking && Target != null && playerDist() > detectRange * 1.5f)
            {
                attacking = false;
                playerDistanceModifier = Random.Range(-1f, 4f);
                playerAngleModifier = Random.insideUnitCircle.normalized;
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
                    //NavMesh.destination = Target.transform.position;
                    NavMesh.speed = attackingMoveSpeedModifier;
                    NavMesh.destination = calculateTargetDestination();
                }
                else
                {
                    NavMesh.speed = moveSpeedModifier;
                    NavMesh.SetDestination(calculatePlayerDestination());
                }
            }
            else
            {
                NavMesh.speed = attackingMoveSpeedModifier;
                NavMesh.destination = calculateTargetDestination();
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
            //NavMesh.SetDestination(Player.transform.position);
            NavMesh.speed = moveSpeedModifier;
            NavMesh.SetDestination(calculatePlayerDestination());
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



    private Vector3 calculatePlayerDestination()
    {
        //Vector3 difference = Vector3.Normalize(transform.position - Player.transform.position);

        //return Quaternion.AngleAxis(-1 * playerAngleModifier, Player.transform.position) * (Player.transform.position + (difference * (7f + playerDistanceModifier)));

        //Vector2 direction2 = Random.insideUnitCircle.normalized;
        Vector3 direction3 = new Vector3(playerAngleModifier.x, 0f, playerAngleModifier.y);

        return Player.transform.position + (direction3 * (7f + playerDistanceModifier));

        //return Player.transform.position + (difference * (7f + playerDistanceModifier));
    }

    private Vector3 calculateTargetDestination()
    {
        Vector3 difference = Vector3.Normalize(transform.position - Target.transform.position);
        return Target.transform.position + (difference * 11f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamerMovement : UnitMovement
{
    // Define attack, detect, mandatory return ranges
    /*
        Roamer will attack any enemy in the detect range and deal damage when 
        within the attack range. If farther than mandatory return range will
        return no matter what
    */
    protected override int detectRange => 10;
    protected override int attackRange => 3;
    private int mandatoryReturnRange = 15;

    // Move speed modifier
    protected override float moveSpeedModifier => 10;

    [HideInInspector]
    public UnitAttack attack;

    private NavMeshAgent NavMesh;

    private bool attacking = true;

    GameObject Target = null;

    private void Start()
    {
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
        } else if (!attacking && playerDist() <= attackRange)
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
                
                if (NavMesh.remainingDistance < attackRange)
                {
                    Destroy(Target);
                }
            }
        } else {
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
        } else
        {
            return null;
        }
    }
}

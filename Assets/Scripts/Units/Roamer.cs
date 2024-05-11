using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
public class Roamer : UnitMovement
{
    // -- Serialize Fields --

    [SerializeField]
    float attackRange;

    [SerializeField]
    float detectRange;

    [SerializeField]
    float moveSpeedModifier;

    [SerializeField]
    Animator roamerAnimator;

    // -- Private Fields --
    private NavMeshAgent NavMesh;
    private bool attacking = true;
    GameObject Target = null;
    GameObject player;

    Vector3 prevPos;
    bool moving;

    // -- Behavior --
    private void Start()
    {
        NavMesh = gameObject.GetComponent<NavMeshAgent>();
        NavMesh.speed = moveSpeedModifier;
        player = GameObject.FindGameObjectWithTag("Player");
        prevPos = player.transform.position;
        moving = true;
    }

    private void CheckDist()
    {
        float playerDist = Vector3.Distance(player.transform.position, this.transform.position);

        if (playerDist > detectRange)
        {
            if (attacking && Target == null)
            {
                attacking = !attacking;
                //Debug.Log("Halting attack");
            }
        }
        else if (!attacking && playerDist <= attackRange)
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
        }
        else
        {
            // Go to 2 units away frem the player in the proper direction
            NavMesh.SetDestination(player.transform.position);
        }

        // check if still moving
        bool before = moving;
        moving = !(Vector3.Distance(this.transform.position, prevPos) <= 0);
        prevPos = this.transform.position;

        if (before && !moving)
        {
            EnterIdle();
        } else if (!before && moving)
        {
            EnterMoving();
        }
    }

    void EnterIdle()
    {
        roamerAnimator.SetBool("IsIdle", true);
        //Debug.Log("Idling!!!!");
    }

    void EnterMoving()
    {
        roamerAnimator.SetBool("IsIdle", false);
        //Debug.Log("Moving!!!!");
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

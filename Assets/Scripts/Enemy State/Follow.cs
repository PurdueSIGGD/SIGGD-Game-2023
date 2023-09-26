using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : EnemyState
{
    private GameObject player;
    private Transform gameObjectTransform;
    private UnityEngine.AI.NavMeshAgent agent;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
        controller = this.GetComponentInParent<EnemyStateController>();
        gameObjectTransform = gameObject.transform;
    }

    public override void StateStart()
    {
        Debug.Log("Follow Start");
        agent.SetDestination(player.transform.position);
    }

    public override void StateStop()
    {
        Debug.Log("Follow Stop");
        agent.SetDestination(gameObjectTransform.position);
    }

    public override void StateUpdate()
    {
        agent.SetDestination(player.transform.position);
    }

    public override void StateTick()
    {
        Debug.Log("Follow Tick");
        if (Vector3.Distance(gameObjectTransform.position, player.transform.position) > 15)
        {
            controller.SwitchState(controller.idleState);
        }
    }
}

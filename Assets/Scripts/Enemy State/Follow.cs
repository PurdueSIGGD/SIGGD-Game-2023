using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : EnemyState
{
    private GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
    }

    public override void StateStart()
    {
        Debug.Log("Follow Start");
        agent.SetDestination(player.transform.position);
    }

    public override void StateStop()
    {
        Debug.Log("Follow Stop");
    }

    public override void StateUpdate()
    {
        agent.SetDestination(player.transform.position);
    }

    public override void StateTick()
    {
        Debug.Log("Follow Tick");
    }
}

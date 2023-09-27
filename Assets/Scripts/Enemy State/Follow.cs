using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : EnemyState
{
    private GameObject player;
    private Transform gameObjectTransform;
    private UnityEngine.AI.NavMeshAgent agent;

    private Dictionary<EnemyState, int> weightTable;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
        controller = this.GetComponentInParent<EnemyStateController>();
        gameObjectTransform = gameObject.transform;

        weightTable = new Dictionary<EnemyState, int>()
        {
            {controller.followState, 500 },
            {controller.attack1, 10 },
            {controller.attack2, 10 }
        };
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
        //Debug.Log("Follow Tick");
        if (Vector3.Distance(gameObjectTransform.position, player.transform.position) > 15) return;

        EnemyState nextState = controller.idleState;
        int weightSum = 0;
        foreach (KeyValuePair<EnemyState, int> state in weightTable)
        {
            weightSum += state.Value;
        }
        int randomWeight = UnityEngine.Random.Range(0, weightSum);
        foreach(KeyValuePair<EnemyState, int> state in weightTable)
        {
            randomWeight -= state.Value;
            if (randomWeight < 0)
            {
                nextState = state.Key;
                break;
            }
        }
        if (nextState == controller.followState) return;
        controller.SwitchState(nextState);
    }
}

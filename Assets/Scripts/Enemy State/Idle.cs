using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : EnemyState
{
    private GameObject player;
    private Transform gameObjectTransform;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        controller = this.GetComponentInParent<EnemyStateController>();
        gameObjectTransform = gameObject.transform;
    }
    public override void StateStart()
    {
        Debug.Log("Idle Start");
    }

    public override void StateStop()
    {
        Debug.Log("Idle Stop");
    }

    public override void StateUpdate()
    {

    }

    public override void StateTick()
    {
        Debug.Log("Idle Tick");
        if (Vector3.Distance(gameObjectTransform.position, player.transform.position) < 10)
        {
            controller.SwitchState(controller.followState);
        }
    }
}

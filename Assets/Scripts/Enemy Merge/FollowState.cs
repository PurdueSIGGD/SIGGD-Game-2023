using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : EnemyState
{
    private GameObject player;
    private TestEnemyController controller;
    private EnemyState idleState;
    private NavScript_Mob nav;

    void Awake()
    {
        controller = gameObject.GetComponentInParent<TestEnemyController>();
        player = controller.Player;
        idleState = controller.idleState;
        nav = gameObject.GetComponentInParent<NavScript_Mob>();
        Debug.Log("Player");
    }

    public override void StateStart()
    {
        nav.enabled = true;
        Debug.Log("Folllow Start!");

    }
    public override void StateStop()
    {

    }
    public override void StateUpdate()
    {

    }
    public override bool StateTick()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distance > 10)
        {
            controller.weightTable[idleState] = 10.0f;
            controller.weightTable[this] = 0.0f;
            return true;
        }
        return false;
    }
}

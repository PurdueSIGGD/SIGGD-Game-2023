using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    private GameObject player;
    private TestEnemyController controller;
    private EnemyState followState;
    private NavScript_Mob nav;

    void Awake()
    {
        controller = gameObject.GetComponentInParent<TestEnemyController>();
        player = controller.Player;
        followState = controller.followState;
        nav = gameObject.GetComponentInParent<NavScript_Mob>();
        Debug.Log("Player");
    }   

    public override void StateStart()
    {
        nav.enabled = false;
        Debug.Log("Idle Start!");

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
        if (distance < 5)
        {
            controller.weightTable[followState] = 10.0f;
            controller.weightTable[this] = 0.0f;
            return true;
        }
        return false;
    }
}

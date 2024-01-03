using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_FollowState : EnemyState
{
    public GameObject enemy;
    private GameObject player;

    private Mob_EnemyController controller;
    private Mob_NavigationController navController;

    private EnemyState followState;
    private Mob_AttackState attackState;

    void Awake()
    {
        controller = enemy.GetComponent<Mob_EnemyController>();
        navController = enemy.GetComponent<Mob_NavigationController>();

        player = controller.player;

        followState = controller.followState;
        attackState = controller.attackState;
    }

    public override void StateStart()
    {
        Debug.Log("Follow Start");
        navController.active = true;
    }
    public override void StateStop()
    {

    }
    public override void StateUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

        if (distance < 1 && attackState.debounce == false) controller.SwitchState(controller.attackState);
        if (distance > 25) controller.SwitchState(controller.idleState);
    }
    public override bool StateTick()
    {
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_IdleState : EnemyState
{
    public GameObject enemy;
    private GameObject player;
    
    private Mob_EnemyController controller;
    private Mob_NavigationController navController;

    private EnemyState followState;
    private EnemyState attackState;

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
        navController.active = false;
    }
    public override void StateStop()
    {

    }
    public override void StateUpdate()
    {

    }
    public override bool StateTick()
    {
        return false;
    }
}
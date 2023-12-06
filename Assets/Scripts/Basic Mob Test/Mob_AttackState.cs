using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_AttackState : EnemyState
{
    public GameObject enemy;
    private GameObject player;

    private Mob_EnemyController controller;
    private Mob_NavigationController navController;

    private EnemyState idleState;
    private EnemyState followState;
    private EnemyState attackState;

    private Collider hitCollider;
    void Awake()
    {
        controller = enemy.GetComponent<Mob_EnemyController>();
        navController = enemy.GetComponent<Mob_NavigationController>();

        player = controller.player;

        idleState = controller.idleState;
        followState = controller.followState;
        attackState = controller.attackState;
    
        hitCollider = gameObject.transform.Find("HitBox").gameObject.GetComponent<Collider>();
    }

    public override void StateStart()
    {
        StartCoroutine(AttackTask());
    }

    private IEnumerator AttackTask()
    {
        navController.active = false;
        hitCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        controller.SwitchState(idleState);
    }

    public override void StateStop()
    {
        hitCollider.enabled = false;
        StopCoroutine(AttackTask());
    }
    public override void StateUpdate()
    {

    }
    public override bool StateTick()
    {
        return false;
    }
}
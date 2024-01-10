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

    public bool debounce = false;

    [SerializeField] private Collider hitCollider;
    void Awake()
    {
        controller = enemy.GetComponent<Mob_EnemyController>();
        navController = enemy.GetComponent<Mob_NavigationController>();

        player = controller.player;

        idleState = controller.idleState;
        followState = controller.followState;
        attackState = controller.attackState;
    }

    public override void StateStart()
    {
        StartCoroutine(AttackTask());
    }

    private IEnumerator AttackTask()
    {
        navController.active = false;
        hitCollider.enabled = true;
        yield return new WaitForSeconds(0.0f);
        StartCoroutine(StartDebounce());
        controller.SwitchState(followState);
    }

    private IEnumerator StartDebounce()
    {
        debounce = true;
        navController.backoff = true;
        yield return new WaitForSeconds(1.0f);
        navController.backoff = false;
        debounce = false;
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
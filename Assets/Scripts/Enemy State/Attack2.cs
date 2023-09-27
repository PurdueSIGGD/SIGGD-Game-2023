using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : EnemyState
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
        Debug.Log("Attack2 Start");
        StartCoroutine(AttackTask());
    }

    public override void StateStop()
    {
        Debug.Log("Attack2 Stop");
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateTick()
    {
        //Debug.Log("Attack2 Tick");
    }

    IEnumerator AttackTask()
    {
        yield return new WaitForSeconds(1.0f);
        EnemyState nextState;
        if (Vector3.Distance(gameObjectTransform.position, player.transform.position) < 15)
        {
            nextState = controller.followState;
        }
        else
        {
            nextState = controller.idleState;
        }
        controller.SwitchState(nextState);
    }
}

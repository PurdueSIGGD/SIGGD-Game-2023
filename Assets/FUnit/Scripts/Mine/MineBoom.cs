using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBoom : UnitState
{
    public override void EnterState(Unit context)
    {
        MineFSM mine = (MineFSM)context;
        mine.config.animator.SetTrigger("Boom");
        context.StartCoroutine(Boom(context, 0.475f));
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }

    public override void UpdateState(Unit context)
    {
        return;
    }

    IEnumerator Boom(Unit context, float wait)
    {
        yield return new WaitForSeconds(wait * 0.3f);

        MineFSM mine = (MineFSM)context;

        Collider[] colliders = Physics.OverlapSphere(mine.gameObject.transform.position, mine.config.blastRadius, mine.config.blastMask);
        foreach (Collider enemy in colliders)
        {
            enemy.GetComponent<HealthPoints>().damageEntity(mine.config.blastDmg);
        }
        Debug.Log("Time to destroy");
        yield return new WaitForSeconds(wait * 0.7f);
        Object.Destroy(mine.gameObject);
    }
}

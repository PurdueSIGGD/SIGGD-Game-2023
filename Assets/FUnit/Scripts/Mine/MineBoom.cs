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
        MineFSM mine = (MineFSM)context;
        mine.gameObject.GetComponent<bombKillCheck>().killBomb(wait);
        mine.gameObject.GetComponent<Collider>().enabled = false;
        mine.config.blastSound.Play();
        yield return new WaitForSeconds(wait * 0.3f);
        Collider[] colliders = Physics.OverlapSphere(mine.gameObject.transform.position, mine.config.blastRadius, mine.config.blastMask);
        foreach (Collider enemy in colliders)
        {
            if (enemy.gameObject.GetComponent<HealthPoints>() != null)
            {
                enemy.gameObject.GetComponent<HealthPoints>().damageEntity(mine.config.blastDmg);
            }
        }
        Debug.Log("Time to destroy");
        yield return new WaitForSeconds(wait * 0.7f);
        //mine.config.animator.StopPlayback();
        //mine.config.animator.enabled = false;
        Object.Destroy(mine.gameObject);
    }
}

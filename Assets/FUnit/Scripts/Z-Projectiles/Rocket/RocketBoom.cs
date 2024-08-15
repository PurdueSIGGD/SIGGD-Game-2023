using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoom : ProjState
{
    public override void EnterState(MonoBehaviour context)
    {
        RocketPFSM rocket = (RocketPFSM)context;
        GameObject obj = rocket.gameObject;
        Animator animator = rocket.config.animator;
        float wait = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        // Boom
        Collider[] hits = Physics.OverlapSphere(obj.transform.position, rocket.config.dmgRadius, rocket.config.layerMask);
        foreach (Collider hit in hits)
        {
            hit.gameObject.GetComponent<HealthPoints>().damageEntity(rocket.config.damage);
        }

        rocket.StartCoroutine(Delete(wait, context));
        animator.SetTrigger("Boom");
    }

    public override void UpdateState(MonoBehaviour context)
    {
        return;
    }

    public override void OnTriggerEnter(MonoBehaviour context, Collider collider)
    {
        return;
    }

    // Private

    IEnumerator Delete(float wait, MonoBehaviour context)
    {
        yield return new WaitForSeconds(wait);
        Object.Destroy(context.gameObject);
    }

}

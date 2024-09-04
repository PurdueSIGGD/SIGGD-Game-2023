using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class RocketBoom : ProjState
{
    public override void EnterState(MonoBehaviour context)
    {
        RocketPFSM rocket = (RocketPFSM)context;
        GameObject obj = rocket.gameObject;
        Animator animator = rocket.config.animator;
        float wait = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        animator.SetTrigger("Boom");
        rocket.config.blastSound.Play();
        GameObject.Destroy(rocket.gameObject, 0.75f);

        var dirComp = rocket.config.dirSprite.GetComponent<DirectionalSprite>();
        dirComp.lookDirectionOverride = Vector3.up;
        dirComp.rotationOffset = 0;
        rocket.transform.localScale = new Vector3(4, 4, 4);

        // Boom
        Collider[] hits = Physics.OverlapSphere(obj.transform.position, rocket.config.dmgRadius, rocket.config.layerMask);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.GetComponent<HealthPoints>() != null)
            {
                hit.gameObject.GetComponent<HealthPoints>().damageEntity(rocket.config.damage);
            }
        }

        //rocket.StartCoroutine(Delete(context, wait));
        //rocket.StartCoroutine(Delete(rocket, 0.75f));
        //GameObject.Destroy(rocket.gameObject, 0.75f);
        /*
        animator.SetTrigger("Boom");
        rocket.config.blastSound.Play();
        GameObject.Destroy(rocket.gameObject, 0.75f);
        */
    }

    public override void UpdateState(MonoBehaviour context)
    {
        // destroy if flying for too long
        RocketPFSM rocket = (RocketPFSM)context;
        if (rocket.personal.time > rocket.config.duration)
        {
            Object.Destroy(rocket.gameObject);
        }
        return;
    }

    public override void OnTriggerEnter(MonoBehaviour context, Collider collider)
    {
        return;
    }

    // Private

    IEnumerator Delete(MonoBehaviour context, float wait)
    {
        yield return new WaitForSeconds(wait);
        Object.Destroy(context.gameObject);
    }

}

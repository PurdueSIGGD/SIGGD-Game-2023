using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFly : ProjState
{
    public override void EnterState(MonoBehaviour context)
    {
        RocketPFSM rocket = (RocketPFSM)context;
        GameObject target = rocket.config.target;
        GameObject obj = rocket.gameObject;

        rocket.personal.time = 0;
        rocket.personal.direction = (target.transform.position - obj.transform.position).normalized;
        rocket.config.animator.ResetTrigger("Boom");
    }

    public override void UpdateState(MonoBehaviour context)
    {
        RocketPFSM rocket = (RocketPFSM)context;
        GameObject target = rocket.config.target;
        GameObject obj = rocket.gameObject;

        rocket.personal.time += Time.deltaTime;


        // update position
        Vector3 currPosition = obj.transform.position;
        currPosition += rocket.personal.direction * rocket.config.speed * Time.deltaTime;
        obj.transform.position = currPosition;


        // destroy if flying for too long
        if (rocket.personal.time > rocket.config.duration)
        {
            Object.Destroy(rocket.gameObject);
        }

        // update direction if target still exists
        if (rocket.config.target != null)
        {
            rocket.personal.direction = (target.transform.position - obj.transform.position).normalized;
        }
    }

    public override void OnTriggerEnter(MonoBehaviour context, Collider collider)
    {
        RocketPFSM rocket = (RocketPFSM)context;
        rocket.SwitchState(RocketPFSM.boomState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RocketeerFire : UnitState
{
    public override void EnterState(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;
        
        rocketeerContext.animator.SetTrigger("Fire");
        rocketeerContext.fireTime = rocketeerContext.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Debug.Log("ROCKET FIRED!!!");
    }

    public override void UpdateState(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;

        if (rocketeerContext.target != null)
        {
            var dir = rocketeerContext.target.transform.position - context.gameObject.transform.position;
            rocketeerContext.gunObj.GetComponent<DirectionalSprite>().lookDirectionOverride = dir;
        }

        if (rocketeerContext.fireTime == 0)
        {
            if (rocketeerContext.projPrefab != null)
            {
                var bullet = Object.Instantiate(rocketeerContext.projPrefab, rocketeerContext.bulletPoint.transform.position, Quaternion.identity).GetComponent<RocketPFSM>();
                bullet.personal.target = rocketeerContext.target;
            }
            rocketeerContext.SwitchState(rocketeerContext.idleState);
        }
    }
}

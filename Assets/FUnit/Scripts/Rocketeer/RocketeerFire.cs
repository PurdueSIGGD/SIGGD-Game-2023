using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RocketeerFire : UnitState
{
    public override void EnterState(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;
        var bullet = Object.Instantiate(rocketeerContext.projPrefab, rocketeerContext.bulletPoint.transform.position, Quaternion.identity).GetComponent<Torpedo>();
        bullet.target = rocketeerContext.target;

        rocketeerContext.fireTime = rocketeerContext.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public override void UpdateState(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;
        if (rocketeerContext.fireTime == 0)
        {
            rocketeerContext.SwitchState(rocketeerContext.idleState);
        }
    }
}

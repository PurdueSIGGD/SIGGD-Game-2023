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

        rocketeerContext.cooldown = rocketeerContext.fireCooldown;
    }

    public override void UpdateState(Unit context)
    {
        return;
    }
}

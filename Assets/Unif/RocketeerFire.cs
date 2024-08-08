using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RocketeerFire : UnifState
{
    public override void EnterState(Unif context)
    {
        Rocketeer rocketeerContext = (Rocketeer)context;

        var bullet = Object.Instantiate(rocketeerContext.projPrefab, rocketeerContext.bulletPoint.transform.position, Quaternion.identity).GetComponent<Torpedo>();
        bullet.target = rocketeerContext.target;
    }

    public override void UpdateState(Unif context)
    {
        return;
    }
}

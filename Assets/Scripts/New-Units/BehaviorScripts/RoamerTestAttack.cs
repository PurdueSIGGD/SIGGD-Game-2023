using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerTestAttack : Attack
{
    public override GameObject FindTarget()
    {
        return null;
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
    }

    public override void Aim()
    {
        
    }

    public override void Fire()
    {
        
    }
}

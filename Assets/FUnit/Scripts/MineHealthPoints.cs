using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHealthPoints : HealthPoints
{
    public override float damageEntity(float damage)
    {
        return base.damageEntity(damage);
    }

}

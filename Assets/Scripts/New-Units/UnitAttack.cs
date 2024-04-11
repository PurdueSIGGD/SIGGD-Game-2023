using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAttack : MonoBehaviour
{
    protected abstract float damage { get; }

    public void DealDamage(HealthPoints target)
    {
        target.damageEntity(damage);
    }
}

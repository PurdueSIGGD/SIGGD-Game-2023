using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenMelee : EnemyAttackController
{
    public override void Damg(HealthPoints point, float dmg)
    {
        this.gameObject.transform.parent.gameObject.GetComponent<SirenAttack>().ShutItDown();  //shuts down grab attack when siren melees
        point.damageEntity(dmg);
    }
}

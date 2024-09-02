using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerFire : UnitState
{
    public override void EnterState(Unit context)
    {
        GunnerFSM gunner = (GunnerFSM)context;
        //gunner.config.animator.SetBool("Fire", true);
        //Debug.Log("GUN: " + gunner.config.animator.GetBool("Fire"));
        gunner.StartCoroutine(Burst(context, gunner.config.burstCount));
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }

    public override void UpdateState(Unit context)
    {
        return;
    }

    IEnumerator Burst(Unit context, int burstCount)
    {
        GunnerFSM gunner = (GunnerFSM)context;
        GameObject prefab = gunner.config.projPrefab;
        GameObject bulletPoint = gunner.config.bulletPoint;

        for (int i = 1; i <= burstCount; i++)
        {
            if (prefab != null)
            {
                var bullet = Object.Instantiate(prefab, bulletPoint.transform.position, Quaternion.identity).GetComponent<BulletPFSM>();
                bullet.personal.target = gunner.personal.target;
            }

            yield return new WaitForSeconds(gunner.config.burstDuration / burstCount);
        }
        gunner.SwitchState(GunnerFSM.idleState);
    }
}

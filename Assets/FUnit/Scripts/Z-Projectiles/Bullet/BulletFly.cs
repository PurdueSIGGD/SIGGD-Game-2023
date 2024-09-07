using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : ProjState
{
    public override void EnterState(MonoBehaviour context)
    {
        BulletPFSM bullet = (BulletPFSM)context;
        GameObject target = bullet.personal.target;
        GameObject obj = bullet.gameObject;

        bullet.personal.time = 0;
        bullet.personal.direction = (target.transform.position - obj.transform.position).normalized;
    }

    public override void OnTriggerEnter(MonoBehaviour context, Collider collider)
    {
        BulletPFSM bullet = (BulletPFSM)context;
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collider.gameObject.GetComponent<HealthPoints>().damageEntity(bullet.config.damage);
            Object.Destroy(bullet.gameObject);
        }
    }

    public override void UpdateState(MonoBehaviour context)
    {
        BulletPFSM bullet = (BulletPFSM)context;
        GameObject target = bullet.personal.target;
        GameObject obj = bullet.gameObject;

        bullet.personal.time += Time.deltaTime;


        // update position
        bullet.config.dirSprite.GetComponent<DirectionalSprite>().lookDirectionOverride = bullet.personal.direction;

        Vector3 currPosition = obj.transform.position;
        currPosition += bullet.personal.direction * bullet.config.speed * Time.deltaTime;
        obj.transform.position = currPosition;


        // destroy if flying for too long
        if (bullet.personal.time > bullet.config.duration)
        {
            Object.Destroy(bullet.gameObject);
        }

        // update direction if target still exists
        if (bullet.personal.target != null)
        {
            bullet.personal.direction = (target.transform.position - obj.transform.position).normalized;
        }
    }
}

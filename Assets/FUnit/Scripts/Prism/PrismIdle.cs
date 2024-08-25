using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        return;
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        PrismFSM prism = (PrismFSM)context;
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            Object.Destroy(other.gameObject);

            float angleBetweenSplits = prism.config.angleBetweenSplits;
            float splitLife = prism.config.splitLife;
            int splitCount = prism.config.splitCount;

            float wingAngle = 0;

            if (splitCount % 2 == 0)
            {
                wingAngle = angleBetweenSplits * (splitCount / 2 - 1) + angleBetweenSplits / 2;
            }
            else
            {
                wingAngle = angleBetweenSplits * (splitCount / 2);
            }

            Vector3 masterVelocity = Quaternion.AngleAxis(-1 * wingAngle, Vector3.up) * velocity;

            // generate splits
            for (int i = 0; i < splitCount; i++)
            {
                GameObject split = Object.Instantiate(prism.config.splitProjPrefab, prism.transform.position + velocity.normalized, Quaternion.identity);
                split.GetComponent<Rigidbody>().velocity = masterVelocity;

                if (splitLife > 0)
                {
                    Object.Destroy(split, splitLife);
                }

                masterVelocity = Quaternion.AngleAxis(angleBetweenSplits, Vector3.up) * masterVelocity;
            }
        }
    }

    public override void UpdateState(Unit context)
    {
        return;
    }
}

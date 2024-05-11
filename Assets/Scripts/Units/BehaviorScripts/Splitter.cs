using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Unit
{
    // -- Serialize Fields --

    [SerializeField]
    GameObject splitProjPrefab;

    [SerializeField]
    int splitCount;

    [SerializeField]
    float splitLife;

    [SerializeField]
    float angleBetweenSplits;

    // -- Behavior --

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            // TODO: Split!
            // get original velocity
            Vector3 velocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            Debug.Log("Direction: " + other.gameObject.GetComponent<Rigidbody>().velocity);

            // destroy old prefab
            Destroy(other.gameObject);

            // calculate new directoin and left wing
            float wingAngle = 0;

            if (splitCount % 2 == 0)
            {
                wingAngle = angleBetweenSplits * (splitCount/ 2 - 1) + angleBetweenSplits/2;
            } else
            {
                wingAngle = angleBetweenSplits * (splitCount / 2);
            }

            Vector3 masterVelocity = Quaternion.AngleAxis(-1 * wingAngle, Vector3.up) * velocity;

            // generate splits
            for (int i = 0; i < splitCount; i++)
            {
                GameObject split = Instantiate(splitProjPrefab, this.transform.position + velocity.normalized, Quaternion.identity);
                split.GetComponent<Rigidbody>().velocity = masterVelocity;

                if (splitLife > 0)
                {
                    Destroy(split, splitLife);
                }

                masterVelocity = Quaternion.AngleAxis(angleBetweenSplits, Vector3.up) * masterVelocity;
            }
        }
    }
}

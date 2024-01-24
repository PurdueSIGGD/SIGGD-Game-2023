using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUnitAim : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float lerpSpeed = 5.0f;

    private List<GameObject> targets;
    private SphereCollider range;

    // Start is called before the first frame update
    void Start()
    {
        // Add Sphere Collider
        range = this.gameObject.AddComponent<SphereCollider>();
        range.isTrigger= true;
        range.radius = radius;
        targets = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy" && !targets.Contains(collider.gameObject))
        {
            //Debug.Log(collider.gameObject.tag);
            targets.Add(collider.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (targets.Contains(collider.gameObject))
        {
            targets.Remove(collider.gameObject);
            //Debug.Log("se fue xd");
        }
    }

    void Update()
    {
        GameObject target = null;
        float smallerDist = float.PositiveInfinity;
        if (targets.Count > 0)
        {
            foreach (GameObject enemy in targets)
            {
                float dist = (transform.position - enemy.transform.position).sqrMagnitude;
                if (dist < smallerDist)
                {
                    target = enemy;
                    smallerDist = dist;
                }
            }
        }

        Quaternion rotation = this.transform.rotation;

        if (target != null)
        {
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lerpSpeed);

    }
}

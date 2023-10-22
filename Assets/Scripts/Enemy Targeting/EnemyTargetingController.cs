using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyTargeting : MonoBehaviour
{
    private struct _weight
    {
        float distanceWeight;
        float 
    }

    public Dictionary<GameObject, _weight> targetWeights;

    public GameObject currentTarget;
    public GameObject nextTarget;
    public bool canSwitch;

    public float aggroRange;

    void Awake()
    {

    }

    void Start()
    {
        canSwitch = true;
    }

    void Update()
    {

    }

    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            // Calculate weights (In final implementation this probably should be implemented by subclasses rather than parent class
            // so different enemies exhibit different behaviour)

            // Portion of target weight based on proximity.
            // Ranges from 0 < x < 100 normalized for aggro range, (Percent of aggro distance from enemy)
            foreach (KeyValuePair<GameObject, float> targetDistance in targetDistances) 
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, targetDistance.Key.transform.position);
                if (distance > aggroRange) continue;

                targetDistance.Value = 100f * (aggroRange - distance) / aggroRange;
            }



            float maxPriority = 0;
            float currentWeight;
            foreach (GameObject target in targets)
            {
 
            }
        }
    }
}
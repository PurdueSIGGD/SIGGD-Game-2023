using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyTargeting : MonoBehaviour
{
    public Dictionary<GameObject, _weight> targetWeights;
    public struct _weight
    {
        public float proximityWeight;  // Portion of weight determined by distance to object.
                                       // Ranges from 0.0-1.0f based on normalized range from player.
                                       // 0.0f if exceeds aggroRange.

        public float visibilityFactor; // On every tick, determine if target is currently visible(raycast). If visible, refresh this value to 1.0f.
                                       // For every tick that target is not visible, decay this value by visibilityDecayRate.
                                       // If visibilityFactor reaches 0, do not select the target
    }

    public GameObject currentTarget;
    public GameObject nextTarget;

    public float aggroRange;
    public float visibilityDecayRate;

    public bool canSwitch;

    public virtual void AddTarget(GameObject newTarget)
    {
        _weight newTargetWeight = new _weight();
        
        newTargetWeight.proximityWeight = 0.0f;
        newTargetWeight.visibilityFactor = 0.0f;

        targetWeights.Add(newTarget, newTargetWeight);
    }
    public virtual void RemoveTarget(GameObject target)
    {
        targetWeights.Remove(target);
        UpdateWeights();
        UpdateTarget();
    }
    public virtual void UpdateWeights()
    {
        float distance;
        _weight targetWeight;
        foreach (KeyValuePair<GameObject, _weight> targetWeightPair in targetWeights)
        {
            targetWeight = targetWeightPair.Value;

            if (true) // In complete code this will check for visibility (e.g. raycast to target)
            {
                targetWeight.visibilityFactor = 1.0f;
            }
            else
            {
                targetWeight.visibilityFactor -= visibilityDecayRate;
                if (targetWeight.visibilityFactor < 0.0f) targetWeight.visibilityFactor = 0.0f;
            }

            distance = Vector3.Distance(gameObject.transform.position, targetWeightPair.Key.transform.position);
            if (distance < aggroRange)
            {
                targetWeight.proximityWeight = (aggroRange - distance) / aggroRange;
            }
            else
            {
                targetWeight.proximityWeight = 0.0f;
            }
        }
    }
    public virtual void UpdateTarget()
    {
        float maxWeight = 0.0f;
        foreach (KeyValuePair<GameObject, _weight> targetWeightPair in targetWeights)
        {
            if (targetWeightPair.Value.visibilityFactor <= 0.0f) continue;
            if (targetWeightPair.Value.proximityWeight > maxWeight)
            {
                nextTarget = targetWeightPair.Key;
                maxWeight = targetWeightPair.Value.proximityWeight;
            }
        }
        if (canSwitch) currentTarget = nextTarget;
    }
    IEnumerator Tick()
    {
        UpdateWeights();
        UpdateTarget();
        yield return new WaitForSeconds(0.25f);
    }
}
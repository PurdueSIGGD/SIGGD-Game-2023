using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobTargetingController : MonoBehaviour
{
    public GameObject target;
    public float distanceToTarget;

    protected const float MIN_VISIBILITY_FACTOR = 0f;
    protected const float MAX_VISIBILITY_FACTOR = 100f;

    protected const float MIN_AGGRESSION_FACTOR = 0f;
    protected const float MAX_AGGRESSION_FACTOR = 100f;
    
    protected class TargetWeight
    {
        public TargetWeight()
        {
            proximity = 0;
            visibilityFactor = 0;
            aggressionFactor = 0;
        }

        public float proximity;
        public float visibilityFactor; // 0 - 100
        public float aggressionFactor; // 0 - 100
    }

    [SerializeField] private float aggroRange;
    [SerializeField] private float visibilityDecayRate;
    [SerializeField] private float aggressionDecayRate;

    private Dictionary<GameObject, TargetWeight> targetWeightDict;
    private LayerMask enemyMask;

    private Transform selfTransform;

    void Awake()
    {
        targetChanged = new UnityEvent();
        targetWeightDict = new Dictionary<GameObject, TargetWeight>();
        enemyMask = LayerMask.GetMask("Enemy");
        distanceToTarget = -1;

        selfTransform = GetComponent<Transform>();
    }

    private const double TICK_LENGTH = 1.0;
    private double tickTimer;
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_LENGTH)
        {
            tickTimer -= TICK_LENGTH;
            UpdateWeights();
            UpdateTarget();
        }

        if (target != null)
        {
            distanceToTarget = Vector3.Distance(selfTransform.position, target.transform.position);

        } else
        {
            distanceToTarget = -1;
        }
    }

    private void UpdateWeights()
    {
        foreach (KeyValuePair<GameObject, TargetWeight> targetWeightPair in targetWeightDict)
        {
            TargetWeight currentWeight = targetWeightPair.Value;
            GameObject currentTarget = targetWeightPair.Key;

            Transform targetTransform = currentTarget.transform;

            bool targetIsVisible = Physics.Raycast(selfTransform.position, targetTransform.position - selfTransform.position, aggroRange, ~enemyMask);
            if (targetIsVisible)
            {
                currentWeight.proximity = Vector3.Distance(selfTransform.position, targetTransform.position);
                currentWeight.visibilityFactor = MAX_VISIBILITY_FACTOR;
            }
            else
            {
                currentWeight.visibilityFactor -= visibilityDecayRate;
                currentWeight.visibilityFactor = (currentWeight.visibilityFactor < 0) ? MIN_VISIBILITY_FACTOR : currentWeight.visibilityFactor;
            }
        }
    }

    private void UpdateTarget()
    {
        GameObject nextTarget = null;
        float highestWeight = -1.0f;
        
        foreach (KeyValuePair<GameObject, TargetWeight> targetWeightPair in targetWeightDict)
        {
            TargetWeight currentWeight = targetWeightPair.Value;
            GameObject currentTarget = targetWeightPair.Key;

            if (currentWeight.visibilityFactor <= 0) continue;

            float weightFactor = currentWeight.visibilityFactor + currentWeight.aggressionFactor;
            if (weightFactor > highestWeight)
            {
                highestWeight = weightFactor;
                nextTarget = currentTarget;
            }
        }
    }

    public void AddTarget(GameObject target)
    {
        TargetWeight weight = new TargetWeight();
        targetWeightDict.Add(target, weight);
    }

    public void RemoveTarget(GameObject target)
    {
        targetWeightDict.Remove(target);
    }
}
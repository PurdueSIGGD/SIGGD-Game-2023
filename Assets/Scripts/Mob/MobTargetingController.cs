using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTargetingController : MonoBehaviour
{
    public GameObject target;

    private const float MIN_VISIBILITY_FACTOR = 0f;
    private const float MAX_VISIBILITY_FACTOR = 100f;

    private const float MIN_AGGRESSION_FACTOR = 0f;
    private const float MAX_AGGRESSION_FACTOR = 100f;

    protected struct TargetWeight
    {
        public float proximity;
        public float visibilityFactor; // 0 - 100
        public float aggressionFactor; // 0 - 100
    }

    [SerializeField] private float aggroRange;
    [SerializeField] private float visibilityDecayRate;
    [SerializeField] private float aggressionDecayRate;
    [SerializeField] private float tickUpdateRate;

    private Dictionary<GameObject, TargetWeight> targetWeightList;

    private void UpdateWeights()
    {

    }

    private void UpdateTarget()
    {

    }

    public void AddTarget(GameObject newTarget)
    {

    }

    public void AddTarget(GameObject[] newTargets)
    {

    }

    public void RemoveTarget(GameObject target)
    {

    }

    public void RemoveTarget(GameObject[] targets)
    {

    }
}
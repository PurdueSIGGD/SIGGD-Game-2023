using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueGoal : BehaviorGoal
{
    public Transform targetTransform;

    public string targetTag;
    public bool targetIsVisible;
    public float targetDistance;
    public float targetHealth;
}
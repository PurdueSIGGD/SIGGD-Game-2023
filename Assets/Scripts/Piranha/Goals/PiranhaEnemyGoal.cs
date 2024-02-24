using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaEnemyGoal : BehaviorGoal
{
    private GameObject gameObject;
    private Transform selfTransform;
    private LayerMask enemyMask;

    public GameObject target;
    public Transform targetTransform;
    
    public float targetDistance;
    public bool targetIsVisible;

    public override bool GoalUpdate()
    {
        if (target == null)
        {
            return false;
        }

        RaycastHit hit;
        targetIsVisible = Physics.Raycast(selfTransform.position, targetTransform.position - selfTransform.position, out hit, Mathf.Infinity, );

        return true;
    }

    public PiranhaEnemyGoal(GameObject gameObject, GameObject target)
    {
        this.gameObject = gameObject;
        this.target = target;
        this.targetTransform = target.transform;
        GoalUpdate();
    }
}
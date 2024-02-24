using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PiranhaNavigationController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private float avoidDistance;
    [SerializeField] private float flankThreshold;
    [SerializeField] private bool seeThroughWalls;

    private Transform selfTransform;
    private float colliderRadius;

    private LayerMask enemyMask;

    private NavMeshAgent navAgent;
    private NavMeshPath navPath;

    void Start()
    {
        selfTransform = gameObject.GetComponent<Transform>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        colliderRadius = gameObject.GetComponent<CapsuleCollider>().radius;
        enemyMask = LayerMask.GetMask("Enemy");
        navPath = new NavMeshPath();
    }

    public bool Pursue(Transform targetTransform)
    {
        NavMesh.CalculatePath(selfTransform.position, targetTransform.position, NavMesh.AllAreas, navPath);
        if (navPath.corners.Length < 1) return false;

        // Rotate towards target
        Vector3 targetLookDir = navPath.corners[1] - selfTransform.position;
        Vector3 newLookDir = Vector3.RotateTowards(selfTransform.forward, targetLookDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
        selfTransform.rotation = Quaternion.LookRotation(newLookDir);

        // Move towards target
        Vector3 moveOffset = selfTransform.forward;
        moveOffset = moveOffset.normalized * (moveSpeed * Time.fixedDeltaTime);
        navAgent.Move(moveOffset);

        return true;
    }

    public bool Retreat(Transform targetTransform)
    {
        NavMesh.CalculatePath(selfTransform.position, targetTransform.position, NavMesh.AllAreas, navPath);
        if (navPath.corners.Length < 1) return false;

        // Rotate towards target
        Vector3 targetLookDir = (navPath.corners[1] - selfTransform.position) * -1;
        Vector3 newLookDir = Vector3.RotateTowards(selfTransform.forward, targetLookDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
        selfTransform.rotation = Quaternion.LookRotation(newLookDir);

        // Move towards target
        Vector3 moveOffset = selfTransform.forward * -1;
        moveOffset = moveOffset.normalized * (moveSpeed * Time.fixedDeltaTime);
        navAgent.Move(moveOffset);

        return true;
    }
}
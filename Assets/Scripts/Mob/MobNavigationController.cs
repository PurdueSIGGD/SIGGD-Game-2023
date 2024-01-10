using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobNavigationController : MonoBehaviour
{
    public enum NavBehavior
    {
        Idle,
        Pursue,
    }

    public bool isActive;
    public NavBehavior behavior;

    public float moveSpeed;
    public float turnSpeed;

    [SerializeField] private float avoidDistance;
    [SerializeField] private float flankThreshold;
    [SerializeField] private bool seeThroughWalls;

    private MobTargetingController targetingController;
    private NavMeshAgent navAgent;
    private Transform targetTransform;
    private Transform selfTransform;
    private NavMeshPath navPath;
    private LayerMask enemyMask;
    private float colliderRadius;

    void Start()
    {
        targetingController = gameObject.GetComponent<MobTargetingController>();
        selfTransform = gameObject.GetComponent<Transform>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyMask = LayerMask.GetMask("Enemy");
        colliderRadius = gameObject.GetComponent<CapsuleCollider>().radius;

        navPath = new NavMeshPath();
    }

    void FixedUpdate()
    {
        switch (behavior)
        {
            case NavBehavior.Idle:
                return;
            case NavBehavior.Pursue:
                Pursue();
                return;
        }
    }

    private void Pursue()
    {
        if (!isActive) return;

        //if (targetingController.target == null) return;
        //targetTransform = targetingController.target.transform;
        targetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        // Rotate towards target
        NavMesh.CalculatePath(selfTransform.position, targetTransform.position, NavMesh.AllAreas, navPath);
        Vector3 targetLookDir = navPath.corners[1] - selfTransform.position;
        Vector3 newLookDir = Vector3.RotateTowards(selfTransform.forward, targetLookDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
        selfTransform.rotation = Quaternion.LookRotation(newLookDir);

        // Move towards target
        Vector3 moveOffset = selfTransform.forward;

        // Avoid contact on left/right side w/ other enemies
        RaycastHit leftHit;
        RaycastHit rightHit;
        bool leftHitBool = Physics.Raycast((selfTransform.position + (selfTransform.right * -1 * colliderRadius * 0.51f)), (selfTransform.right * -1), out leftHit, avoidDistance, ~enemyMask);
        bool rightHitBool = Physics.Raycast((selfTransform.position + (selfTransform.right * colliderRadius * 0.51f)), selfTransform.right, out rightHit, avoidDistance, ~enemyMask);
        if (leftHitBool && rightHitBool)
        {
            moveOffset += selfTransform.right * (leftHit.distance - rightHit.distance);
        }
        else if (leftHitBool || rightHitBool)
        {
            moveOffset += selfTransform.right * (rightHitBool ? -1 : 1);
        }

        bool isFlankingRight;
        float angle;

        // Flank if within range & visible
        RaycastHit targetHit;
        bool targetIsVisible = Physics.Raycast(selfTransform.position, targetTransform.position - selfTransform.position, out targetHit, Mathf.Infinity, enemyMask);
        if (targetIsVisible && targetHit.distance < flankThreshold)
        {
            angle = Vector3.Angle(selfTransform.right, (selfTransform.position - targetTransform.position));
            isFlankingRight = (angle < 90);
            moveOffset += selfTransform.right * (isFlankingRight ? 1 : -1);
        }

        // Avoid contact ahead of enemy
        RaycastHit frontHit;
        if (Physics.Raycast(selfTransform.position + (selfTransform.forward * colliderRadius * 0.51f), selfTransform.forward, out frontHit, avoidDistance, enemyMask))
        {
            angle = Vector3.Angle(selfTransform.right, (selfTransform.position - frontHit.collider.gameObject.transform.position));
            isFlankingRight = (angle < 90);
            moveOffset += selfTransform.right * (isFlankingRight ? 1 : -1);
        }

        moveOffset = moveOffset.normalized * (moveSpeed * Time.fixedDeltaTime);
        navAgent.Move(moveOffset);
    }
}
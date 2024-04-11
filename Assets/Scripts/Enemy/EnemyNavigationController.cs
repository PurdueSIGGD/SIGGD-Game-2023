using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigationController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private float avoidDistance; // When obstacle is within this distance, entity will avoid the obstacle
    [SerializeField] private float flankThreshold; // When target is within this distance, entity will flank the target
    [SerializeField] private bool seeThroughWalls;

    private EnemyTargetingController targetingController;
    private NavMeshAgent navAgent;
    private Transform selfTransform;
    private NavMeshPath navPath;
    private LayerMask enemyMask;
    private float colliderRadius;

    void Start()
    {
        targetingController = gameObject.GetComponent<EnemyTargetingController>();
        selfTransform = gameObject.GetComponent<Transform>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyMask = LayerMask.GetMask("Enemy");
        colliderRadius = gameObject.GetComponent<CapsuleCollider>().radius;

        navPath = new NavMeshPath();
    }

    [SerializeField] private float pursueMoveFactor = 1;
    [SerializeField] private float pursueTurnFactor = 1;

    private void Pursue()
    {
        if (targetingController.target == null) return;
        Transform targetTransform = targetingController.target.transform;

        NavMesh.CalculatePath(selfTransform.position, targetTransform.position, NavMesh.AllAreas, navPath);
        if (navPath.corners.Length < 1) return;

        // Rotate towards target
        Vector3 targetLookDir = navPath.corners[1] - selfTransform.position;
        Vector3 newLookDir = Vector3.RotateTowards(selfTransform.forward, targetLookDir, turnSpeed * pursueTurnFactor * Time.fixedDeltaTime, 0.0f);
        selfTransform.rotation = Quaternion.LookRotation(newLookDir);

        // Move towards target
        Vector3 moveOffset = selfTransform.forward;

        // Avoid contact on left/right side w/ other enemies
        RaycastHit leftHit;
        RaycastHit rightHit;
        bool leftHitBool = Physics.Raycast((selfTransform.position + (selfTransform.right * -1 * colliderRadius * 0.51f)), (selfTransform.right * -1), out leftHit, avoidDistance, enemyMask);
        bool rightHitBool = Physics.Raycast((selfTransform.position + (selfTransform.right * colliderRadius * 0.51f)), selfTransform.right, out rightHit, avoidDistance, enemyMask);
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
        bool targetIsVisible = Physics.Raycast(selfTransform.position, targetTransform.position - selfTransform.position, out targetHit, Mathf.Infinity, ~enemyMask);
        if (targetIsVisible && targetHit.distance < flankThreshold)
        {
            angle = Vector3.Angle(selfTransform.right, (selfTransform.position - targetTransform.position));
            moveOffset = Vector3.Lerp(moveOffset, selfTransform.right, 2 * (flankThreshold - targetHit.distance) / flankThreshold);
        }

        // Avoid contact ahead of enemy
        RaycastHit frontHit;
        if (Physics.Raycast(selfTransform.position + (selfTransform.forward * colliderRadius * 0.51f), selfTransform.forward, out frontHit, avoidDistance, ~enemyMask))
        {
            angle = Vector3.Angle(selfTransform.right, (selfTransform.position - frontHit.collider.gameObject.transform.position));
            isFlankingRight = (angle < 90);
            moveOffset += selfTransform.right * (isFlankingRight ? 1 : -1);
        }

        moveOffset = moveOffset.normalized * (moveSpeed * pursueMoveFactor * Time.fixedDeltaTime);
        navAgent.Move(moveOffset);
    }

    [SerializeField] private float dashRangeThreshold;
    [SerializeField] private float dashMoveFactor = 1;
    [SerializeField] private float dashTurnFactor = 1;
    [SerializeField] private float dashExpDecay = 1;
    [SerializeField] private float dashDuration = -1;
    [SerializeField] private float dashCooldown = -1;
    private float dashCurrentCooldown = 0;

    private bool CheckDashPreconditions()
    {
        if (dashCurrentCooldown > 0)
        {
            return false;
        }

        Transform targetTransform = targetingController.target.transform;
        Vector3 targetDirection = targetTransform.position - selfTransform.position;
        float distanceToTarget = targetDirection.magnitude;
        if (distanceToTarget > dashRangeThreshold)
        {
            return false;
        }

        float dot = Vector3.Dot(selfTransform.forward, targetDirection / distanceToTarget);
        if (dot < 0.9)
        {
            return false;
        } 

        return true;
    }

    private void Dash()
    {
        if (targetingController.target == null) return;
        Transform targetTransform = targetingController.target.transform;

        // Rotate towards target
        NavMesh.CalculatePath(selfTransform.position, targetTransform.position, NavMesh.AllAreas, navPath);
        Vector3 targetLookDir = navPath.corners[1] - selfTransform.position;
        Vector3 newLookDir = Vector3.RotateTowards(selfTransform.forward, targetLookDir, turnSpeed * dashTurnFactor * Time.fixedDeltaTime, 0.0f);
        selfTransform.rotation = Quaternion.LookRotation(newLookDir);

        // Move towards target
        Vector3 moveOffset = selfTransform.forward;

        moveOffset = moveOffset.normalized * moveSpeed * dashMoveFactor * Mathf.Exp(-dashExpDecay * (dashDuration - currentBehaviorRemainingTime)) * Time.fixedDeltaTime;
        navAgent.Move(moveOffset);
    }

    // Behavior Selection

    private enum NavBehavior
    {
        Pursue,
        Dash,
    }

    private NavBehavior currentBehavior;
    private float currentBehaviorRemainingTime = 0;
    
    private void SetCurrentBehavior()
    {
        if (targetingController.target == null)
        {
            currentBehavior = NavBehavior.Pursue;
            currentBehaviorRemainingTime = 0;
            return;
        }
        
        if (CheckDashPreconditions())
        {
            currentBehavior = NavBehavior.Dash;
            currentBehaviorRemainingTime = dashDuration;
            dashCurrentCooldown = dashCooldown + dashDuration;
            return;
        }

        currentBehavior = NavBehavior.Pursue;
        currentBehaviorRemainingTime = 0;
    }

    private void UpdateTimers()
    {
        float deltaTime = Time.fixedDeltaTime;
        
        if (currentBehaviorRemainingTime > 0)
        {
            currentBehaviorRemainingTime -= deltaTime;
        }

        if (dashCurrentCooldown > 0)
        {
            dashCurrentCooldown -= deltaTime;
        }
    }

    void FixedUpdate()
    {
        UpdateTimers();

        if (currentBehaviorRemainingTime <= 0 || targetingController.target == null) // Only change state if current state is not blocking or there is no target
        {
            SetCurrentBehavior();
        }
    
        switch (currentBehavior)
        {
            case NavBehavior.Pursue:
                Pursue();
                return;
            case NavBehavior.Dash:
                Dash();
                return;
        }
    }
}
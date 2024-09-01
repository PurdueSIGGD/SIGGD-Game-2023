using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhaleNav : MonoBehaviour
{
    private Transform thisEnemy;
    private Transform target;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private DirectionalSprite spriteHolder;

    [SerializeField] private float passDuration;
    [SerializeField] private float maxPassDistance;
    private float currentDuration;

    private LayerMask enemyMask;
    private LayerMask wallMask;
    private NavMeshPath path;

    [SerializeField] private float searchTurnSpeed;
    [SerializeField] private float searchMoveSpeed;
    [SerializeField] private float angleThreshold;
    [SerializeField] private float passMoveSpeed;
    [SerializeField] private float passTurnSpeed;
    [SerializeField] private bool targetPlayer;

    [SerializeField] private AudioSource passSound;

    private enum NavMode
    {
        SEARCHING,
        PASSING
    };
    private NavMode navMode = NavMode.SEARCHING;

    void Start()
    {
        thisEnemy = gameObject.transform;
        target = null;

        enemyMask = LayerMask.NameToLayer("Enemy");
        wallMask = LayerMask.NameToLayer("Wall");
        path = new NavMeshPath();


        currentDuration = 0.0f;

        UpdateTarget();
    }

    void UpdateTarget()
    {
        List<GameObject> targets = new List<GameObject>();
        if (targetPlayer) targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Unit"));

        // Select random target
        target = targets[Random.Range(0, targets.Count)].transform;
    }

    // Update the navigation state; If conditions stay the same then the state will not switch.
    void NavModeUpdate()
    {
        // Pathfind to target until it finds a line of sight
        if (navMode == NavMode.SEARCHING)
        {
            if (target == null) UpdateTarget();

            Vector3 targetDir = target.position - thisEnemy.position;

            RaycastHit cast;
            bool hasLOS = Physics.Raycast(thisEnemy.position, targetDir, out cast, maxPassDistance, ~enemyMask);
            if (!hasLOS) return;

            float angleToTarget = Vector3.Angle(targetDir, thisEnemy.forward);
            if (angleToTarget > angleThreshold) return;

            passSound.Play();
            navMode = NavMode.PASSING;
        }

        // Do a pass until duration runs out
        if (navMode == NavMode.PASSING)
        {
            if (target == null) UpdateTarget();

            currentDuration += Time.fixedDeltaTime;

            /*
            RaycastHit cast;
            Vector3 castPosition = new Vector3(thisEnemy.position.x, 1, thisEnemy.position.y);
            if (Physics.Raycast(thisEnemy.position, thisEnemy.forward, out cast, 10.0f, wallMask))
            {
                Debug.Log("Cast");
                GameObject t = cast.collider.gameObject;
                Debug.Log(t.tag);
                if (t.layer == ~wallMask)
                {
                    navMode = NavMode.SEARCHING;
                    currentDuration = 0;
                    return;
                }
            }
            */

            if (currentDuration < passDuration) return;
            currentDuration -= passDuration;

            UpdateTarget();
            navMode = NavMode.SEARCHING;
        }
    }

    void SearchNavUpdate()
    {
        NavMesh.CalculatePath(thisEnemy.position, target.position, NavMesh.AllAreas, path);
        for (int i = 1; i < path.corners.Length; i++) { Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.green); }
        if (path.corners.Length <= 1) return;

        // Update Orientation
        Vector3 targetDir = target.position - thisEnemy.position;
        Vector3 newDir = Vector3.RotateTowards(thisEnemy.forward, targetDir, searchTurnSpeed * Time.fixedDeltaTime, 0.0f);
        thisEnemy.rotation = Quaternion.LookRotation(newDir);

        // Update Movement
        Vector3 moveOffset = thisEnemy.forward;
        agent.Move(moveOffset * searchMoveSpeed * Time.fixedDeltaTime);
    }

    void PassNavUpdate()
    {
        NavMesh.CalculatePath(thisEnemy.position, target.position, NavMesh.AllAreas, path);
        for (int i = 1; i < path.corners.Length; i++) { Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.green); }
        if (path.corners.Length <= 1) return;

        // Update Orientation
        Vector3 targetDir = target.position - thisEnemy.position;
        Vector3 newDir = Vector3.RotateTowards(thisEnemy.forward, targetDir, passTurnSpeed * Time.fixedDeltaTime, 0.0f);
        thisEnemy.rotation = Quaternion.LookRotation(newDir);

        // Update Movement
        Vector3 moveOffset = thisEnemy.forward;
        agent.Move(moveOffset * passMoveSpeed * Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        Vector3 moveOffset = thisEnemy.forward;
        spriteHolder.lookDirectionOverride = moveOffset;
        NavModeUpdate();
        switch(navMode)
        {
            case NavMode.SEARCHING:
                SearchNavUpdate(); break;
            case NavMode.PASSING:
                PassNavUpdate(); break; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob_NavigationController : MonoBehaviour
{
    public bool active;

    [SerializeField] private NavMeshAgent agent;
    private Transform enemyTransform;
    private Transform targetTransform;
    private NavMeshPath path;
    
    public float speed;
    public float turnSpeed;
    
    private float colliderRadius;

    private bool flankDir;
    [SerializeField] private float rayDist;
    [SerializeField] private float flankDist;
    [SerializeField] private bool seeThroughWalls;


    void Start()
    {
        enemyTransform = gameObject.GetComponent<Transform>();
        targetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        path = new NavMeshPath();

        colliderRadius = gameObject.GetComponent<CapsuleCollider>().radius;
    }
    void FixedUpdate()
    {
        if (!active) return;
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        LayerMask mask2 = LayerMask.GetMask("Player");
        if (seeThroughWalls)
        {
            mask = ~mask2;
        }
        if (Physics.Raycast(GetComponent<Transform>().position, targetTransform.position - enemyTransform.position, out hit, Mathf.Infinity, ~mask))
        {
            if (hit.collider.gameObject.transform == targetTransform)
            {
                NavMesh.CalculatePath(enemyTransform.position, targetTransform.position, NavMesh.AllAreas, path);
                //agent.SetDestination(player.position);
                //Vector3 targetDir = player.position - this_enemy.position;
                Vector3 targetDir = path.corners[1] - enemyTransform.position;
                //Debug.Log(path.corners[1]);
                Vector3 newDir = Vector3.RotateTowards(enemyTransform.forward, targetDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
                enemyTransform.rotation = Quaternion.LookRotation(newDir);

                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                }
                Vector3 move_offset = enemyTransform.forward;
                RaycastHit leftHit;
                RaycastHit rightHit;
                bool leftHitBool = Physics.Raycast((enemyTransform.position + (enemyTransform.right * -1 * colliderRadius * 0.51f)), (enemyTransform.right * -1), out leftHit, rayDist, mask);
                bool rightHitBool = Physics.Raycast((enemyTransform.position + (enemyTransform.right * colliderRadius * 0.51f)), enemyTransform.right, out rightHit, rayDist, mask);
                if (leftHitBool && !rightHitBool)
                {
                    move_offset += enemyTransform.right * 1;
                }
                else if (!leftHitBool && rightHitBool)
                {
                    move_offset += enemyTransform.right * -1;
                }
                else if (leftHitBool && rightHitBool)
                {
                    move_offset += enemyTransform.right * (rightHit.distance - leftHit.distance) * -1;
                }
                RaycastHit frontHit;
                if (hit.distance < flankDist)
                {
                    Vector3 targetPos = targetTransform.position;
                    float angle = Vector3.Angle(enemyTransform.right, (enemyTransform.position - targetPos));
                    if (angle < 90)
                    {
                        flankDir = true;
                    }
                    else
                    {
                        flankDir = false;
                    }
                    Vector3 flank_offset = enemyTransform.right;
                    if (flankDir == false)
                    {
                        flank_offset *= -1;
                    }
                    //Debug.DrawLine(this_enemy.position, (this_enemy.position + flank_offset), Color.white);
                    move_offset += flank_offset;
                }

                if (Physics.Raycast((enemyTransform.position + (enemyTransform.forward * colliderRadius * 0.51f)), enemyTransform.forward, out frontHit, rayDist, mask))
                {
                    Vector3 targetPos = frontHit.collider.gameObject.transform.position;
                    float angle = Vector3.Angle(enemyTransform.right, (enemyTransform.position - targetPos));
                    if (angle < 90)
                    {
                        flankDir = true;
                    }
                    else
                    {
                        flankDir = false;
                    }
                    Vector3 flank_offset = enemyTransform.right;
                    if (flankDir == false)
                    {
                        flank_offset *= -1;
                    }
                    //Debug.DrawLine(this_enemy.position, (this_enemy.position + flank_offset), Color.white);
                    move_offset += flank_offset;
                }

                move_offset = move_offset.normalized;
                //string pos_debug = "" + move_offset.x + " " + move_offset.y + " " + move_offset.z;
                Debug.DrawLine(enemyTransform.position, (enemyTransform.position + 2 * move_offset), Color.blue);
                agent.Move(move_offset * speed * Time.fixedDeltaTime);
            }
        }


    }
}

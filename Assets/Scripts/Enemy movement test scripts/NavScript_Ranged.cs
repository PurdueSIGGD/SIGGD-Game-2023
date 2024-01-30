using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript_Ranged : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform this_enemy;
    private Transform player;
    private NavMeshPath path;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private Vector3 boxSize;
    private bool flankDir;
    [SerializeField] private float rayDist;
    [SerializeField] private bool seeThroughWalls;
    [SerializeField] float maxRangeDist;


    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        boxSize = GetComponent<BoxCollider>().size;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        LayerMask mask2 = LayerMask.GetMask("Player");
        if (seeThroughWalls) {
            mask = ~mask2;
        }
        if (Physics.Raycast(GetComponent<Transform>().position, player.position - this_enemy.position, out hit, Mathf.Infinity, mask)) {
            if (hit.collider.gameObject.transform == player) {
                NavMesh.CalculatePath(this_enemy.position, player.position, NavMesh.AllAreas, path);
                //agent.SetDestination(player.position);
                //Vector3 targetDir = player.position - this_enemy.position;
                Vector3 targetDir = path.corners[1] - this_enemy.position;
                //Debug.Log(path.corners[1]);
                Vector3 newDir = Vector3.RotateTowards(this_enemy.forward, targetDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
                this_enemy.rotation = Quaternion.LookRotation(newDir);

                for (int i = 0; i < path.corners.Length - 1; i++) {
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                }
                float distToPlayer = Vector3.Distance(player.position, this_enemy.position);
                Vector3 move_offset = Vector3.zero;

                
                if (distToPlayer < (maxRangeDist - 1)) {
                    //Debug.Log("back back");
                    move_offset = this_enemy.forward * -0.1f;
                }
                if (path.corners.Length > 2 || distToPlayer > maxRangeDist) {
                    //Debug.Log("Hello");
                    move_offset = this_enemy.forward;
                }


                RaycastHit leftHit;
                RaycastHit rightHit;
                bool leftHitBool = Physics.Raycast((this_enemy.position + (this_enemy.right * -1 * boxSize.x * 0.51f)), (this_enemy.right * -1), out leftHit, rayDist, mask);
                bool rightHitBool = Physics.Raycast((this_enemy.position + (this_enemy.right * boxSize.x * 0.51f)), this_enemy.right, out rightHit, rayDist, mask);
                if (leftHitBool && !rightHitBool) {
                    move_offset += this_enemy.right * 1;
                }
                else if (!leftHitBool && rightHitBool) {
                    move_offset += this_enemy.right * -1;
                }
                else if (leftHitBool && rightHitBool) {
                    move_offset += this_enemy.right * (rightHit.distance - leftHit.distance) * -1;
                }
                

                RaycastHit frontHit;
                if (Physics.Raycast((this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f)), this_enemy.forward, out frontHit, rayDist, mask)) {
                    Vector3 targetPos = frontHit.collider.gameObject.transform.position;
                    float angle = Vector3.Angle(this_enemy.right, (this_enemy.position - targetPos));
                    if (angle < 90) {
                        flankDir = true;
                    }
                    else {
                        flankDir = false;
                    }
                    Vector3 flank_offset = this_enemy.right;
                    if (flankDir == false) {
                        flank_offset *= -1;
                    }
                    //Debug.DrawLine(this_enemy.position, (this_enemy.position + flank_offset), Color.white);
                    move_offset += flank_offset;
                }
                move_offset = move_offset.normalized;
                
                Debug.DrawLine(this_enemy.position, (this_enemy.position + 2 * move_offset), Color.blue);
                if (move_offset != Vector3.zero) {
                    agent.Move(move_offset * speed * Time.fixedDeltaTime);
                }
            }
        }

        
    }
}

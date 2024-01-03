using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript_Mob : MonoBehaviour
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
    [SerializeField] private float flankDist;
    [SerializeField] private bool seeThroughWalls;


    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        boxSize = GetComponent<BoxCollider>().size;
    }
    void FixedUpdate()
    {
        Debug.Log("updaet");

        if (!enabled) return;
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        LayerMask mask2 = LayerMask.GetMask("Player");
        if (seeThroughWalls) {
            mask = ~mask2;
        }
        Debug.Log("aaaaaa");

        if (Physics.Raycast(GetComponent<Transform>().position, player.position - this_enemy.position, out hit, Mathf.Infinity, ~mask)) {
            Debug.Log("rrrrrr");
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
                Vector3 move_offset = this_enemy.forward;
                RaycastHit leftHit;
                RaycastHit rightHit;
                bool leftHitBool = Physics.Raycast((this_enemy.position + (this_enemy.right * -1 * boxSize.x * 0.51f)), (this_enemy.right * -1), out leftHit, rayDist, mask);
                bool rightHitBool = Physics.Raycast((this_enemy.position + (this_enemy.right * boxSize.x * 0.51f)), this_enemy.right, out rightHit, rayDist, mask);
                Debug.Log("Pre");
                if (leftHitBool && !rightHitBool) {
                    Debug.Log("Side1");
                    move_offset += this_enemy.right * 1;
                }
                else if (!leftHitBool && rightHitBool) {
                    Debug.Log("Side2");
                    move_offset += this_enemy.right * -1;
                }
                else if (leftHitBool && rightHitBool) {
                    Debug.Log("Side3");
                    move_offset += this_enemy.right * (rightHit.distance- leftHit.distance) * -1;
                } 
                else if (!leftHitBool && !rightHitBool)
                {
                    Debug.Log("None");
                }
                RaycastHit frontHit;
                if (hit.distance < flankDist) {
                    Debug.Log("Flank");
                    Vector3 targetPos = player.position;
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

                if (Physics.Raycast((this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f)), this_enemy.forward, out frontHit, rayDist, mask)) {
                    Debug.Log("Flank2");

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
                //string pos_debug = "" + move_offset.x + " " + move_offset.y + " " + move_offset.z;
                Debug.DrawLine(this_enemy.position, (this_enemy.position + 2 * move_offset), Color.blue);
                agent.Move(move_offset * speed * Time.fixedDeltaTime);
            }
        }

        
    }
}

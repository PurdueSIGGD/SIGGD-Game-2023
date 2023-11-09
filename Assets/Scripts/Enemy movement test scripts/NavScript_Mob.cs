using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.VirtualTexturing;

public class NavScript_Mob : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform this_enemy;
    private Transform player;
    public float maxDistance;
    private NavMeshPath path;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private Vector3 boxSize;
    [SerializeField] private float rayDist;
    [SerializeField] private float flankDist;
    private bool flankDir;


    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        boxSize = GetComponent<BoxCollider>().size;
        flankDir = (Random.Range(0, 1) > 0.5);
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        Debug.Log(mask == null);
        if (Physics.Raycast(GetComponent<Transform>().position, player.position - this_enemy.position, out hit, Mathf.Infinity, ~mask)) {
            if (hit.collider.gameObject.transform == player) {
                NavMesh.CalculatePath(this_enemy.position, player.position, NavMesh.AllAreas, path);
                //agent.SetDestination(player.position);
                Vector3 targetDir = player.position - this_enemy.position;
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
                if (leftHitBool && !rightHitBool) {
                    move_offset += this_enemy.right * 1;
                }
                else if (!leftHitBool && rightHitBool) {
                    move_offset += this_enemy.right * -1;
                }
                else if (leftHitBool && rightHitBool) {
                    move_offset += this_enemy.right * (rightHit.distance- leftHit.distance) * -1;
                }
                RaycastHit frontHit;
                if ((hit.distance < flankDist) || (Physics.Raycast((this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f)), this_enemy.forward, out frontHit, rayDist, mask))) {
                    Vector3 flank_offset = this_enemy.right;
                    if (flankDir == true) {
                        flank_offset *= -1;
                    }
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

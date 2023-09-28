using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript_Mob : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform this_enemy;
    private Transform player;
    public float maxDistance;
    private NavMeshPath path;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;


    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
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
            }
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript_Lunge : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform this_enemy;
    private Transform player;
    private NavMeshPath path;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float flankSpeed;
    private Vector3 boxSize;
    [SerializeField] private float retreatSpeed;
    [SerializeField] private float minRangeDist;
    [SerializeField] private float rangeVariability;
    [SerializeField] private float pounceSpeed;
    [SerializeField] private float pounceDecay;
    [SerializeField] private float maxPounceTime;
    [SerializeField] private float minPounceTime;
    private bool inPounce;
    private float tempSpeed;
    private int flankDir;
    private float lastPounceTime;
    private float timeToNextPounce;

    [SerializeField] private float minChange;
    [SerializeField] private float maxChange;
    private float lastChangeTime;
    private float timeToNextChange;
    private float tempRange;



    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        boxSize = GetComponent<BoxCollider>().size;
        lastChangeTime = 0f;
        flankDir = -1;
        timeToNextChange = Random.Range(minChange, maxChange);
        timeToNextPounce = Random.Range(minPounceTime, maxPounceTime);
        lastPounceTime = 0f;
        tempRange = minRangeDist;
    }
    void FixedUpdate()
    {

        float distToPlayer = Vector3.Distance(player.position, this_enemy.position);
        Vector3 move_offset = Vector3.zero;

        if (inPounce == false) {

            NavMesh.CalculatePath(this_enemy.position, player.position, NavMesh.AllAreas, path);
            //agent.SetDestination(player.position);
            //Vector3 targetDir = player.position - this_enemy.position;
            Vector3 targetDir = path.corners[1] - this_enemy.position;
            //Debug.Log(path.corners[1]);
            Vector3 newDir = Vector3.RotateTowards(this_enemy.forward, targetDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
            this_enemy.rotation = Quaternion.LookRotation(newDir);

            // for (int i = 0; i < path.corners.Length - 1; i++) {
            //     Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            // }


            if (distToPlayer < minRangeDist) {
                //Debug.Log("back back");
                move_offset = this_enemy.forward * -1 * retreatSpeed;
            }
            else if (distToPlayer < (tempRange - 0.5f)) {
                //Debug.Log("back faster");
                move_offset = this_enemy.forward * -1;
            }
            if (path.corners.Length > 2 || distToPlayer > (tempRange + 0.5f)) {
                //Debug.Log("Hello");
                move_offset = this_enemy.forward;
            }

            
            move_offset = move_offset.normalized;

            if (path.corners.Length <= 2) {
                move_offset += this_enemy.right * flankDir * flankSpeed;
            }
            else {
                lastPounceTime = Time.time;
            }


            if ((lastChangeTime + timeToNextChange) < Time.time) {
                lastChangeTime = Time.time;
                timeToNextChange = Random.Range(minChange, maxChange);
                tempRange = minRangeDist + Random.Range(0f, rangeVariability);
                if (Random.value > 0.5f) {
                    flankDir = -1;
                }
                else {
                    flankDir = 1;
                }
            }
        }


        if (inPounce) {
            tempSpeed = pounceSpeed * Mathf.Exp(-1 * pounceDecay * (Time.time - lastPounceTime));
            move_offset += this_enemy.forward * tempSpeed;
            if (tempSpeed < 0.5f) {
                //End pounce
                inPounce = false;
                lastPounceTime = Time.time;
                timeToNextPounce = Random.Range(minPounceTime, maxPounceTime);
            }
        }

        if (inPounce == false) {
            RaycastHit frontHit;
            if (Physics.Raycast((this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f)), this_enemy.forward, out frontHit, Vector3.Distance(this_enemy.position, player.position))) {
                if (frontHit.collider.gameObject.transform == player) {
                    //Debug.Log("player available");
                    if ((lastPounceTime + timeToNextPounce) < Time.time) {
                        //Debug.Log("pounce!!!");
                        lastPounceTime = Time.time;
                        inPounce = true;
                    }
                }
            }
        }
        //move_offset = move_offset.normalized;
        
        Debug.DrawLine(this_enemy.position, (this_enemy.position + 2 * move_offset), Color.blue);
        if (move_offset != Vector3.zero) {
            agent.Move(move_offset * speed * Time.fixedDeltaTime);
        }

        
    }
}

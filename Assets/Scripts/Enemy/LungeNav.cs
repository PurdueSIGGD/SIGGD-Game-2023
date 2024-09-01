using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LungeNav : MonoBehaviour
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
    [SerializeField] private AudioSource pounceNoise;
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
    private LayerMask playerMask;



    void Start() {
        //player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        boxSize = this_enemy.localScale;
        lastChangeTime = 0f;
        flankDir = -1;
        timeToNextChange = Random.Range(minChange, maxChange);
        timeToNextPounce = Random.Range(minPounceTime, maxPounceTime);
        lastPounceTime = 0f;
        tempRange = minRangeDist + 0.5f;
        playerMask = LayerMask.NameToLayer("Player");
        playerMask = ~playerMask;
    }
    void FixedUpdate()
    {

        float distToPlayer = Vector3.Distance(player.position, this_enemy.position);
        Vector3 move_offset = Vector3.zero;

        if (inPounce == false) {

            move_offset = Vector3.zero;
            //Vector3 playerPos = player.position;
            NavMesh.CalculatePath(this_enemy.position, player.position, NavMesh.AllAreas, path);
            Vector3 targetDir = path.corners[1] - this_enemy.position;
            //Debug.DrawLine(path.corners[1], this_enemy.position, Color.magenta);
            //for (int i = 1; i < path.corners.Length; i++) { Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.green); }
            Vector3 newDir = Vector3.RotateTowards(this_enemy.forward, targetDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
            this_enemy.rotation = Quaternion.LookRotation(newDir);


            if (distToPlayer < minRangeDist) {
                //Debug.Log("back faster");
                move_offset = this_enemy.forward * -1 * retreatSpeed;
            }
            else if (distToPlayer < tempRange - 0.5f) {
                //Debug.Log("back back");
                move_offset = this_enemy.forward * -0.5f * retreatSpeed;
            }
            if (path.corners.Length > 2 || distToPlayer > tempRange) {
                //Debug.Log("forward");
                move_offset = this_enemy.forward * speed;
            }
            //Debug.DrawLine(this_enemy.position, this_enemy.position + (move_offset * 5), Color.green);
            

            if (path.corners.Length <= 2) {
                move_offset += this_enemy.right * flankDir * flankSpeed;
            }
            else {
                lastPounceTime = Time.time;
            }


            if ((lastChangeTime + timeToNextChange) < Time.time) {
                lastChangeTime = Time.time;
                timeToNextChange = Random.Range(minChange, maxChange);
                tempRange = minRangeDist + 0.5f + Random.Range(0f, rangeVariability);
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
            move_offset = this_enemy.forward * tempSpeed;
            if (tempSpeed < 0.5f) {
                //End pounce
                inPounce = false;
                lastPounceTime = Time.time;
                timeToNextPounce = Random.Range(minPounceTime, maxPounceTime);
            }
        }

        if (inPounce == false) {
            RaycastHit frontHit;
            move_offset = move_offset.normalized;
            //bool playerHit = Physics.Raycast((this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f)), this_enemy.forward, out frontHit, Vector3.Distance(this_enemy.position, player.position) + 9, playerMask);
            bool playerHit = Physics.Raycast(this_enemy.position + new Vector3(0f, 1f, 0f), this_enemy.forward, out frontHit, 30f, playerMask);
            //Debug.DrawLine(this_enemy.position + (this_enemy.forward * boxSize.z * 0.51f), frontHit.point, Color.magenta);
            Debug.DrawRay(this_enemy.position + new Vector3(0f, 1f, 0f), this_enemy.forward * 30f, Color.magenta);
            //Debug.Log("Barry Player Postion: " + player.position.x + ", " + player.position.y + ", " + player.position.z);
            if (playerHit) {
                Debug.DrawRay(frontHit.point, Vector3.up * 20f, Color.magenta);
                if ((lastPounceTime + timeToNextPounce) < Time.time) {
                    Debug.Log("Barry Pounce");
                    pounceNoise.Play();
                    lastPounceTime = Time.time;
                    inPounce = true;
                }
            }
        }
        
        //Debug.DrawLine(this_enemy.position, this_enemy.position + (move_offset * 5), Color.red);
        if (move_offset != Vector3.zero) {
            agent.Move(move_offset * speed * Time.fixedDeltaTime);
        }

        
    }
}

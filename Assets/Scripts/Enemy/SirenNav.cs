using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class SirenNav : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform this_enemy;
    private Transform player;
    private NavMeshPath path;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float flankSpeed;
    [SerializeField] private float maxRangeDist;
    [SerializeField] private float rangeVariability;
    [SerializeField] private float minChange;
    [SerializeField] private float maxChange;
    private float timeToNextChange;
    private float tempRange;
    private int flankDir;
    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();
        flankDir = -1;
        tempRange = maxRangeDist;
        canMove = true;
        StartCoroutine(swapDirs());
    }

    void FixedUpdate() {

        float distToPlayer = Vector3.Distance(player.position, this_enemy.position);
        Vector3 move_offset = Vector3.zero;

        NavMesh.CalculatePath(this_enemy.position, player.position, NavMesh.AllAreas, path);
        Vector3 targetDir = path.corners[1] - this_enemy.position;
        Vector3 newDir = Vector3.RotateTowards(this_enemy.forward, targetDir, turnSpeed * Time.fixedDeltaTime, 0.0f);
        this_enemy.rotation = Quaternion.LookRotation(newDir);  

        if (path.corners.Length > 2 || distToPlayer > (tempRange + 0.5f)) {
            move_offset += this_enemy.forward * 1.5f;
        }

        if (path.corners.Length <= 2) {
            move_offset += this_enemy.right * flankDir * flankSpeed;
            move_offset += this_enemy.forward * -1f;
        }

        

        if (move_offset != Vector3.zero && canMove) {
            agent.Move(move_offset * speed * Time.fixedDeltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator swapDirs() {
        while (true) {
            timeToNextChange = Random.Range(minChange, maxChange);
            tempRange = maxRangeDist - Random.Range(0f, rangeVariability);
            if (Random.value > 0.5f) {
                flankDir = -1;
            }
            else {
                flankDir = 1;
            }
            yield return new WaitForSeconds(timeToNextChange);
        }
    }
}

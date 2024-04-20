using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenAttack : MonoBehaviour
{

    [SerializeField] private Transform this_enemy;
    private Transform player;
    private LayerMask playerMask;
    [SerializeField] private float minAttackDelay;
    [SerializeField] private float attackRandomTimeBuffer;
    [SerializeField] private float minAttackRange;
    private float lastAttackTime;
    private float nextAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.NameToLayer("Player");
        playerMask = ~playerMask;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        lastAttackTime = Time.time;
        nextAttackTime = minAttackDelay + Random.Range(0, attackRandomTimeBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit frontHit;
        if (Physics.Raycast((this_enemy.position + (this_enemy.forward * this_enemy.localScale.z * 0.51f)), this_enemy.forward, out frontHit, Vector3.Distance(this_enemy.position, player.position) + 9, playerMask)) {
            if ((lastAttackTime + nextAttackTime) < Time.time && Vector3.Distance(this_enemy.position, player.position) < minAttackRange) {
                lastAttackTime = Time.time;
                nextAttackTime = minAttackDelay + Random.Range(0, attackRandomTimeBuffer);
                this.ThrowHands();
            }
        }
    }

    void ThrowHands() {
        Debug.Log("throwing hands");
    }
}

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
    [SerializeField] private float maxAttackRange;
    [SerializeField] private float minAttackRange;
    [SerializeField] private GameObject bulletHand;
    [SerializeField] private float bulletSpeed;
    private float lastAttackTime;
    private float nextAttackTime;
    [SerializeField] private float attackPause;
    private SirenNav thisNav;


    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.NameToLayer("Player");
        playerMask = ~playerMask;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        lastAttackTime = Time.time;
        nextAttackTime = minAttackDelay + Random.Range(0, attackRandomTimeBuffer);
        thisNav = this.GetComponent<SirenNav>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit frontHit;
        float distToPlayer = Vector3.Distance(this_enemy.position, player.position);
        if (Physics.Raycast((this_enemy.position + (this_enemy.forward * this_enemy.localScale.z * 0.51f)), this_enemy.forward, out frontHit, distToPlayer + 9, playerMask)) {
            if ((lastAttackTime + nextAttackTime) < Time.time && distToPlayer < maxAttackRange && distToPlayer > minAttackRange) {
                lastAttackTime = Time.time;
                nextAttackTime = minAttackDelay + Random.Range(0, attackRandomTimeBuffer);
                StartCoroutine(attack());
            }
        }
    }

    private void ThrowHands() {
        Debug.Log("throwing hands");
        GameObject hand = Instantiate(bulletHand, this_enemy.position + this_enemy.forward, this_enemy.rotation);
        hand.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, bulletSpeed));
    }
    IEnumerator attack() {
        thisNav.canMove = false;
        //make a sound or something here
        yield return new WaitForSeconds(attackPause / 2);
        this.ThrowHands();
        yield return new WaitForSeconds(attackPause / 2);
        thisNav.canMove = true;
        yield return null;
    }
}

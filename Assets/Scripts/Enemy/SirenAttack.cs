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
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float bulletReelTime;
    [SerializeField] private float bulletDAMAGE;
    [SerializeField] private float pullStrength;
    [SerializeField] private float bulletGrabTime;
    private float lastAttackTime;
    private float nextAttackTime;
    [SerializeField] private float attackPause;
    private GameObject curHand;
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

    public void ShutItDown() {
        if (curHand != null) {
            curHand.GetComponent<HandMechanics>().ShutItDown();
        }
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
        curHand = Instantiate(bulletHand, this_enemy.position + (this_enemy.forward * 1.6f) + (this_enemy.up * 0.5f), this_enemy.rotation);
        curHand.GetComponent<HandMechanics>().SetFields(this.gameObject.transform, bulletDAMAGE, bulletLifeTime, bulletReelTime, pullStrength, bulletGrabTime);
        curHand.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, bulletSpeed));
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

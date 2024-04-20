using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandMechanics : MonoBehaviour
{
    private float lifeTime;
    private float reelTime;
    private float DAMAGE;
    private Transform parentSiren = null;
    private float pullStrength;
    private float grabTime;
    private float birthTime;
    private bool attached;


    public void SetFields(Transform parent, float dmg, float life, float reel, float pull, float grab) {
        parentSiren = parent;
        lifeTime = life;
        DAMAGE = dmg;
        reelTime = reel;
        pullStrength = pull;
        grabTime = grab;
    }

    // Start is called before the first frame update
    void Start()
    {
        birthTime = Time.time;
        attached = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attached == false && birthTime + lifeTime < Time.time) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            attached = true;
            StartCoroutine(waitAndDie(col.gameObject));
        }
        else {
            Destroy(this.gameObject);
        }
    }

    IEnumerator waitAndDie(GameObject player) {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        player.GetComponent<HealthPoints>().damageEntity(DAMAGE);
        Movement playerMove = player.GetComponent<Movement>();
        if (playerMove.sirend == false) {
            playerMove.sirend = true;
            yield return new WaitForSeconds(grabTime);
            StartCoroutine(pullPlayer(player));
            yield return new WaitForSeconds(reelTime);
            StopCoroutine(pullPlayer(player));
            if (player == null) {  //in case the player dies before the hand un-sticks
                Destroy(this.gameObject);
                yield return null;
            }
            playerMove.sirend = false;
        }
        Destroy(this.gameObject);
        yield return null;
    }

    IEnumerator pullPlayer(GameObject player) {
        while (player != null) {
            Vector3 newPos = player.transform.position + (pullStrength * (parentSiren.position - player.transform.position));
            newPos.y = player.transform.position.y;
            player.GetComponent<Rigidbody>().MovePosition(newPos);
        }
        yield return null;
    }
}

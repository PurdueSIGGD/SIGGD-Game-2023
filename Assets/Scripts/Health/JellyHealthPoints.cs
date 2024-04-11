using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyHealthPoints : HealthPoints
{

    [SerializeField] private GameObject jellyChild;
    [SerializeField] private int childCount;
    [SerializeField] private float spreadDist;
    [SerializeField] private float spreadTime;
    private GameObject[] children; 
    private float angleDiff;

    //for debug purposes
    // [SerializeField] private int countTime = 1000;
    // private int count = 0;
    // private bool beingKilled = false;

    public void Update() {
        // count += 1;
        // if (count > countTime && !beingKilled) {
        //     this.kill();
        //     beingKilled = true;
        // }
    }


    // Start is called before the first frame update

    public override void kill() {
        Debug.Log("kill jelly");
        spawnJellies();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<MobNav>().enabled = false;
        StartCoroutine(die());
    }

    private void spawnJellies() {
        children = new GameObject[childCount];

        float randDir = Random.Range(0, 360);
        angleDiff = 360 / childCount;

        for (int i = 0; i < childCount; i++) {
            Quaternion randQuat = Quaternion.Euler(0, (randDir + (angleDiff * i)), 0);
            children[i] = Instantiate(jellyChild, this.gameObject.transform.position, randQuat);
            children[i].GetComponent<MobNav>().enabled = false;
            StartCoroutine(moveToTarget(children[i], Time.time));
        }


    }

    IEnumerator moveToTarget(GameObject jellyCh, float startTime) {
        float speedCoeff = (3 * spreadDist) / Mathf.Pow(spreadTime, 3);
        float currTime = Time.time - startTime;
        float currSpeed;
        Vector3 offset;
        while (currTime < spreadTime) {
            currTime = Time.time - startTime;
            currSpeed = Mathf.Pow((currTime - spreadTime), 2);
            currSpeed *= speedCoeff;

            offset = jellyCh.transform.forward.normalized;
            jellyCh.transform.position = jellyCh.transform.position + (offset * currSpeed * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
        jellyCh.GetComponent<MobNav>().enabled = true;
        yield return null;
    }

    IEnumerator die() {
        yield return new WaitForSeconds(spreadTime);
        for (int i = 0; i < childCount; i++) {
            children[i].GetComponent<MobNav>().enabled = true;
        }
        Destroy(this.gameObject);
        yield return null;
    }
}

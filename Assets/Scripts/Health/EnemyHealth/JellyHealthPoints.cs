using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class JellyHealthPoints : HealthPoints
{

    [SerializeField] private GameObject spriteRenderer;
    [SerializeField] private GameObject jellyChild;
    [SerializeField] private int childCount;
    [SerializeField] private float spreadDist;
    [SerializeField] private float spreadTime;
    [SerializeField] private AudioSource splitSound;
    private GameObject[] children; 
    private float angleDiff;


    private bool beingKilled = false;

    public void Update() {
        // count += 1;
        // if (count > countTime && !beingKilled) {
        //     this.kill();
        //     beingKilled = true;
        // }
    }


    // Start is called before the first frame update

    public override void kill() {
        if (!beingKilled)
        {
            beingKilled = true;
            spawnJellies();
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            spriteRenderer.SetActive(false);
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<MobNav>().enabled = false;
            StartCoroutine(die());
        }
    }

    private void spawnJellies() {
        children = new GameObject[childCount];

        float randDir = Random.Range(0, 360);
        angleDiff = 360 / childCount;
        splitSound.Play();
        for (int i = 0; i < childCount; i++) {
            Quaternion randQuat = Quaternion.Euler(0, (randDir + (angleDiff * i)), 0);
            children[i] = Instantiate(jellyChild, this.gameObject.transform.position, randQuat);
            children[i].GetComponent<MobNav>().enabled = false;
            //Debug.Log("set invul");
            StartCoroutine(moveToTarget(children[i], Time.time));
        }


    }

    IEnumerator moveToTarget(GameObject jellyCh, float startTime) {
        float speedCoeff = (3 * spreadDist) / Mathf.Pow(spreadTime, 3);
        float currTime = Time.time - startTime;
        float currSpeed;
        Vector3 offset;
        while (currTime < spreadTime) {
            if (jellyCh == null) {
                yield return null;
            }
            currTime = Time.time - startTime;
            currSpeed = Mathf.Pow((currTime - spreadTime), 2);
            currSpeed *= speedCoeff;

            offset = jellyCh.transform.forward.normalized;
            jellyCh.transform.position = jellyCh.transform.position + (offset * currSpeed * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
        if (jellyCh != null) {
            jellyCh.GetComponent<ChildJellyHealthPoints>().invulnerable = false;
            jellyCh.GetComponent<MobNav>().enabled = true;
        }
        yield return null;
    }

    IEnumerator die() {
        yield return new WaitForSeconds(spreadTime);
        for (int i = 0; i < childCount; i++) {
            if (children[i] != null) {
                children[i].GetComponent<ChildJellyHealthPoints>().invulnerable = false;
                children[i].GetComponent<MobNav>().enabled = true;
            }
        }
        Destroy(this.gameObject);
        yield return null;
    }
}

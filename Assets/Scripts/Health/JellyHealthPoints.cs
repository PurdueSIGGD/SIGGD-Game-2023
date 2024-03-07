using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyHealthPoints : HealthPoints
{

    [SerializeField] private GameObject jellyChild;
    [SerializeField] private int childCount;
    [SerializeField] private float childSpread;
    [SerializeField] private float spreadTime;

    //for debug purposes
    [SerializeField] private int countTime = 1000;
    private int count = 0;
    private GameObject[] children; 

    public void Update() {
        count += 1;
        if (count > countTime) {
            this.kill();
        }
    }


    // Start is called before the first frame update

    public void kill() {
        Debug.Log("kill jelly");
        spawnJellies();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(die());
    }

    private void spawnJellies() {
        children = new GameObject[childCount];
        for (int i = 0; i < childCount; i++) {
            children[i] = Instantiate(jellyChild, this.gameObject.transform.position, Quaternion.identity);
            children[i].GetComponent<MobNav>().enabled = false;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyHealthPoints : HealthPoints
{

    [SerializeField] private GameObject jellyChild;
    [SerializeField] private int childCount;

    //for debug purposes
    [SerializeField] private int countTime = 1000;
    private int count = 0;

    public void Update() {
        count += 1;
        if (count > countTime) {
            this.kill();
        }
    }


    // Start is called before the first frame update

    public void kill() {
        Debug.Log("kill jelly");
        for (int i = 0; i < childCount; i++) {
            Instantiate(jellyChild, this.gameObject.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

}

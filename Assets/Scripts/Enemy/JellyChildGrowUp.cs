using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyChildGrowUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float timeToMature;
    [SerializeField] private float randomBuffer;
    [SerializeField] private GameObject matureJelly;
    private float birthTime;
    private float matureTime;

    void Start()
    {
        birthTime = Time.time;
        matureTime = birthTime + timeToMature + Random.Range(0, randomBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= matureTime) {
            SpawnAdult();
            Destroy(this.gameObject);
        }
    }

    private void SpawnAdult() {
        Instantiate(matureJelly, this.gameObject.transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyChildGrowUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float timeToMature;
    [SerializeField] private float randomBuffer;
    [SerializeField] private GameObject matureJelly;
    [SerializeField] private AudioSource growUpSound;
    private float birthTime;
    public float matureTime;
    [SerializeField] private GameObject spriteRenderer;

    void Start()
    {
        birthTime = Time.time;
        matureTime = birthTime + timeToMature + Random.Range(0, randomBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= matureTime) {
            matureTime += 100f;
            StartCoroutine(growUp());
            //SpawnAdult();
            //Destroy(this.gameObject);
        }
    }

    private IEnumerator growUp()
    {
        SpawnAdult();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spriteRenderer.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<MobNav>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void SpawnAdult() {
        growUpSound.Play();
        Instantiate(matureJelly, this.gameObject.transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<EnemySpawner>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirVector = player.transform.position - transform.position;
        transform.position += dirVector.normalized * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider c) {
        if (c is CapsuleCollider) {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        //Set rotation maybe
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        gameObject.transform.position = playerTransform.position;
    }
}

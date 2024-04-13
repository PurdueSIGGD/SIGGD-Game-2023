using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerData>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : UnitMovement
{

    // -- Serialize Fields --

    // Distance fron player
    [SerializeField]
    float distFromPlayer;

    // Speed at which unit orbits in radians/sec
    [SerializeField]
    float orbitalSpeed;

    [SerializeField]
    bool clockwise;

    [SerializeField]
    float height;

    // -- Private Fields --
    private GameObject player;
    private float time;
    private float orbit;

    // -- Override Methods --
    void Start()
    {
        // Initialize fields
        player = GameObject.FindGameObjectWithTag("Player");
        time = 0;
        orbitalSpeed = (clockwise) ? orbitalSpeed : -1 * orbitalSpeed;

        // Calculate time to orbit once in secs
        orbit = 2*Mathf.PI / orbitalSpeed;
    }

    void Update()
    {
        // Update time
        time = (time + Time.deltaTime) % orbit;

        // Calculate unit circle position
        float x = Mathf.Sin(time * orbitalSpeed);
        float y = Mathf.Cos(time * orbitalSpeed);


        Vector3 pos = player.transform.position + new Vector3(x * distFromPlayer, height, y * distFromPlayer);
        this.transform.position = pos;
    }
}

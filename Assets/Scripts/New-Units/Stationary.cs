using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stationary : UnitMovement
{
    [Header("Boundries & Materials")]

    // Default material when within range
    [SerializeField]
    Material defaultMaterial;

    // Warning material when outside range 
    [SerializeField] 
    Material warningMaterial;

    // Warning boundary (beyond)
    [SerializeField]
    float warningBoundary;

    // Self-Destruct boundary (beyond)
    [SerializeField]
    float selfDestructBoundary;

    // Private Fields
    private GameObject player;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch player/owner
        player = GameObject.FindGameObjectWithTag("Player");

        // Fetch MeshRenderer component
        mesh = player.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find distancew from player
        float dist = Mathf.Abs((this.transform.position - player.transform.position).magnitude);

        // Check if WARNING or DESTRUCT
        if (dist <= warningBoundary) 
        {
            // -- Set material to default --
            this.GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
        } 
        else if (dist <= selfDestructBoundary)
        {
            // -- Set material to warning --
            this.GetComponent<MeshRenderer>().material = warningMaterial;
        }
        else
        {
            // -- Destroy game object --
            Destroy(this.gameObject);
        }

    }


}

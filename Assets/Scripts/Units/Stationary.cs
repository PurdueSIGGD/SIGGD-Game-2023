using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stationary : UnitMovement
{

    // Move speed modifier

    // -- Serialize Fields --
    [Header("Boundries")]

    // Warning boundary (beyond)
    [SerializeField]
    float warning;

    // Self-Destruct boundary (beyond)
    [SerializeField]
    float selfDestruct;

    // -- Private Fields --
    private GameObject player;

    // -- Override Methods --
    void Start()
    {
        // Fetch player/owner
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Find distance from player
        float dist = Mathf.Abs((this.transform.position - player.transform.position).magnitude);

        // Check if WARNING or DESTRUCT
        if (dist <= warning)
        {
            // -- TODO: Some Indicator --
        }
        else if (dist <= selfDestruct)
        {
            // -- TODO: Some Indicator --
        }
        else
        {
            // -- Destroy game object --
            Destroy(this.gameObject);
        }
    }
}

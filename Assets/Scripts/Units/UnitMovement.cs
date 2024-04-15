using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMovement : MonoBehaviour
{
    // Reference to the player
    public GameObject Player { get; private set; }

    // Movement speed modifier
    protected float moveSpeedModifier { get; }

    // Get player reference
    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Get difference to player
    public float playerDist()
    {
        return Vector3.Distance(Player.transform.position, transform.position);
    }

    // Ranges for detecting enemies and attacking enemies
    protected int detectRange { get; }
    protected int attackRange { get; }

}

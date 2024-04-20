using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    protected float ManaCost { get; }
    protected float SellCost { get; }
    protected HealthPoints Health { get; }


    // Behaviors
    protected UnitMovement movement;

    protected virtual void Start()
    {
        this.movement = GetComponent<UnitMovement>();
    }
}

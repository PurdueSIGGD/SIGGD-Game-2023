using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    public float ManaCost { get; protected set;  }
    public float SellCost { get; protected set; }
    public HealthPoints Health { get; protected set; }


    // Behaviors
    public UnitMovement Movement { get; protected set; }

    protected virtual void Start()
    {
        this.Movement = GetComponent<UnitMovement>();
        this.Health = GetComponent<HealthPoints>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Fields
    [Header("Unit - Base")]
    public float manaCost;
    public float sellCost;
    public UnitType type;
    public int level;

    // Properties
    public UnitMovement Movement { get; protected set; }
    public HealthPoints Health { get; protected set; }

    protected virtual void Start()
    {
        this.Movement = GetComponent<UnitMovement>();
        this.Health = GetComponent<HealthPoints>();
    }
}

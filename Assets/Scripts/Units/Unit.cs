using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    protected float ManaCost { get; }
    protected float SellCost { get; }
    protected float Health { get; }


    // Behaviors
    protected UnitMovement movement;

    // Health Object
    protected UnitHealthPoints healthPoints;
}

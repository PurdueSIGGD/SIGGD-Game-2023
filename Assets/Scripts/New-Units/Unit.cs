using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    protected abstract float ManaCost { get; }
    protected abstract float SellCost { get; }
    protected abstract float Health { get; }

    // Behaviors
    protected UnitAttack attack;
    protected UnitMovement movement;

    // Health Object
    protected UnitHealthPoints healthPoints;

    // Define function
    protected abstract void DefineBehavior();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    protected abstract float ManaCost { get; }
    protected abstract float SellCost { get; }
    protected abstract float Health { get; }

<<<<<<< HEAD
    // Behaviors
    protected UnitAttack attack;
    protected UnitMovement movement;

    // Health Object
    protected UnitHealthPoints healthPoints;
=======
    [SerializeField]
    public float Range;
    public float ManaCost { get; protected set; }
    public float SellCost { get; protected set; }
    public UnitMovement Movement { get; protected set; }

    // Private Fields
    protected Rigidbody RB;
    protected Collider Collider;

    // TODO: Health Object
    protected HealthPoints Health;
>>>>>>> main

    // Define function
    protected abstract void DefineBehavior();
}

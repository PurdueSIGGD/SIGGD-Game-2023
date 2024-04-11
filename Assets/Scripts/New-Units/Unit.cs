using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    protected GameObject player;

    protected float Range;
    protected float ManaCost { get; private set; }
    protected float SellCost { get; private set; }
    protected UnitMovement Movement;

    // Private Fields
    protected Rigidbody RB;
    protected Collider Collider;

    // TODO: Health Object

}

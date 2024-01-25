using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    [SerializeField]
    public GameObject Player;

    [SerializeField]
    public float Range;
    public float ManaCost { get; protected set; }
    public float SellCost { get; protected set; }
    public UnitMovement Movement { get; protected set; }

    // Private Fields
    protected Rigidbody RB;
    protected Collider Collider;

    // TODO: Health Object

}

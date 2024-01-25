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
    public float ManaCost { get; private set; }
    public float SellCost { get; private set; }
    public UnitMovement Movement { get; private set; }

    // Private Fields
    protected Rigidbody RB;
    protected Collider Collider;

    // TODO: Health Object

}

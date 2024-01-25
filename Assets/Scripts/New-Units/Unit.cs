using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // Properties
    public float ManaCost { get; private set; }
    public float SellCost { get; private set; }
    public UnitMovement Movement { get; private set; }

    
    // TODO: Health Object

}

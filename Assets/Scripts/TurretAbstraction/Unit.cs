using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // The light cost of the unit
    protected int manaCost { get; set; }

    // If the turret action is repeatable
    protected bool repeatable { get; set; }
}

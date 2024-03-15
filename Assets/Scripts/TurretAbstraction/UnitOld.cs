using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitOld : MonoBehaviour
{
    // The light cost of the unit
    protected int manaCost { get; set; }

    // If the turret action is repeatable
    protected bool repeatable { get; set; }
}

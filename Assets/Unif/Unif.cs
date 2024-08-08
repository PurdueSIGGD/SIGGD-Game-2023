using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthPoints))]
[RequireComponent(typeof(UnitMovement))]
public abstract class Unif : MonoBehaviour
{
    // Fields
    [Header("Unit - Base")]
    public float manaCost;
    public UnitType type;
    public int level;


}

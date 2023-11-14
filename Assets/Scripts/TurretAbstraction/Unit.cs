using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
<<<<<<< Updated upstream
    protected int manaCost;
=======
    // The light cost of the unit
    protected int manaCost { get; set; }

    // If the turret action is repeatable
    protected bool repeatable { get; set; }

    // Trigger event
    protected abstract bool DetectTrigger();

    // What the turret does
    protected abstract void Action();

    // Check for trigger on update
    public void Update()
    {
        DetectTrigger();
    }
>>>>>>> Stashed changes
}

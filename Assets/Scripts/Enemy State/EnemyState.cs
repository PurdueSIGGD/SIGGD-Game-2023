using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected EnemyStateController controller;

    public abstract void StateStart(); // If state is finite, start method should call SwitchState() from coroutine
    public abstract void StateStop(); // Called by SwitchState() to clean up resources
    public abstract void StateUpdate(); // Method for updating on every frame
    public abstract bool StateTick(); // Method for updating at interval slower than update(); used for updating state
                                        // Returns a boolean on whether controller should check for a switch.
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected EnemyStateController controller;
    private void Start()
    {
        controller = GetComponent<EnemyStateController>();
    }

    public abstract void StateStart(); // If state is finite, start method should call SwitchState() on return
    public abstract void StateStop(); // Called by SwitchState() to clean up resources
    public abstract void StateUpdate(); // Method for updating on every frame
}
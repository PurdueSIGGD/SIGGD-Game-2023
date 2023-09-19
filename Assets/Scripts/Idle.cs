using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype for possible idle state
public class Idle : EnemyState
{
    public void Start()
    {
        
    }

    public void Stop()
    {

    }

    public void Update()
    {
        // If in range, SwitchState(Attack);
    }
}

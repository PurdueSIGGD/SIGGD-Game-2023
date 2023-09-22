using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype for possible enemy state controller
public class EnemyStateController : MonoBehaviour
{
    public Idle idleState;
    public Attack attackState;

    public EnemyState currentState;

    void Start()
    {
        

        SwitchState(idleState);
    }

    void Update()
    {
        currentState.StateUpdate();
    }

    // Stops the current state and begins the new state specified by argument.
    // Should be called from Start() for finite states (e.g. attacks)
    // and from Update() for infinite states (e.g. idle, patrol)
    public void SwitchState(EnemyState nextState)
    {
        currentState?.StateStop();
        currentState = nextState;
        currentState.StateStart();
    }

}

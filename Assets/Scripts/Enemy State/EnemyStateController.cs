using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype for possible enemy state controller
public class EnemyStateController : MonoBehaviour
{
    public Idle idleState;
    public Follow followState;

    public EnemyState currentState;

    void Start()
    {
        StartCoroutine(Tick());
        SwitchState(idleState);
    }

    void Update()
    {
        currentState.StateUpdate();
    }

    // Updates at intervals slower than every frame; Used to update state
    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            currentState?.StateTick();
        }
    }

    // Stops the current state and begins the new state specified by argument.
    // Should be called from Start() or coroutine for finite states (e.g. attacks)
    // and from Tick() for non-finite states (e.g. idle, patrol)
    public void SwitchState(EnemyState nextState)
    {
        currentState?.StateStop();
        currentState = nextState;
        currentState.StateStart();
    }
}

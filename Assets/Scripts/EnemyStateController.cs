using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prototype for possible enemy state controller
public class EnemyStateController : MonoBehaviour
{
    private EnemyState currentState;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    // Stops the current state and begins the new state specified by argument.
    // Should be called from Start() for finite states (e.g. attacks)
    // and from Update() for infinite states (e.g. idle, patrol)
    public void SwitchState(EnemyState nextState)
    {
        currentState.Stop();
        currentState = nextState;
        currentState.Start();
    }

}

public interface EnemyState
{
    public void Start(); // If state is finite, start method should call SwitchState() on return
    public void Stop(); // Called by SwitchState() to clean up resources
    public void Update(); // Method for updating on every frame
}

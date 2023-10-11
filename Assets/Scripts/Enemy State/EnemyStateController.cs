using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for state controllers for enemy AI
public class EnemyStateController : MonoBehaviour
{
    // Enemy-Specific States should be declared in subclass objects, not within this class.
    public Dictionary<EnemyState, int> weightTable; // Table of probability weights for each state,
                                                    // should be instantiated by subclasses of this class.
    public EnemyState currentState;
    private EnemyState nextstate; // Reserved for Tick()

    void Start()
    {
        StartCoroutine(Tick());
        SwitchState(currentState);
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
        
            bool shouldSwitch = currentState.StateTick(); // Tick will return a bool for whether a switch should be attempted
            if (!shouldSwitch) yield return null;

            EnemyState nextState = currentState; // Calculate next state based on weighted probabilities
            int weightSum = 0;
            foreach (KeyValuePair<EnemyState, int> state in weightTable)
            {
                weightSum += state.Value;
            }
            int randomWeight = UnityEngine.Random.Range(0, weightSum);
            foreach (KeyValuePair<EnemyState, int> state in weightTable)
            {
                randomWeight -= state.Value;
                if (randomWeight < 0)
                {
                    nextState = state.Key;
                    break;
                }
            }

            if (nextstate != currentState) SwitchState(nextstate);
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
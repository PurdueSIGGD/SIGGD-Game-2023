using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for state controllers for enemy AI
public class EnemyStateController : MonoBehaviour
{
    // Enemy-Specific States should be declared in subclass objects, not within this class.
    public Dictionary<EnemyState, float> weightTable; // Table of probability weights for each state,
                                                    // should be instantiated by subclasses of this class.
    public EnemyState currentState;

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
    IEnumerator Tick() //TODO change to method called by a synchronized tick method??
    {
        EnemyState nextState;
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            nextState = currentState; // Randomly select next state based on weighted probabilities
            bool shouldSwitch = currentState.StateTick(); // Tick will return a bool for whether a switch should be attempted
            if (!shouldSwitch) yield return null;

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

            if (nextState != currentState) SwitchState(nextState);
        }
    }

    // Stops the current state and begins the new state specified by argument.
    // Should be called from coroutine for finite states (e.g. attacks). (Beware of recursive calls!)
    // and from Tick() for non-finite states (e.g. idle, patrol)
    public void SwitchState(EnemyState nextState)
    {
        currentState?.StateStop();
        currentState = nextState;
        currentState.StateStart();
    }
}
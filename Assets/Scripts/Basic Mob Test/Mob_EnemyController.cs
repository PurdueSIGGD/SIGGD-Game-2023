using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_EnemyController : MonoBehaviour 
{
    public GameObject player;
    
    public Dictionary<EnemyState, float> weightTable;
	
	public EnemyState idleState;
	public EnemyState followState;
	public EnemyState attackState;

	public EnemyState currentState;

    void Awake()
    {
        weightTable = new Dictionary<EnemyState, float> { };
        weightTable[idleState] = 0.0f;
        weightTable[followState] = 0.0f;
        weightTable[attackState] = 0.0f;
    }

	void Start() 
	{
		StartCoroutine(Tick());
		SwitchState(currentState);
	}

	void Update() 
	{
		currentState.StateUpdate();
	}

    public void SwitchState(EnemyState nextState)
    {
        currentState?.StateStop();
        currentState = nextState;
        currentState.StateStart();
    }

	public IEnumerator Tick()
    {
        EnemyState nextState;
        while (true)
        {
			yield return new WaitForSeconds(0.25f);

			nextState = currentState; 

            bool shouldSwitch = currentState.StateTick(); // Tick will return a bool for whether a switch should be attempted
            if (shouldSwitch) // Randomly select next state based on weighted probabilities
			{
                float weightSum = 0;
                foreach (KeyValuePair<EnemyState, float> state in weightTable)
                {
                    weightSum += state.Value;
                }
                float randomWeight = UnityEngine.Random.Range(0, weightSum);
                foreach (KeyValuePair<EnemyState, float> state in weightTable)
                {
                    randomWeight -= state.Value;
                    if (randomWeight <= 0)
                    {
                        nextState = state.Key;
                        break;
                    }
                }

                if (nextState != currentState) SwitchState(nextState);
            }
		}
	}
} 
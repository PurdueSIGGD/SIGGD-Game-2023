using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : EnemyStateController
{
    public EnemyState idleState;
    public EnemyState followState;

    public GameObject Player;

    void Awake()
    {
        weightTable = new Dictionary<EnemyState, float> { };
        weightTable[idleState] = 0.0f;
        weightTable[followState] = 0.0f;

        Debug.Log(idleState);
    }
}

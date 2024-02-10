using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateController : MonoBehaviour
{
    private Dictionary<string, EnemyState> stateDictionary;

    EnemyState currentState;
    string currentStateName;
}
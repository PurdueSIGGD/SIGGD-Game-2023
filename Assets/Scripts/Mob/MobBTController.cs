using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTController : MonoBehaviour
{
    private MobNavigationController navigationController;
    private MobTargetingController targetingController;

    private MobBTTree activeTree;

    private Dictionary<string, bool> blackboard;

    private void InitializeBlackboard()
    {

    }

    private bool shouldEvaluate;
    private void OnConditionalUpdate(string key, bool val)
    {
        blackboard[key] = val;
        shouldEvaluate = true;
    }
        
    private void ConnectConditionals()
    {

    }





    private EnemyState currentState;
    private IEnumerator currentTask;
    int overridePriority;

    private Dictionary<string, EnemyState> stateDict;

    private void InitializeStateDict()
    {

    }
    
    private void AttemptSwitch()
    {
        string stateName = activeTree.Evaluate(gameObject, blackboard);

        EnemyState nextState = stateDict[stateName];
        if (nextState == null || nextState.GetPriority() < overridePriority) return;

        SwitchState(nextState);
    }

    private void SwitchState(EnemyState nextState)
    {
        if (currentTask != null) StopCoroutine(currentTask);
        currentState.ExitState();

        currentState = nextState;

        currentTask = currentState.EnterState();
        StartCoroutine(currentTask);
    }





    void Awake()
    {
        InitializeBlackboard();
        InitializeStateDict();
    }

    void Start()
    {
        navigationController = GetComponent<MobNavigationController>();
        targetingController = GetComponent<MobTargetingController>();

        ConnectConditionals();
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        if (shouldEvaluate) AttepmtSwitch();
        shouldEvaluate = false;
    }
}
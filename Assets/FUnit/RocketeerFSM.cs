using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketeerFSM : Unit
{
    public UnitState currentState;
    public RocketeerIdle idleState = new RocketeerIdle();
    public RocketeerFire fireState = new RocketeerFire();

    // Serialize Fields
    [Header("Unit Fields")]

    [SerializeField]
    public float fireCooldown;

    [SerializeField]
    public float range;

    [SerializeField]
    public GameObject gunObj;

    [SerializeField]
    public GameObject bulletPoint;

    [SerializeField]
    public LayerMask projMask;

    [SerializeField]
    public GameObject projPrefab;


    // Context Variables
    public GameObject target;
    public float cooldown;

    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown < 0) cooldown = 0;

        currentState.UpdateState(this);
    }

    public void SwitchState(UnitState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

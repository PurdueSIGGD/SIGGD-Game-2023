using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealerFSM : Unit
{
    public UnitState currentState;
    public HealerIdle idleState = new HealerIdle();
    public HealerPulse pulseState = new HealerPulse();

    // Serialize Fields

    [Header("Unit Fields")]

    [SerializeField]
    public float healAmount;

    [SerializeField]
    public float lightAmount;

    [SerializeField]
    public float cooldown;

    [SerializeField]
    public Animator animator;

    // Private Fields
    [NonSerialized] public float pulseTime;
    [NonSerialized] public GameObject player;


    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        pulseTime -= Time.deltaTime;
        if (pulseTime < 0) pulseTime = 0;

        currentState.UpdateState(this);
    }

    public void SwitchState(UnitState state)
    {
        currentState = state; 
        currentState.EnterState(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MineFSM : Unit
{
    public static UnitState currentState;
    public static MineIdle idleState = new MineIdle();
    public static MineTrigger triggerState = new MineTrigger();
    public static MineBoom boomState = new MineBoom();

    // Serialize Fields

    [Serializable]
    public struct MineConfig
    {
        public float triggerRadius;
        public float blastRadius;
        public float blastDmg;
        public LayerMask blastMask;
        public Animator animator;
    }

    [SerializeField]
    public MineConfig config;

    public struct MinePersonal
    {
        public float time;
    }

    [NonSerialized]
    public MinePersonal personal = new MinePersonal();

    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    public void SwitchState(UnitState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

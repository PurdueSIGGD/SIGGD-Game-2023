using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MineFSM : Unit
{
    public UnitState currentState;
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
        public AudioSource triggerSound;
        public AudioSource blastSound;
        public AudioSource placementSound;
    }

    [SerializeField]
    public MineConfig config;

    public struct MinePersonal
    {
        public float time;
        public bool armed;
    }

    [NonSerialized]
    public MinePersonal personal = new MinePersonal();

    private void Start()
    {
        config.placementSound.Play();
        personal.armed = false;
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

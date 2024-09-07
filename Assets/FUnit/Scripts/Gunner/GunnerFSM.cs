using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerFSM : Unit
{
    public UnitState currentState;
    public static GunnerIdle idleState = new GunnerIdle();
    public static GunnerFire fireState = new GunnerFire();

    // Serialize Fields
    [Serializable]
    public struct GunnerConfig
    {
        public float fireCooldown;
        public float range;
        public int burstCount;
        public float burstDuration;
        public GameObject gunObj;
        public GameObject bulletPoint;
        public LayerMask projMask;
        public GameObject projPrefab;
        public Animator animator;
    }

    [SerializeField]
    public GunnerConfig config;

    public struct GunnerPersonal
    {
        public GameObject target;
        public bool reloaded;
    }

    [NonSerialized]
    public GunnerPersonal personal;

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

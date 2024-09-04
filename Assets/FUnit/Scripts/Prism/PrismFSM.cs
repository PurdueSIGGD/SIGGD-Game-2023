using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class PrismFSM : Unit
{
    public UnitState currentState;
    public static PrismIdle idleState = new PrismIdle();

    // Serialize Fields

    [Serializable]
    public struct PrismConfig
    {
        public GameObject splitProjPrefab;
        public int splitCount;
        public float splitLife;
        public float angleBetweenSplits;
        public AudioSource splitSound;
        public AudioSource placementSound;
    }

    [SerializeField]
    public PrismConfig config;

    private void Start()
    {
        config.placementSound.Play();
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

using System;
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

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public AudioSource fireSound;

    [SerializeField]
    public AudioSource placementSound;


    // Context Variables
    [NonSerialized] public GameObject target;
    [NonSerialized] public float fireTime;

    void Start()
    {
        placementSound.Play();
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        fireTime -= Time.deltaTime;
        if (fireTime < 0) fireTime = 0;

        currentState.UpdateState(this);
    }

    public void SwitchState(UnitState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

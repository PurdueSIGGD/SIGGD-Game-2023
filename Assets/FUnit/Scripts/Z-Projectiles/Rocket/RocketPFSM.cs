using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RocketPFSM : MonoBehaviour
{
    public ProjState currentState;
    public static RocketBoom boomState = new RocketBoom();
    public static RocketFly flyState = new RocketFly();

    // Serialize Fields

    [Serializable]
    public struct RocketConfig
    {
        public float duration;
        public float damage;
        public float speed;
        public float dmgRadius;
        public LayerMask layerMask;
        public Animator animator;
        public GameObject dirSprite;
        public AudioSource blastSound;
    }

    [SerializeField]
    public RocketConfig config;

    public struct RocketPersonal
    {
        public GameObject target;
        public Vector3 direction;
        public float time;
    }

    [NonSerialized]
    public RocketPersonal personal = new RocketPersonal();


    private void Start()
    {
        currentState = flyState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);

        if (personal.time > config.duration + 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    public void SwitchState(ProjState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

}

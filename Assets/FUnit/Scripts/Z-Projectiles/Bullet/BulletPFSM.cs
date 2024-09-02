using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPFSM : MonoBehaviour
{
    public ProjState currentState;
    public static BulletFly flyState = new BulletFly();

    // Serialize Fields

    [Serializable]
    public struct BulletConfig
    {
        public float duration;
        public float damage;
        public float speed;
        public GameObject dirSprite;
    }

    [SerializeField]
    public BulletConfig config;

    public struct BulletPersonal
    {
        public GameObject target;
        public Vector3 direction;
        public float time;
    }

    [NonSerialized]
    public BulletPersonal personal = new BulletPersonal();


    private void Start()
    {
        currentState = flyState;
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

    public void SwitchState(ProjState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

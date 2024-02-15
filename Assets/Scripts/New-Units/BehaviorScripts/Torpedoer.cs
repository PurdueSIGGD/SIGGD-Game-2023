using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Stationary))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Torpedoer : Attack
{
    // -- Private Fields --
    GameObject empObj;
    SphereCollider attackField;

    // -- Override Methods --

    void Start()
    {
        // Fetch Components of Unit
        this.RB = GetComponent<Rigidbody>();
        this.Collider = GetComponent<BoxCollider>();
        this.Movement = GetComponent<Stationary>();

        // Setup Colliders
        empObj = new GameObject("AttackField");
        empObj.transform.parent = this.transform;
        attackField = empObj.AddComponent<SphereCollider>();
        attackField.isTrigger = true;
        attackField.radius = Range;
        TriggerCallback callback = empObj.AddComponent<TriggerCallback>();
        callback.SetEnterCallback(this.gameObject, "EnterRange");
        callback.SetExitCallback(this.gameObject, "ExitRange");
    }

    void Update()
    {
    }

    public override void Aim()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject FindTarget()
    {
        throw new System.NotImplementedException();
    }

    public override void Fire()
    {
        throw new System.NotImplementedException();
    }

    // -- Callbacks --

    void EnterRange(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
            Debug.Log("Entered Range!");
    }

    void ExitRange(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
            Debug.Log("Exited Range!");
    }
    
}

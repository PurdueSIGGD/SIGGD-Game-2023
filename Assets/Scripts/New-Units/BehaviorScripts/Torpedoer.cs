using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedoer : Attack
{
    // Private Fields
     

    // Start is called before the first frame update
    void Start()
    {
        // Fetch Components of Unit
        this.RB = GetComponent<Rigidbody>();
        this.Collider = GetComponent<BoxCollider>();
        this.Movement = GetComponent<Stationary>();
    }

    // Update is called once per frame
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

    
}

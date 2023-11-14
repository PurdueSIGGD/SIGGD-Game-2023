using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTurret : Unit
{
    // Range
    int range = 4;

    // Collider for range detection
    SphereCollider sC;

    private void Start()
    {
        // Add a range collider
        sC = gameObject.AddComponent<SphereCollider>();
        sC.radius = range;
        sC.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] targets = { other.gameObject };
        Action(targets);
    }

    protected override void Action(GameObject[] targets)
    {
        throw new System.NotImplementedException();
    }
}

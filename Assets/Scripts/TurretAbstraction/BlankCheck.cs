using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCheck : MonoBehaviour
{
    [SerializeField] private TurretController tc;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("HIT!");
        tc.colliderBlocked = true;
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Exit!");
        tc.colliderBlocked = false;
    }
}

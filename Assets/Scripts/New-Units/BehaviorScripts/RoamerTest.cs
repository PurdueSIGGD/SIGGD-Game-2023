using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerTest : Unit
{
    private Attack roamerAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Movement = gameObject.AddComponent<Roamer>();
    }
}

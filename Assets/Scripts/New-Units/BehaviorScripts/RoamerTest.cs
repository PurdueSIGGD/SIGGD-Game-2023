<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerTest : Unit
{
    // Mana
    protected override float ManaCost => 3;
    protected override float SellCost => 1;

    // Health
    protected override float Health => 4;

    // Behavior
    protected override void DefineBehavior()
    {
        attack = gameObject.AddComponent<RoamerTestAttack>();
        movement = gameObject.AddComponent<RoamerMovement>();
        healthPoints = gameObject.AddComponent<UnitHealthPoints>();
        healthPoints.SetMaxHealth(Health);
    }

    // Awake
    private void Awake()
    {
        DefineBehavior();
    }
}
=======
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
>>>>>>> main

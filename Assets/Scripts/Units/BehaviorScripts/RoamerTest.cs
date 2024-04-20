using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerTest : Unit
{
    // Mana

    // Health

    // Behavior
    protected void DefineBehavior()
    {
        //attack = gameObject.AddComponent<RoamerTestAttack>();
        //movement = gameObject.AddComponent<RoamerMovement>();
        //healthPoints = gameObject.AddComponent<UnitHealthPoints>();
        //healthPoints.SetMaxHealth(Health);
    }

    // Awake
    private void Awake()
    {
        DefineBehavior();
    }
}

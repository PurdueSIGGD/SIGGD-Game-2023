using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : WeaponScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns true if the attack is successfully started, or false if it's not -- for example, if a
    // cooldown has not charged or the previous attack hasn't finished yet.
    new public bool PerformAttack() {
        // Call animation stuff, manipulate hitboxes, manipulate health values, etc.
        return true;
    }
}

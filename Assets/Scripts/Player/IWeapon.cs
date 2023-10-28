using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon {

    // Returns true if the attack is successfully started, or false if it's not -- for example, if a
    // cooldown has not charged or the previous attack hasn't finished yet.
    public bool PerformAttack(int attack);

    // Take out weapon (true) or put it away (false)
    // Enables or disables all physics and rendering components of the weapon, NOT scripts,
    // so cooldowns can continue running.
    // Copy this code, add any additional components that need to be addressed, and put it in the implemented method.
    public void SetEnabled(bool enabled);

}

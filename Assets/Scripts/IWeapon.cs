public interface IWeapon {

    // Returns true if the attack is successfully started, or false if it's not -- for example, if a
    // cooldown has not charged or the previous attack hasn't finished yet.
    public bool PerformAttack(int attack);

    // Take out weapon (true) or put it away (false)
    // Enables or disables all components of the weapon except for scripts, so cooldowns can continue running.
    public void SetEnabled(bool enabled);

}

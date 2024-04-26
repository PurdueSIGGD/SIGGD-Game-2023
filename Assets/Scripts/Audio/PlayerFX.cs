using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    public AudioSource playerFire;
    public AudioSource playerDamage;
    public AudioSource playerMelee;
    public AudioSource sonarPing;

    public void PlayerFire()
    {
        playerFire.Play();
    }

    public void PlayerDamage()
    {
        playerDamage.Play();
    }

    public void PlayerMelee()
    {
        playerMelee.Play();
    }

    public void SonarPing()
    {
        sonarPing.Play();
    }
}

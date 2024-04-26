using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSound : MonoBehaviour
{

    //this script is for calling one of three death sounds and nothing else
    // Start is called before the first frame update
    [SerializeField] private AudioSource[] possibleSounds;

    public void playDeathSound() {
        int rand = Random.Range(0, possibleSounds.Length);
        possibleSounds[rand].Play();
    }
}

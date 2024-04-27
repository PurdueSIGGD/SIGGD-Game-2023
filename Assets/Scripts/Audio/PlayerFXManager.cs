using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXManager : MonoBehaviour
{
    public static PlayerFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayFXClip(AudioClip audioClip, Transform spawnTransform, float volume, float startTime)
    {
        // Spawn in gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        
        // Assign audio clip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Assign clip start time
        audioSource.time = startTime; 

        // Play sound
        audioSource.Play();

        // Get length of audio clip
        float clipLength = audioSource.clip.length;

        // Destroy clip after playing
        Destroy(audioSource.gameObject, clipLength);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This idea is for this script to be attached to AudioSources which
// need to interact with the SoundSystem
[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour
{
    public bool audioState = true;
    [HideInInspector]
    public AudioSource audioSource;
    public SoundSystem.AudioSourceType audioSourceType;
    
    // Start is called before the first frame update
    private void Start()
    {
        var soundSystem = FindObjectOfType<SoundSystem>();
        audioSource = GetComponent<AudioSource>();
        
        // Add this reference to the SoundSystem
        soundSystem.AddSource(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // This allows the fader to fade the audio
    [HideInInspector]
    public float volumeFaderMod = 1;
    public AudioSource[] musicSources;
    [HideInInspector]
    public List<AudioSource> objectiveSources;
    [HideInInspector]
    public List<AudioSource> sirenSources;
    private AudioListener playerListener;
    private float sirenFade = 0;
    private float objectiveFade = 0;
    
    // Tutorial track
    
    // 3 x Music tracks tied to locations
    
    // Siren (overrides all)
    
    // Objective tracks (overrides standard)

    private void Start()
    {
        playerListener = FindObjectOfType<AudioListener>();
        objectiveSources = new List<AudioSource>();
        sirenSources = new List<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        var sirenOn = sirenSources.Count > 0;
        var objectiveOn = objectiveSources.Count > 0;
        
        // Update fade values
        sirenFade = Mathf.Clamp(sirenFade + (sirenOn ? 1 : -1) * Time.deltaTime, 0, 1);
        objectiveFade = Mathf.Clamp(objectiveFade + (objectiveOn ? 1 : -1) * Time.deltaTime, 0, 1);

        float leftover = 1;
        if (sirenOn)
        {
            leftover = Mathf.Clamp(leftover - sirenFade, 0, 1);
            DistanceNormalizeAudio(musicSources, leftover);
        }
        if (objectiveOn)
        {
            leftover = Mathf.Clamp(leftover - objectiveFade, 0, 1);
            DistanceNormalizeAudio(musicSources, leftover);
        }
        DistanceNormalizeAudio(musicSources, leftover);
    }

    private void DistanceNormalizeAudio(AudioSource[] audioSources, float mixAmount)
    {
        var playerTrans = playerListener.transform;
        // Calculate the total distance
        float totalDist = 0;
        foreach (var audioSource in audioSources)
        {
            totalDist += Vector3.Distance(audioSource.transform.position, playerTrans.position);
        }
        // Set the volume of each music source
        foreach (var audioSource in audioSources)
        {
            var distance = Vector3.Distance(audioSource.transform.position, playerTrans.position);
            var ratio = distance / totalDist;
            audioSource.volume = ratio * volumeOverrideMod * volumeFaderMod * mixAmount;
        }
    }
    
}

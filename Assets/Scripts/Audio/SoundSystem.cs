using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // This allows the fader to fade the audio
    [HideInInspector]
    public float volumeFaderMod = 1;

    [SerializeField] private float fadeInSeconds = 1;
    [SerializeField] private float fadeOutSeconds = 4;
    private List<AudioSourceController> musicSources = new List<AudioSourceController>();
    private List<AudioSourceController> objectiveSources = new List<AudioSourceController>();
    private List<AudioSourceController> sirenSources = new List<AudioSourceController>();
    private AudioListener playerListener;
    private float sirenFade = 0;
    private float objectiveFade = 0;
    
    public enum AudioSourceType
    {
        Music,
        Objective,
        Siren,
    }
    
    // Saves an AudioSourceController in the correct list
    public void AddSource(AudioSourceController audioSourceController)
    {
        switch (audioSourceController.audioSourceType)
        {
            case AudioSourceType.Music:
                musicSources.Add(audioSourceController);
                break;
            case AudioSourceType.Objective:
                objectiveSources.Add(audioSourceController);
                break;
            case AudioSourceType.Siren:
                sirenSources.Add(audioSourceController);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Start()
    {
        playerListener = FindObjectOfType<AudioListener>();
        Debug.Log(musicSources[0].audioSource.clip.name);
    }

    // Update is called once per frame
    private void Update()
    {
        musicSources[0].audioSource.Play();

        var sirenOn = GetSourceOn(sirenSources);
        var objectiveOn = GetSourceOn(objectiveSources);
        
        // Update fade values
        sirenFade = Mathf.Clamp(sirenFade + (sirenOn ? 1 / fadeInSeconds : -1 / fadeOutSeconds) * Time.deltaTime, 0, 1);
        objectiveFade = Mathf.Clamp(objectiveFade + (objectiveOn ? 1 / fadeInSeconds : -1 / fadeOutSeconds) * Time.deltaTime, 0, 1);

        // This allows the volume to be dynamically allocated to every tracked source
        float leftover = 1;
        
        leftover -= sirenFade;
        DistanceNormalizeAudio(sirenSources, sirenFade);
        
        var used = Mathf.Min(leftover, objectiveFade);
        leftover -= used;
        DistanceNormalizeAudio(objectiveSources, used);
        
        DistanceNormalizeAudio(musicSources, leftover);
    }

    // Check if any of these controllers have a source that is on
    private static bool GetSourceOn(IEnumerable<AudioSourceController> audioSourceControllers)
    {
        var found = false;
        foreach (var audioSourceController in audioSourceControllers)
        {
            if (audioSourceController.audioState)
            {
                found = true;
            }
        }

        return found;
    }

    // Applies volume to the controllers based on distance to the player
    private void DistanceNormalizeAudio(List<AudioSourceController> audioSourceControllers, float mixAmount)
    {
        var playerTrans = playerListener.transform;
        // Calculate the longest distance
        float longestDist = 0;
        var counter = 0;
        var values = new float[audioSourceControllers.Count];
        foreach (var audioSourceController in audioSourceControllers)
        {
            var dist = Vector3.Distance(audioSourceController.transform.position, playerTrans.position);
            if (dist > longestDist)
            {
                longestDist = dist;
            }

            values[counter] = dist;

            counter++;
        }
        // Calculate how far each value is from the largest value and get the sum
        float sumDist = 0;
        for (var i = 0; i < values.Length; i++)
        {
            values[i] = longestDist - values[i];
            sumDist += values[i];
        }
        
        // Calculate the final volume
        for (var i = 0; i < values.Length; i++)
        {
            var finalVolume = sumDist == 0 ? 1 : values[i] / sumDist;
            audioSourceControllers[i].audioSource.volume = finalVolume * volumeFaderMod * mixAmount;
        }
    }
    
}

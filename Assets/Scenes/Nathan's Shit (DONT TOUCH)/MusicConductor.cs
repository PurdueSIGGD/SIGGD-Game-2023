using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicConductor : MonoBehaviour
{


    [SerializeField] public MusicTrack titleTrack;
    [SerializeField] public MusicTrack tutorialTrack;
    [SerializeField] public MusicTrack lvl1Track;
    [SerializeField] public MusicTrack lvl2Track;
    [SerializeField] public MusicTrack lvl3Track;
    [SerializeField] public MusicTrack deapOceanBassTrack;
    [SerializeField] public MusicTrack sirenTrack;
    [SerializeField] public MusicTrack deathTrack;
    [SerializeField] public MusicTrack hummingTrack;
    [SerializeField] public MusicTrack nullTrack;

    [SerializeField] public float generalVolume = 0.4f;
    public bool isCrossfading = false;
    private MusicTrack activeTrack = null;

    [SerializeField] public bool crossfadeDEVTOOL = false;
    [SerializeField] public MusicTrackName crossfadeTrackDEVTOOL;
    [SerializeField] public float crossfadeOutTimeDEVTOOL = 5f;
    [SerializeField] public float crossfadeInDelayDEVTOOL = 5f;
    [SerializeField] public float crossfadeInTimeDEVTOOL = 5f;
    [SerializeField] public bool crossfadeInUnisonDEVTOOL = false;
    //private bool tutorialPlayingDEVTOOL = true;

    private bool isGeneralVolumeFading = false;





    // Start is called before the first frame update
    void Start()
    {
        // Crossfade test DEV TOOL
        activeTrack = nullTrack;
        nullTrack.playTrack(generalVolume, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (deapOceanBassTrack.isPlaying || lvl1Track.isPlaying || lvl2Track.isPlaying || lvl3Track.isPlaying || sirenTrack.isPlaying || hummingTrack.isPlaying) //&& generalVolume < 0.4f) //&& !isGeneralVolumeFading)
        {
            if (generalVolume < 0.4f && !isCrossfading)
            {
                generalVolume = 0.4f;
                activeTrack.generalVolume = generalVolume;
            }
            //generalVolume = 0.4f;
            //StartCoroutine(fadeGeneralVolume(0.4f, 2f));
        } else if (generalVolume > 0.3f && !isCrossfading)// && !isGeneralVolumeFading)
        {
            generalVolume = 0.3f;
            activeTrack.generalVolume = generalVolume;
            //StartCoroutine(fadeGeneralVolume(0.3f, 2f));
        }

        /*
        // Crossfade test DEV TOOL
        if (crossfadeDEVTOOL)
        {
            crossfadeDEVTOOL = false;
            if (tutorialPlayingDEVTOOL)
            {
                crossfade(5f, lvl1Track, 1f, 7f);
                tutorialPlayingDEVTOOL = false;
            }
            else
            {
                crossfade(5f, tutorialTrack, 2.5f, 0f);
                tutorialPlayingDEVTOOL = true;
            }
        }
        */

        // Crossfade test DEV TOOL
        if (crossfadeDEVTOOL)
        {
            crossfadeDEVTOOL = false;
            crossfade(crossfadeOutTimeDEVTOOL, getMusicTrack(crossfadeTrackDEVTOOL), crossfadeInTimeDEVTOOL, crossfadeInDelayDEVTOOL, crossfadeInUnisonDEVTOOL);
        }
    }





    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*public void loop(AudioSource fadeOutTrack, AudioSource fadeInTrack, float fadeTime)
    {
        StartCoroutine(loopFader(fadeOutTrack, fadeInTrack, fadeTime));
    }

    private IEnumerator loopFader(AudioSource fadeOutTrack, AudioSource fadeInTrack, float fadeTime)
    {
        fadeInTrack.Play();
        fadeInTrack.time = loopStartTime;
        //Debug.Log("MUSIC lOOPER: track2 playing - Time: " + track2.time);
        isLoopFading = true;
        StartCoroutine(fade(fadeOutTrack, false, fadeTime));
        StartCoroutine(fade(fadeInTrack, true, fadeTime / 2f));
        yield return new WaitForSeconds(fadeTime);
        isLoopFading = false;
        //Debug.Log("MUSIC LOOPER: Fading complete.");
    }

    private IEnumerator fade(AudioSource fadeTrack, bool fadeIn, float fadeTime)
    {
        if (fadeIn)
        {
            Debug.Log("MUSIC LOOPER: track2 Fading in...");
        }
        else
        {
            Debug.Log("MUSIC LOOPER: track1 Fading out...");
        }

        float fadeSteps = 25f;
        float fadeDirection = (fadeIn) ? 1 : -1;
        for (int i = 0; i < fadeSteps; i++)
        {

            fadeTrack.volume += fadeDirection * generalVolume / fadeSteps;
            yield return new WaitForSeconds(fadeTime / fadeSteps);
        }

    }*/
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    /// <summary>
    /// Fades out the active music track and fades in the specified fade-in music track.
    /// </summary>
    /// <param name="fadeOutTime">
    /// The time, in seconds, that it will take to fade out the active music track.
    /// </param>
    /// <param name="fadeInTrack">
    /// The new music track to fade in.
    /// </param>
    /// <param name="fadeInTime">
    /// The time, in seconds, that it will take to fade in the new music track.
    /// </param>
    /// <param name="fadeInDelay">
    /// The time, in seconds, that will pass before the new music track begins fading in.
    /// </param>
    /// <param name="startTime">
    /// The time, in seconds, at which the new music track will start playing.
    /// </param>
    public void crossfade(float fadeOutTime, MusicTrack fadeInTrack, float fadeInTime, float fadeInDelay, float startTime)
    {
        StartCoroutine(crossfadeCoroutineHelper(fadeOutTime, fadeInTrack, fadeInTime, fadeInDelay, false, startTime));
    }


    /// <summary>
    /// Fades out the active music track and fades in the specified fade-in music track.
    /// </summary>
    /// <param name="fadeOutTime">
    /// The time, in seconds, that it will take to fade out the active music track.
    /// </param>
    /// <param name="fadeInTrack">
    /// The new music track to fade in.
    /// </param>
    /// <param name="fadeInTime">
    /// The time, in seconds, that it will take to fade in the new music track.
    /// </param>
    /// <param name="fadeInDelay">
    /// The time, in seconds, that will pass before the new music track begins fading in.
    /// </param>
    /// <param name="inUnison">
    /// If true, the new music track will start playing at the current time of the active music track.
    /// </param>
    public void crossfade(float fadeOutTime, MusicTrack fadeInTrack, float fadeInTime, float fadeInDelay, bool inUnison)
    {
        StartCoroutine(crossfadeCoroutineHelper(fadeOutTime, fadeInTrack, fadeInTime, fadeInDelay, inUnison, 0f));
    }

    private IEnumerator crossfadeCoroutineHelper(float fadeOutTime, MusicTrack fadeInTrack, float fadeInTime, float fadeInDelay, bool inUnison, float startTime)
    {
        isCrossfading = true;

        StartCoroutine(fade(activeTrack, false, fadeOutTime, inUnison, startTime));
        //activeTrack.stopPlayingTrack();
        yield return new WaitForSeconds(fadeInDelay);
        //fadeInTrack.playTrack(0f);
        StartCoroutine(fade(fadeInTrack, true, fadeInTime, inUnison, startTime));
        //activeTrack = fadeInTrack;

        yield return new WaitForSeconds((fadeOutTime - fadeInDelay > fadeInTime) ? fadeOutTime - fadeInDelay : fadeInTime);
        activeTrack = fadeInTrack;
        isCrossfading = false;
    }

    private IEnumerator fade(MusicTrack fadeTrack, bool fadeIn, float fadeTime, bool inUnison, float startTime)
    {
        fadeTrack.isBeingConducted = true;
        if (fadeIn)
        {
            fadeTrack.playTrack(0f, (inUnison) ? activeTrack.time : startTime);
        }

        float fadeSteps = 25f;
        float fadeDirection = (fadeIn) ? 1 : -1;
        for (int i = 0; i < fadeSteps; i++)
        {

            fadeTrack.generalVolume += fadeDirection * generalVolume / fadeSteps;
            yield return new WaitForSeconds(fadeTime / fadeSteps);
        }

        if (!fadeIn)
        {
            fadeTrack.stopPlayingTrack();
        }
        fadeTrack.isBeingConducted = false;
    }




    private IEnumerator fadeGeneralVolume(float newVolume, float fadeTime)
    {
        isGeneralVolumeFading = true;
        float fadeSteps = 25f;
        for (int i = 0; i < fadeSteps; i++)
        {

            generalVolume += (newVolume - generalVolume) / fadeSteps;
            yield return new WaitForSeconds(fadeTime / fadeSteps);
        }
        isGeneralVolumeFading = false;
    }





    public MusicTrack getMusicTrack(MusicTrackName trackName)
    {
        switch (trackName)
        {
            case MusicTrackName.TITLE:
                return titleTrack;
            case MusicTrackName.TUTORIAL:
                return tutorialTrack;
            case MusicTrackName.LVL1:
                return lvl1Track;
            case MusicTrackName.LVL2:
                return lvl2Track;
            case MusicTrackName.LVL3:
                return lvl3Track;
            case MusicTrackName.DEEP_OCEAN_BASS:
                return deapOceanBassTrack;
            case MusicTrackName.SIREN:
                return sirenTrack;
            case MusicTrackName.DEATH:
                return deathTrack;
            case MusicTrackName.NULL:
                return nullTrack;
            case MusicTrackName.HUMMING:
                return hummingTrack;
            default:
                return null;
        }
    }





    public enum MusicTrackName
    {
        TITLE,
        TUTORIAL,
        LVL1,
        LVL2,
        LVL3,
        DEEP_OCEAN_BASS,
        SIREN,
        DEATH,
        HUMMING,
        NULL
    }


}

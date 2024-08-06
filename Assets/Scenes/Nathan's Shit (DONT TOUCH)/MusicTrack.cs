using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrack : MonoBehaviour
{


    [SerializeField] public float generalVolume = 0.4f;
    [SerializeField] public AudioSource track1;
    [SerializeField] public AudioSource track2;
    [SerializeField] public bool loops = true;
    [SerializeField] public float loopStartTime = 17.3f;
    [SerializeField] public float loopEndTime = 125.2f;
    [SerializeField] public float loopFadeTime = 5f;
    public bool isLoopFading = false;
    public bool isTrack1Active = true;
    public bool isBeingConducted = false;
    public bool isPlaying = false;
    public float time = 0f;





    // Start is called before the first frame update
    void Start()
    {
        /*
        //DEV TESTING TOOL
        track1.volume = generalVolume;
        track2.volume = 0f;
        track1.Play();
        isPlaying = true;
        //
        */
    }





    // Update is called once per frame
    void Update()
    {

        time = (isTrack1Active) ? track1.time : track2.time;

        // Volume Cap
        if (track1.volume > generalVolume)
        {
            track1.volume = generalVolume;
        }

        if (track2.volume > generalVolume)
        {
            track2.volume = generalVolume;
        }



        //Conductor Volume Fading
        if (isBeingConducted && isTrack1Active && !isLoopFading)
        {
            track1.volume = generalVolume;
        }

        if (isBeingConducted && !isTrack1Active && !isLoopFading)
        {
            track2.volume = generalVolume;
        }



        // Looper
        if (loops && track1.time >= loopEndTime && isTrack1Active && !isLoopFading)
        {
            //Debug.Log("MUSIC LOOPER: Update trigger tripped");
            isTrack1Active = false;
            loop(track1, track2, loopFadeTime);
        }

        if (loops && track2.time >= loopEndTime && !isTrack1Active && !isLoopFading)
        {
            isTrack1Active = true;
            loop(track2, track1, loopFadeTime);
        }
    }





    public void playTrack(float initialVolume, float initialTime)
    {
        if (isPlaying)
        {
            return;
        }
        isTrack1Active = true;
        generalVolume = initialVolume;
        track1.volume = generalVolume;
        track2.volume = 0f;
        track1.Play();
        track1.time = initialTime;
        isPlaying = true;
    }

    public void stopPlayingTrack()
    {
        if (!isPlaying)
        {
            return;
        }
        track1.Stop();
        track2.Stop();
        isPlaying = false;
    }





    public void loop(AudioSource fadeOutTrack, AudioSource fadeInTrack, float fadeTime)
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
        /*if (fadeIn)
        {
            Debug.Log("MUSIC LOOPER: track2 Fading in...");
        }
        else
        {
            Debug.Log("MUSIC LOOPER: track1 Fading out...");
        }*/

        float fadeSteps = 25f;
        float fadeDirection = (fadeIn) ? 1 : -1;
        for (int i = 0; i < fadeSteps; i++)
        {

            fadeTrack.volume += fadeDirection * generalVolume / fadeSteps;
            yield return new WaitForSeconds(fadeTime / fadeSteps);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] public float generalVolume = 0.4f;


    [SerializeField] public AudioSource musicTrack1;
    [SerializeField] public AudioSource musicTrack2;
    [SerializeField] public float musicLoopStart = 38;
    [SerializeField] public float musicLoopEnd = 125;
    [SerializeField] public float musicCrossfadeDuration = 1f;
    public bool isCrossfading = false;

    public bool track1Active = true;

    // Start is called before the first frame update
    void Start()
    {
        musicTrack1.volume = generalVolume;
        musicTrack2.volume = 0f;
        musicTrack1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicTrack1.time >= musicLoopEnd && track1Active && !isCrossfading)
        {
            track1Active = false;
            musicTrack2.Play();
            musicTrack2.time = musicLoopStart;
            //crossfade(musicTrack1, musicTrack2, musicCrossfadeDuration);
            hardfade(musicTrack1, musicTrack2, musicCrossfadeDuration);
        }

        if (musicTrack2.time >= musicLoopEnd && !track1Active && !isCrossfading)
        {
            track1Active = true;
            musicTrack1.Play();
            musicTrack1.time = musicLoopStart;
            //crossfade(musicTrack2 , musicTrack1, musicCrossfadeDuration);
            hardfade(musicTrack2, musicTrack1, musicCrossfadeDuration);
        }
    }



    public void crossfade(AudioSource fadeOutTrack, AudioSource fadeInTrack, float duration)
    {
        StartCoroutine(crossfadeCoroutine(fadeOutTrack, fadeInTrack, duration));
    }

    public void hardfade(AudioSource fadeOutTrack, AudioSource fadeInTrack, float duration)
    {
        StartCoroutine(hardfadeCoroutine(fadeOutTrack, fadeInTrack, duration));
    }


    private IEnumerator crossfadeCoroutine(AudioSource fadeOutTrack, AudioSource fadeInTrack, float duration)
    {
        isCrossfading = true;
        for (int i = 0; i < 25; i++)
        {
            fadeOutTrack.volume -= generalVolume * 0.04f;
            fadeInTrack.volume += generalVolume * 0.04f;
            yield return new WaitForSeconds(duration / 25f);
        }
        isCrossfading = false;
    }

    private IEnumerator hardfadeCoroutine(AudioSource fadeOutTrack, AudioSource fadeInTrack, float duration)
    {
        isCrossfading = true;
        //fadeInTrack.volume = generalVolume;
        for (int i = 0; i < 25; i++)
        {
            fadeOutTrack.volume -= generalVolume * 0.02f;
            fadeInTrack.volume += generalVolume * 0.04f;
            yield return new WaitForSeconds(duration / 50f);
        }
        for (int i = 0; i < 25; i++)
        {
            fadeOutTrack.volume -= generalVolume * 0.02f;
            yield return new WaitForSeconds(duration / 50f);
        }
        isCrossfading = false;
    }
}

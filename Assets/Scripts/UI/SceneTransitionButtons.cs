using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButtons : MonoBehaviour
{
    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    private IEnumerator QuitGameCoroutine()
    {
        StartFade(Color.black, 1);
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        musicConductor.crossfade(1f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    public void NewGame()
    {
        StartCoroutine(NewGameCoroutine());
    }
    
    private IEnumerator NewGameCoroutine()
    {
        SaveManager.ClearSave();
        StartFade(Color.black, 3);
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        musicConductor.crossfade(3f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("World Systems");
        SceneManager.LoadScene("Music Rework Test");
    }
    
    public void ContinueGame()
    {
        StartCoroutine(ContinueCoroutine());
    }

    private IEnumerator ContinueCoroutine()
    {
        StartFade(Color.black, 3);
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        musicConductor.crossfade(3f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("World Systems");
        SceneManager.LoadScene("Music Rework Test");
        Debug.Log("a");
    }

    private void StartFade(Color color, float time)
    {
        var fader = FindObjectOfType<Fader>();
        if (fader == null)
        {
            Debug.LogError("You need to drag the fader prefab into the scene");
        }
        fader.FadeOut(color, time);
    }
    
    public void Credits()
    {
        StartCoroutine(CreditsCoroutine());
    }
    
    private IEnumerator CreditsCoroutine()
    {
        StartFade(Color.black, 3);
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        musicConductor.crossfade(3f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("CreditsScene");
    }

    public void SmallTitle()
    {
        StartCoroutine(SmallTitleCoroutine());
    }

    private IEnumerator SmallTitleCoroutine()
    {
        StartFade(Color.black, 2);
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        musicConductor.crossfade(2f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Small Title Scene");
    }
}

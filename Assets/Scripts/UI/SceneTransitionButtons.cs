using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        StartCoroutine(NewGameCoroutine());
    }
    
    private IEnumerator NewGameCoroutine()
    {
        SaveManager.ClearSave();
        StartFade(Color.black, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Level");
    }
    
    public void ContinueGame()
    {
        StartCoroutine(ContinueCoroutine());
    }

    private IEnumerator ContinueCoroutine()
    {
        StartFade(Color.black, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Level");
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
        StartFade(Color.black, 1);
        yield return new WaitForSeconds(1);
        //SceneManager.LoadScene("Main Level");
    }
}

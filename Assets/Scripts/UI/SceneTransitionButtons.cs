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
        SaveManager.ClearSave();
        SceneManager.LoadScene("Main Level");
    }
    
    public void ContinueGame()
    {
        SceneManager.LoadScene("Main Level");
    }
    
    public void Credits()
    {
        // Go to the credits scene
    }
}

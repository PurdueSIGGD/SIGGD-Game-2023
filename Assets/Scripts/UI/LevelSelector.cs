using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject selectorObj;
    [SerializeField]
    private List<string> scenes;
    private int currentScenePointer;

    // Start is called before the first frame update
    void Start()
    {
        var levelIdx = 1;
        foreach (var scene in scenes) {
            var sceneButton = Instantiate(selectorObj, transform);
            sceneButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + levelIdx++;
            sceneButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(scene));
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(scenes[currentScenePointer]);
    }

    public void NextLevel() {
        SceneManager.LoadScene(scenes[currentScenePointer++]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

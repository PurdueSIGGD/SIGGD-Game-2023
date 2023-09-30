using System.Collections;
using System.Collections.Generic;
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
        foreach (var scene in scenes) {
            var sceneButton = Instantiate(selectorObj, transform);
            sceneButton.GetComponent<Button>().onClick.AddListener(() => print(scene));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas pauseCanvas;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseCanvas.enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            togglePause();
        }
    }

    public void togglePause() {
        if (paused) {
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
            mainCanvas.enabled = true;
        }
        else {
            Time.timeScale = 0;
            mainCanvas.enabled = false;
            pauseCanvas.enabled = true;
        }
        paused = !paused;
    }
}

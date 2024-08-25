using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePrompt : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text promptTextBox;

    // Start is called before the first frame update
    void Start()
    {
        promptTextBox.enabled = false;
        progressBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPrompt(string promptMessage)
    {
        promptTextBox.text = promptMessage.ToUpper();
        promptTextBox.enabled = true;
    }

    public void hidePrompt()
    {
        promptTextBox.enabled = false;
    }

    public void showProgressBar()
    {
        progressBar.value = 0;
        progressBar.gameObject.SetActive(true);
    }

    public void hideProgressBar()
    {
        progressBar.gameObject.SetActive(false);
    }

    /**
     * Sets the value of the progress in the progress bar
     * FROM 0 TO 1
     */
    public void setProgressBar(float progress)
    {
        progressBar.value = progress;
    }

    [ContextMenu("Do the thing")]
    public void testing1()
    {
        StartCoroutine(testing());
    }

    public IEnumerator testing()
    {
        Debug.Log("doin the thang");
        showPrompt("ello matey");
        showProgressBar();

        float i = 0;
        while (i < 1)
        {
            i += (float) (0.1 * Time.deltaTime);
            setProgressBar(i);
        }

        hidePrompt();
        hideProgressBar();

        yield return null;
    }
}

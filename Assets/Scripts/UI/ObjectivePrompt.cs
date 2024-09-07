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

    public void showProgressBar(float progress)
    {
        progressBar.value = progress;
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

    //TESTING
    /*
    [ContextMenu("Do the thing")]
    public void testing1()
    {
        StartCoroutine(testing());
    }

    public IEnumerator testing()
    {
        showPrompt("ello matey");
        showProgressBar();

        yield return new WaitForSeconds(1);

        float i = 0;
        while (i < 1)
        {
            i += 1f * Time.deltaTime;
            setProgressBar(i);
            yield return new WaitForSeconds(0.1f);
            Debug.Log(i);
        }

        //yield return new WaitForSeconds(3f);

        hidePrompt();
        hideProgressBar();

        yield return null;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private Image image;
    [SerializeField] private float fadeLength;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    private float fadeCounter = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponentInChildren<Image>();
    }

    // Creates a fade effect
    public void SetFader(Color newStartColor, Color newEndColor, float newFadeLength)
    {
        Debug.Log($"Starting new fade of length {newFadeLength}");
        fadeCounter = 0;
        fadeLength = newFadeLength;
        startColor = newStartColor;
        endColor = newEndColor;
    }

    public void FadeOut(Color newEndColor, float newFadeLength)
    {
        SetFader(new Color(0,0,0,0), newEndColor, newFadeLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeCounter < fadeLength)
        {
            fadeCounter += Time.deltaTime;
        }
        
        var newColor = Color.Lerp(startColor, endColor, fadeCounter / fadeLength);
        if (newColor.a == 0f || fadeCounter >= fadeLength * 1.5f)
        {
            image.gameObject.SetActive(false);
        }
        else
        {
            image.gameObject.SetActive(true);
            image.color = newColor;
        }
    }
}

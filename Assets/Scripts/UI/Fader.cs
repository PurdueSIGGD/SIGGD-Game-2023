using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
    }

    // Creates a fade effect
    public void SetFader(bool fadeIn, float fadeLength, Color startColor, Color endColor)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

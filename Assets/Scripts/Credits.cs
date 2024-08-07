using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public float initialFadeSpeed;
    public float finalFadeSpeed;
    public float logoFadeSpeed;
    public float scrollSpeed;

    private int alpha = 255;
    private Transform credits;
    private Image blackBg;
    private Image logo;

    // Start is called before the first frame update
    void Start()
    {
        credits = transform.GetChild(0).GetChild(0);
        blackBg = transform.GetChild(1).GetComponent<Image>();
        logo = transform.GetChild(2).GetComponent<Image>();
        StartCoroutine(scroll()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator scroll()
    {
        // Change the black screen to the skybox background
        while (alpha > 0)
        {
            alpha -= (int)(initialFadeSpeed * Time.deltaTime);
            if (alpha < 0)
                alpha = 0;
            Color blackBgColor = new Color32(0, 0, 0, (byte)alpha);
            blackBg.color = blackBgColor;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        // Start scrolling credits
        float y = credits.localPosition.y;
        while (credits.localPosition.y < 1300)
        {
            y += scrollSpeed * Time.deltaTime;
            credits.localPosition = new Vector3(0, y, 0);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        // Change the background back to black and show the SIGGD logo
        // Change the background to black
        while (alpha < 255)
        {
            alpha += (int) (finalFadeSpeed * Time.deltaTime);
            if (alpha > 255)
                alpha = 255;
            Color blackBgColor = new Color32(0, 0, 0, (byte) alpha);
            blackBg.color = blackBgColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // Show the SIGGD logo
        alpha = 0;
        while (alpha < 255)
        {
            alpha += (int) (logoFadeSpeed * Time.deltaTime);
            if (alpha > 255)
                alpha = 255;
            Color blackBgColor = new Color32(255, 255, 255, (byte) alpha);
            logo.color = blackBgColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // Remove the SIGGD logo
        while (alpha > 0)
        {
            alpha -= (int) (logoFadeSpeed * Time.deltaTime);
            if (alpha < 0)
                alpha = 0;
            Color blackBgColor = new Color32(255, 255, 255, (byte) alpha);
            logo.color = blackBgColor;
            yield return null;
        }
    }
}

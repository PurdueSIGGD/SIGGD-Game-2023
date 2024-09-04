using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class TitleIntro : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Image quoteBackground;
    [SerializeField] private TextMeshProUGUI quoteText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private List<Button> optionButtons;

    [SerializeField] public bool isDeathScene;

    public Color backgroundColor;
    public Color textColor;
    public Color titleColor;

    // Start is called before the first frame update
    void Start()
    {
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        if (isDeathScene)
        {
            musicConductor.crossfade(0f, musicConductor.deathTrack, 2f, 0f, 0f);
        }
        else
        {
            musicConductor.crossfade(0f, musicConductor.titleTrack, 0f, 0f, 0f);
        }
        backgroundColor = quoteBackground.color;
        backgroundColor.a = 1;
        quoteBackground.color = backgroundColor;
        textColor = quoteText.color;
        textColor.a = 0;
        quoteText.color = textColor;
        titleColor = titleText.color;
        titleColor.a = 0;
        titleText.color = titleColor;
        foreach (var optionButton in optionButtons)
        {
            var buttonText = optionButton.GetComponentInChildren<TextMeshProUGUI>();
            Color buttonColor = optionButton.image.color;
            Color optionColor = buttonText.color;
            buttonColor.a = 0;
            optionColor.a = 0;
            optionButton.image.color = buttonColor;
            buttonText.color = optionColor;
        }
        StartCoroutine(titleSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator titleSequence()
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 100; i++)
        {
            textColor.a += 0.01f;
            quoteText.color = textColor;
            //titleColor.a += 0.01f;
            //titleText.color = titleColor;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds((isDeathScene) ? 0f : 4f);

        if (!isDeathScene)
        {
            for (int i = 0; i < 200; i++)
            {
                backgroundColor.a -= 0.005f;
                quoteBackground.color = backgroundColor;
                textColor.a -= 0.005f;
                quoteText.color = textColor;
                titleColor.a += 0.01f;
                titleText.color = titleColor;
                yield return new WaitForSeconds(0.02f);
            }
        }

        foreach (var optionButton in optionButtons)
        {
            var buttonText = optionButton.GetComponentInChildren<TextMeshProUGUI>();
            Color buttonColor = optionButton.image.color;
            Color optionColor = buttonText.color;
            for (int i = 0; i < 25; i++)
            {
                buttonColor.a += 0.04f;
                optionColor.a += 0.04f;
                optionButton.image.color = buttonColor;
                buttonText.color = optionColor;
                yield return new WaitForSeconds(0.02f);
            }
        }

        if (isDeathScene)
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 200; i++)
            {
                backgroundColor.a -= 0.005f;
                quoteBackground.color = backgroundColor;
                textColor.a -= 0.005f;
                quoteText.color = textColor;
                //titleColor.a += 0.01f;
                //titleText.color = titleColor;
                yield return new WaitForSeconds(0.02f);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsEventDirector : MonoBehaviour
{

    [SerializeField] public CompanionMessages messanger;

    private string creditsSender = "Purdue SIGGD 2023-2024";

    private string credits1 = "- - - NAMELESS THINGS - - -";

    private string credits2 = "\n- - - PROGRAMMING TEAM - - - \n\n- Aiden Velleca. . . \n- Alex Boezer. . . \n- Andy Sharpe. . . \n- Ansh Agrawal. . . \n- Ivan Yang. . . \n- Jacob Aldridge. . . \n- Lance Salvador. . . \n- Lingyu Li. . . \n- Nathan Whitacre. . .";

    private string credits3 = "\n- - - DESIGN TEAM - - - \n\n- Alex Ashby. . . \n- Ansh Agrawal. . . \n- Ivan Yang. . . \n- Nathan Whitacre. . .";

    private string credits4 = "\n- - - ART TEAM - - - \n\n- Alex Ashby. . . \n- Andy Sliwa. . . \n- Ivan Yang. . . \n- Lance Salvador. . .";

    private string credits5 = "\n- - - AUDIO TEAM - - - \n\n- Andrew Hanlon. . . \n- Ivan Yang. . . \n- Jacob Aldridge. . . \n- Noah Henson. . .";

    private string credits6 = "Special thanks to all other SIGGD members and playtesters.";

    private string credits7 = "Extra special thanks to YOU for playing our game!";

    private string credits8 = "- - - NAMELESS THINGS - - -";

    [SerializeField] public Image logo;

    public float logoFadeSpeed;

    private int alpha = 255;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(creditSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator creditSequence()
    {
        var fader = FindObjectOfType<Fader>();
        fader.SetFader(Color.black, new Color(0, 0, 0, 0), 1f);
        yield return new WaitForSeconds(1f);
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        musicConductor.crossfade(0f, musicConductor.sirenTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(3f);
        yield return messanger.showMessage(credits1, creditsSender, false, true);
        yield return new WaitForSeconds(4.25f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.5f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits2, creditsSender, false, true);
        yield return new WaitForSeconds(2f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits3, creditsSender, false, true);
        yield return new WaitForSeconds(2f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits4, creditsSender, false, true);
        yield return new WaitForSeconds(2f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits5, creditsSender, false, true);
        yield return new WaitForSeconds(2f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits6, creditsSender, false, true);
        yield return new WaitForSeconds(2f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits7, creditsSender, false, true);
        yield return new WaitForSeconds(3f);
        messanger.hideMessage(true);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage("", creditsSender, false, true);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(credits8, creditsSender, false, true);
        yield return new WaitForSeconds(5f);
        fader.FadeOut(Color.black, 3f);
        musicConductor.crossfade(3f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("World Systems");

        // Show the SIGGD logo
        alpha = 0;
        while (alpha < 255)
        {
            alpha += (int)(logoFadeSpeed * Time.deltaTime);
            if (alpha > 255)
                alpha = 255;
            Color blackBgColor = new Color32(255, 255, 255, (byte)alpha);
            logo.color = blackBgColor;
            yield return null;
        }

        yield return new WaitForSeconds(4f);

        // Remove the SIGGD logo
        while (alpha > 0)
        {
            alpha -= (int)(logoFadeSpeed * Time.deltaTime);
            if (alpha < 0)
                alpha = 0;
            Color blackBgColor = new Color32(255, 255, 255, (byte)alpha);
            logo.color = blackBgColor;
            yield return null;
        }

        SceneManager.LoadScene("Title Scene");
    }
}

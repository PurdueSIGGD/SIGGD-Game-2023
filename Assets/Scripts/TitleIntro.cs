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
    [SerializeField] public bool isSmallTitle = false;

    public Color backgroundColor;
    public Color textColor;
    public Color titleColor;

    private string deathQuote1 = "That is not dead which can eternal lie,\nand with strange aeons, even death may die\n\nH. P. Lovecraft";

    private string deathQuote2 = "The boundaries which divide Life from Death are at best shadowy and vague. Who shall say where the one ends, and where the other begins?\n\nEdgar Allan Poe";

    private string deathQuote3 = "The dark side of life, and the horror of it, belonged to a world that lay remote from his own select little atmosphere of books and dreamings.\n\nAlgernon Blackwood";

    private string deathQuote4 = "And, suddenly, it came home to me that I was a little man in a little ship, in the midst of a very great sea.\n\nWilliam Hope Hodgson";

    private string deathQuote5 = "We have just begun to navigate a strange region; we must expect to encounter strange adventures, strange perils.\n\nArthur Machen";

    private string deathQuote6 = "Behind the scenes of life there is something pernicious that makes a nightmare of our world.\n\nThomas Ligotti";

    private string deathQuote7 = "The meaning of life is that it stops.\n\nFranz Kafka";

    private string deathQuote8 = "No live organism can continue for long to exist sanely\nunder conditions of absolute reality.\n\nShirley Jackson";

    private string deathQuote9 = "Ocean, n. A body of water occupying about two-thirds of a world made for man — who has no gills.\n\nAmbrose Bierce";

    private string deathQuote10 = "What on earth would a man do with himself,\nif something did not stand in his way?\n\nH. G. Wells";

    private string deathQuote11 = "The oldest and strongest emotion of mankind is fear, and the oldest and strongest kind of fear is fear of the unknown.\n\nH. P. Lovecraft";

    private string deathQuote12 = "We live on a placid island of ignorance in the midst\nof black seas of infinity, and it was not\nmeant that we should voyage far.\n\nH. P. Lovecraft";

    private string deathQuote13 = "Ocean is more ancient than the mountains, and freighted with the memories and the dreams of Time.\n\nH. P. Lovecraft";

    private string rareQuote1 = "GIBBAAAAAAAAAH\n\nGibby";

    private string rareQuote2 = "I'm going to turn into a truck now.\n\nOptimus Prime";

    private string rareQuote3 = "IMMA FIRIN' MAH LASAAAAAR\n\nBAAAAAAAAAAH";

    // Start is called before the first frame update
    void Start()
    {
        if (isDeathScene)
        {
            int deathQuoteNum = Random.Range(0, 13);
            switch (deathQuoteNum)
            {
                case 0:
                    quoteText.text = deathQuote1;
                    break;
                case 1:
                    quoteText.text = deathQuote2;
                    break;
                case 2:
                    quoteText.text = deathQuote3;
                    break;
                case 3:
                    quoteText.text = deathQuote4;
                    break;
                case 4:
                    quoteText.text = deathQuote5;
                    break;
                case 5:
                    quoteText.text = deathQuote6;
                    break;
                case 6:
                    quoteText.text = deathQuote7;
                    break;
                case 7:
                    quoteText.text = deathQuote8;
                    break;
                case 8:
                    quoteText.text = deathQuote9;
                    break;
                case 9:
                    quoteText.text = deathQuote10;
                    break;
                case 10:
                    quoteText.text = deathQuote11;
                    break;
                case 11:
                    quoteText.text = deathQuote12;
                    break;
                case 12:
                    quoteText.text = deathQuote13;
                    break;
                default:
                    quoteText.text = deathQuote1;
                    break;
            }
            int randomQuoteNum = Random.Range(0, 1000);
            switch (randomQuoteNum)
            {
                case 0:
                    quoteText.text = rareQuote1;
                    break;
                case 1:
                    quoteText.text = rareQuote2;
                    break;
                case 2:
                    quoteText.text = rareQuote3;
                    break;
                default:
                    break;
            }

        }
        MusicConductor musicConductor = FindObjectOfType<MusicConductor>();
        if (isDeathScene)
        {
            musicConductor.crossfade(0f, musicConductor.deathTrack, 2f, 0f, 0f);
        }
        else if (isSmallTitle)
        {
            musicConductor.crossfade(0f, musicConductor.titleTrack, 2f, 0f, musicConductor.titleTrack.loopStartTime);
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
        if (isSmallTitle)
        {
            StartCoroutine(smallTitleSequence());
        }
        else
        {
            StartCoroutine(titleSequence());
        }
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
            yield return new WaitForSeconds(4f);
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


    public IEnumerator smallTitleSequence()
    {
        yield return new WaitForSeconds(0.3f);

        /*
        for (int i = 0; i < 100; i++)
        {
            textColor.a += 0.01f;
            quoteText.color = textColor;
            //titleColor.a += 0.01f;
            //titleText.color = titleColor;
            yield return new WaitForSeconds(0.02f);
        }
        */

        //yield return new WaitForSeconds((isDeathScene) ? 0f : 4f);

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
                yield return new WaitForSeconds(0.01f);
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
            yield return new WaitForSeconds(4f);
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

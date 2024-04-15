using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialDirector : MonoBehaviour
{

    [SerializeField] public Movement playerMovement;
    [SerializeField] public EnemySpawner enemySpawner;


    [SerializeField] public InteractPrompt interactPrompt;
    [SerializeField] public CompanionMessages messanger;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Image fadeScreenImage;

    private string startUpSender = "ASP";

    private string startUpMessage1 = "- ASP-Assisted Exo-Suit startup initiated";

    private string startUpMessage2 = "- Exo-Suit boot protocols in progress... \n" +
                                     "- - - - - \n" +
                                     "- Electrostatic seals locked \n" +
                                     "- - - - - \n" +
                                     "- Life support systems online \n" +
                                     //"- - - - - \n" +
                                     //"- Haptic sensors engaged \n" +
                                     "- - - - - \n" +
                                     "- Neural link initialized \n" +
                                     "- - - - - \n\n\n" +
                                     "- Boot complete \n" +
                                     "- - - - - - - - - \n" +
                                     "- Establishing connection to SS Lawson... \n" +
                                     "- - - - - - - - - \n";

    //private string startUpMessage3 = "- Boot complete \n" +
    //"- - - - - \n" +
    //"- Bay 02 pressure normalized \n" +
    //"- - - - - \n" +
    //"- Bay 02 port lock released \n" +
    //"- - - - - \n" +
    //"- User controls engaged";
    //"- Establishing connection to SS Lawson... \n";
    //"- - - - - \n" +
    //"- Good luck, explorer.";

    private string startUpMessage4 = //"- EchoLight Mission 0239 Active \n" +
                                     //"- - - - - \n" +
                                     //"- Establishing connection to SS Lawson... \n" +
                                     //"- - - - - \n" +
                                     //"- Good luck, explorer.";
                                     "- Good luck, explorer.";



    private string tutorialSender = "Commanding Officer Robinson";

    private string tutorialMessage1 = "Kid, can you hear me alright?";

    private string tutorialMessage2 = "Good.";

    private string tutorialMessage3 = "Now, this mission's simple. Remember, all we're doin' is gettin' you aquainted with " +
                                     "your new ride.";

    private string tutorialMessage4 = "Make sure to take good care of 'er. As much as she costs, you'd be working in " +
                                     "indentured servitude for the rest of your natural life if you didn't bring 'er back " +
                                     "in one piece. Heh heh...";

    private string tutorialMessage5 = "Anywho, enough chit chat. Let's get a move on.";

    private string tutorialMessage6 = "I mean that literally. Move the Exo-suit around a bit. We gotta get you used " +
                                      "to steerin' this thing.";

    private string movementTestPrompt = "WASD | Move Exo-suit";


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startUpEvent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public IEnumerator showMessage(string message, string sender, bool slow)
    {
        yield return messanger.showMessage(message, sender, slow);
        //yield return new WaitForSeconds(0.01f);
    }*/


    public IEnumerator startUpEvent()
    {
        //ASP exo-suit start up
        fadeScreenImage.enabled = true;
        yield return new WaitForSeconds(1f);
        yield return messanger.showMessage("", startUpSender, false);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage(startUpMessage1, startUpSender, false);
        yield return new WaitForSeconds(2f);
        yield return messanger.showMessage(startUpMessage2, startUpSender, false);
        yield return new WaitForSeconds(0.75f);
        //yield return messanger.showMessage(startUpMessage3, startUpSender, false);
        //yield return new WaitForSeconds(1f);
        yield return messanger.showMessage("", startUpSender, false);
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage(startUpMessage4, startUpSender, false);
        yield return new WaitForSeconds(1.5f);
        messanger.hideMessage();

        //Fade into scene
        yield return new WaitForSeconds(0.5f);
        Color fadeScreenColor = fadeScreenImage.color;
        for (int i = 0; i < 100; i++)
        {
            fadeScreenColor.a -= 0.01f;
            fadeScreenImage.color = fadeScreenColor;
            yield return new WaitForSeconds(0.03f);
        }
        fadeScreenImage.enabled = false;

        //Robinson tutorial intro
        yield return new WaitForSeconds(1f);
        yield return messanger.showMessage("", tutorialSender, false);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage(tutorialMessage1, tutorialSender, false);
        yield return new WaitForSeconds(2f);
        yield return messanger.showMessage(tutorialMessage2, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(tutorialMessage3, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(tutorialMessage4, tutorialSender, false);
        yield return new WaitForSeconds(1.5f);
        yield return messanger.showMessage(tutorialMessage5, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(tutorialMessage6, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        interactPrompt.showPrompt(movementTestPrompt);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }
}

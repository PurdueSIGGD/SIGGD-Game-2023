using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal;

public class TutorialDirector : MonoBehaviour
{

    [SerializeField] public Movement playerMovement;
    [SerializeField] public LightResource playerLightResource;
    [SerializeField] public HealthPoints playerHealthPoints;
    [SerializeField] public EnemySpawner enemySpawner;


    [SerializeField] public InteractPrompt interactPrompt;
    [SerializeField] public CompanionMessages messanger;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Image fadeScreenImage;

    //public int sequencePhase;


    //START UP SEQUENCE --------------------------------------------------------------------
    public sequenceState startUpState = sequenceState.WAITING;

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



    //MOVEMENT TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState movementTutorialState = sequenceState.WAITING;

    private string movementTutorialPrompt = "WASD | Move Exo-suit";

    private string movementTutorialMessage1 = "There, see? Your a dern natural. And now that you can drive that thing, " +
                                              "it's time to put you to work.";

    private string movementTutorialMessage2 = "Remember, we're down here to collect two things: Light, and Echoes. First, let's " +
                                              "start lookin' for Light.";

    private string movementTutorialMessage3 = "Jet through this trench 'till you come across a bright glowin' orb on the ground. " +
                                              "They're damn near impossible to miss. You'll know one when you see it.";



    //LIGHT SEARCH SEQUENCE--------------------------------------------------------------------

    public bool lightSearchTrigger = false;

    public sequenceState lightSearchState = sequenceState.WAITING;

    private string lightSearchObjective = "Search for Light";

    private string lightSearchMessage1 = "Right there. See it? If you get close the suit will automatically absorb it. " +
                                         "Give it a try.";



    //LIGHT PICKUP SEQUENCE --------------------------------------------------------------------

    public sequenceState lightPickupState = sequenceState.WAITING;

    private string lightPickupObjective = "Collect the Light Orb";

    private string lightPickupMessage1 = "Pretty cool huh?";

    private string lightPickupMessage2 = "Well, hope you lined your whitey tighties with lead! If not that's " +
                                         "radiation poisoning in 20 years.";

    private string lightPickupMessage3 = "Yeah, the R&D boys told me the suit protects you from that stuff, " +
                                         "but I've never agreed with science before, and I'm not gonna start now!";

    private string lightPickupMessage4 = "That said, there's more down there you gotta collect. So keep lookin'.";



    //LIGHT HUNT SEQUENCE --------------------------------------------------------------------

    public sequenceState lightHuntState = sequenceState.WAITING;

    private string lightHuntObjective = "Search for Light";





    // Start is called before the first frame update
    void Start()
    {
        //startUpState = sequenceState.WAITING;
        //movementTutorialState = sequenceState.WAITING;
        //lightSearchState = sequenceState.WAITING;
        //lightPickupState = sequenceState.WAITING;

        //Start Up Trigger
        //lightSearchTrigger.enabled = false;
        StartCoroutine(startUpSequence());
    }

    // Update is called once per frame
    void Update()
    {

        //Movement Tutorial Trigger
        if (movementTutorialState == sequenceState.READY)
        {
            if ((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.A)) ||
                (Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.D)))
            {
                StartCoroutine(movementTutorialSequence());
            }
        }

        //Light Search Trigger
        if (lightSearchState == sequenceState.READY)
        {
            if (lightSearchTrigger)
            {
                lightSearchState = sequenceState.RUNNING;
                //lightSearchTrigger = false;
                StartCoroutine(lightSearchSequence());
            }
        }

        //Light Pickup Trigger
        if (lightPickupState == sequenceState.READY)
        {
            if (playerLightResource.currentLight > 0)
            {
                StartCoroutine(lightPickupSequence());
            }
        }

        //Light Hunt Trigger
        if (lightHuntState == sequenceState.READY)
        {
            if (playerLightResource.currentLight >= 50)
            {
                //StartCoroutine(lightHuntSequence());
            }
        }

    }



    //START UP SEQUENCE --------------------------------------------------------------------
    public IEnumerator startUpSequence()
    {
        startUpState = sequenceState.RUNNING;

        //ASP exo-suit start up
        playerMovement.enabled = false;
        fadeScreenImage.enabled = true;
        yield return new WaitForSeconds(1f);
        yield return messanger.showMessage("", startUpSender, false);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage(startUpMessage1, startUpSender, false);
        yield return new WaitForSeconds(2f);
        yield return messanger.showMessage(startUpMessage2, startUpSender, false);
        yield return new WaitForSeconds(0.25f);
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
        playerMovement.enabled = true;
        interactPrompt.showPrompt(movementTutorialPrompt);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        startUpState = sequenceState.COMPLETE;
        movementTutorialState = sequenceState.READY;
        //sequencePhase++;
    }



    //MOVEMENT TUTORIAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator movementTutorialSequence()
    {
        //sequencePhase++;
        movementTutorialState = sequenceState.RUNNING;

        yield return new WaitForSeconds(1.5f);
        interactPrompt.hidePrompt();
        yield return new WaitForSeconds(0.5f);
        yield return messanger.showMessage("", tutorialSender, false);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage(movementTutorialMessage1, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(movementTutorialMessage2, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(movementTutorialMessage3, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        objectivePrompt.showPrompt(lightSearchObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        movementTutorialState = sequenceState.COMPLETE;
        lightSearchState = sequenceState.READY;
        //lightSearchTrigger.enabled = true;
        //sequencePhase++;
    }



    //LIGHT SEARCH SEQUENCE--------------------------------------------------------------------
    public IEnumerator lightSearchSequence()
    {
        lightSearchState = sequenceState.RUNNING;

        objectivePrompt.hidePrompt();
        yield return messanger.showMessage("", tutorialSender, false);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage(lightSearchMessage1, tutorialSender, false);
        yield return new WaitForSeconds(0.75f);
        objectivePrompt.showPrompt(lightPickupObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        lightSearchState = sequenceState.COMPLETE;
        lightPickupState = sequenceState.READY;
    }



    //LIGHT PICKUP SEQUENCE --------------------------------------------------------------------
    public IEnumerator lightPickupSequence()
    {
        lightPickupState = sequenceState.RUNNING;

        objectivePrompt.hidePrompt();
        yield return messanger.showMessage("", tutorialSender, false);
        yield return new WaitForSeconds(0.75f);
        yield return messanger.showMessage(lightPickupMessage1, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(lightPickupMessage2, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(lightPickupMessage3, tutorialSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(lightPickupMessage4, tutorialSender, false);
        yield return new WaitForSeconds(0.75f);
        objectivePrompt.showPrompt(lightHuntObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        lightPickupState = sequenceState.COMPLETE;
        lightHuntState = sequenceState.READY;
    }
}



public enum sequenceState
{
    WAITING,
    READY,
    RUNNING,
    COMPLETE
}
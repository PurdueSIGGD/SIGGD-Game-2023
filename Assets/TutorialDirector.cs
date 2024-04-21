using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDirector : MonoBehaviour
{

    [SerializeField] public bool fastSequencesDEV;

    [SerializeField] public Movement playerMovement;
    [SerializeField] public LightResource playerLightResource;
    [SerializeField] public HealthPoints playerHealthPoints;
    [SerializeField] public playerAttackHandler playerAttackHandler;
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
                                     "- - - - - \n" +
                                     "- Establishing connection to SS Lawson... \n" +
                                     "- - - - - \n";

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

    //private string tutorialMessage4 = "Make sure to take good care of 'er. As much as she costs, you'd be working in " +
    //"indentured servitude for the rest of your natural life if you didn't bring 'er back " +
    //"in one piece. Heh heh...";

    private string tutorialMessage4 = "Make sure to take good care of 'er. She's worth more than the U.S. defense budget. ";

    private string tutorialMessage5 = "Anywho, enough chit chat. Let's get a move on.";

    private string tutorialMessage6 = "I mean that literally. Move the Exo-suit around a bit. We gotta get you used " +
                                      "to steerin' this thing.";



    //MOVEMENT TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState movementTutorialState = sequenceState.WAITING;

    private string movementTutorialPrompt = "WASD | Move Exo-suit";

    //private string movementTutorialMessage1 = "There, see? Your a dern natural. And now that you can drive that thing, " +
    //"it's time to put you to work.";

    private string movementTutorialMessage1 = "There, see? Your a dern natural. Now time to put you to work.";

    private string movementTutorialMessage2 = "Remember, we're down here to collect two things: Light, and Artifacts. Let's " +
                                              "start off lookin' for Light.";

    private string movementTutorialMessage3 = "Head down this trench 'till you see a glowin' orb on the ground. " +
                                              "They're damn near impossible to miss.";



    //LIGHT SEARCH SEQUENCE--------------------------------------------------------------------

    public bool lightSearchTrigger = false;

    public sequenceState lightSearchState = sequenceState.WAITING;

    private string lightSearchObjective = "Search for Light";

    private string lightSearchMessage1 = "See it? If you get close, the suit will absorb it. Give it a try.";



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

    private string lightHuntMessage1 = "Oh, and I forgot to mention, there's some mutant wildlife down there that's " +
                                       "attracted to all the light you're collecting.";

    private string lightHuntMessage2 = "You'll probably only find small ones in this trench, so don't worry too much.";

    private string lightHuntMessage3 = "But the things out there...";

    private string lightHuntMessage4 = "Look, they can all get pretty agressive, so the R&D boys cooked up some " +
                                       "weapons to defend yourself with.";

    private string lightHuntMessage5 = "First, you can fend things off with a melee strike. Try it out now.";



    //MELEE TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState meleeTutorialState = sequenceState.WAITING;

    private string meleeTutorialPrompt = "Right Click | Melee";

    private string meleeTutorialMessage1 = "That's useful if something gets too close. For longer distances, " +
                                           "you've got a Light-powered blaster. Fire away.";



    //BLASTER TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState blasterTutorialState = sequenceState.WAITING;

    private string blasterTutorialPrompt = "Left Click | Shoot Blaster";

    private string blasterTutorialMessage1 = "This beauty will chew through anything in your way, but it also " +
                                             "chews through the Exo-suit's Light reserves.";

    private string blasterTutorialMessage2 = "Check your HUD for a blue bar, that shows how much Light you have.";

    private string blasterTutorialMessage3 = "The bar below it shows the suit's Armor status. If that gets to zero, " +
                                             "you're in trouble.";

    private string blasterTutorialMessage4 = "Even if it does though, you can always collect Light to repair " +
                                             "yourself in a pinch.";

    private string blasterTutorialMessage5 = "Alright, let's keep going. Should be more Light through that tight " +
                                             "passageway to the north.";



    //FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    public sequenceState firstEncounterState = sequenceState.WAITING;

    private string firstEncounterObjective = "Search for Light";






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
            if (playerLightResource.currentLight >= 20)
            {
                StartCoroutine(lightHuntSequence());
            }
        }

        //Melee Tutorial Trigger
        if (meleeTutorialState == sequenceState.READY)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(meleeTutorialSequence());
            }
        }

        //Blaster Tutorial Trigger
        if (blasterTutorialState == sequenceState.READY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(blasterTutorialSequence());
            }
        }

    }



    //START UP SEQUENCE --------------------------------------------------------------------
    public IEnumerator startUpSequence()
    {
        startUpState = sequenceState.RUNNING;

        //ASP exo-suit start up
        playerMovement.enabled = false;
        playerAttackHandler.enabled = false;
        fadeScreenImage.enabled = true;
        if (!fastSequencesDEV)
        {
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
        }

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
        if (!fastSequencesDEV)
        {
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
        }
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
        if (!fastSequencesDEV)
        {
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
            yield return messanger.showMessage(movementTutorialMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(movementTutorialMessage2, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(movementTutorialMessage3, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        objectivePrompt.showPrompt(lightSearchObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        movementTutorialState = sequenceState.COMPLETE;
        lightSearchState = sequenceState.READY;
        //lightSearchTrigger.enabled = true;
        //sequencePhase++;
    }



    //LIGHT SEARCH SEQUENCE --------------------------------------------------------------------
    public IEnumerator lightSearchSequence()
    {
        lightSearchState = sequenceState.RUNNING;

        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.25f);
            yield return messanger.showMessage(lightSearchMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
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
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(lightPickupMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(lightPickupMessage2, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(lightPickupMessage3, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(lightPickupMessage4, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(lightHuntObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        lightPickupState = sequenceState.COMPLETE;
        lightHuntState = sequenceState.READY;
    }



    //LIGHT HUNT SEQUENCE --------------------------------------------------------------------
    public IEnumerator lightHuntSequence()
    {
        lightHuntState = sequenceState.RUNNING;

        //objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(lightHuntMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(lightHuntMessage2, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(lightHuntMessage3, tutorialSender, false);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(lightHuntMessage4, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(lightHuntMessage5, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        //objectivePrompt.showPrompt(lightHuntObjective);
        objectivePrompt.hidePrompt();
        interactPrompt.showPrompt(meleeTutorialPrompt);
        lightHuntState = sequenceState.COMPLETE;
        meleeTutorialState = sequenceState.READY;
        playerAttackHandler.enabled = true;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //MELEE TUTORIAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator meleeTutorialSequence()
    {
        meleeTutorialState = sequenceState.RUNNING;

        yield return new WaitForSeconds(0.5f);
        //objectivePrompt.hidePrompt();
        interactPrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(meleeTutorialMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        //objectivePrompt.showPrompt(lightHuntObjective);
        interactPrompt.showPrompt(blasterTutorialPrompt);
        meleeTutorialState = sequenceState.COMPLETE;
        blasterTutorialState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //BLASTER TUTORIAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator blasterTutorialSequence()
    {
        blasterTutorialState = sequenceState.RUNNING;

        yield return new WaitForSeconds(0.75f);
        //objectivePrompt.hidePrompt();
        interactPrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(blasterTutorialMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(blasterTutorialMessage2, tutorialSender, false);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(blasterTutorialMessage3, tutorialSender, false);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(blasterTutorialMessage4, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(blasterTutorialMessage5, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(firstEncounterObjective);
        //objectivePrompt.hidePrompt();
        //interactPrompt.showPrompt(TutorialPrompt);
        blasterTutorialState = sequenceState.COMPLETE;
        firstEncounterState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }





}



public enum sequenceState
{
    WAITING,
    READY,
    RUNNING,
    COMPLETE
}
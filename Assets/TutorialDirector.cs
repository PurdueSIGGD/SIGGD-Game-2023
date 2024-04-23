using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
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





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- FIELDS ---------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



    //START UP SEQUENCE --------------------------------------------------------------------
    public sequenceState startUpState = sequenceState.WAITING;

    private string startUpSender = "ASP-7";

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

    private string tutorialMessage3 = "This assignment's simple. Right now, all we're doin' is gettin' you aquainted with " +
                                      "this Exo-suit. That way later you can help recover the Automatons that sunk here with our cargo ship.";

    //private string tutorialMessage4 = "Make sure to take good care of 'er. As much as she costs, you'd be working in " +
    //"indentured servitude for the rest of your natural life if you didn't bring 'er back " +
    //"in one piece. Heh heh...";

    private string tutorialMessage4 = "Each one's unique, so the boss wants us to find all the destroyed ones and scan their models. " +
                                      "That way we can replicate them with our new Echo tech.";

    private string tutorialMessage5 = "Anywho, we'll get into all that later. Enough chit chat. Let's get a move on.";

    private string tutorialMessage6 = "I mean that literally. Move the Exo-suit around a bit. We gotta get you used " +
                                      "to steerin' this thing.";



    //MOVEMENT TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState movementTutorialState = sequenceState.WAITING;

    private string movementTutorialPrompt = "WASD | Move Exo-suit";

    //private string movementTutorialMessage1 = "There, see? Your a dern natural. And now that you can drive that thing, " +
    //"it's time to put you to work.";

    private string movementTutorialMessage1 = "There, see? You're a dern natural. Now, time to put you to work.";

    //private string movementTutorialMessage2 = "Remember, we're down here to collect two things: Light, and Artifacts. Let's " +
    //"start off lookin' for Light.";

    private string movementTutorialMessage2 = "Our scans show that we're parked on top of a huge Light deposit, so get ready for " +
                                              "a little egg hunt.";

    private string movementTutorialMessage3 = "Head down this trench 'till you see a glowin' orb on the ground. " +
                                              "They're damn near impossible to miss.";



    //LIGHT SEARCH SEQUENCE--------------------------------------------------------------------

    [SerializeField] public SequenceTrigger lightSearchTrigger;

    private string lightSearchObjective = "Search for Light";

    private string lightSearchMessage1 = "See it? If you get close, the suit will absorb it. Give it a try.";



    //LIGHT PICKUP SEQUENCE --------------------------------------------------------------------

    public sequenceState lightPickupState = sequenceState.WAITING;

    private string lightPickupObjective = "Collect the Light Orb";

    private string lightPickupMessage1 = "Pretty neat huh? These Light Orbs bubble up to the ocean floor from " +
                                         "deep underground.";

    private string lightPickupMessage2 = "They're the only energy source strong enough to power all our tech, but " +
                                         "they're rare as hell.";

    private string lightPickupMessage3 = "All things considered, I'd say it's pretty lucky that that cargo ship " +
                                         "crashed where it did. This place is a gold mine.";

    private string lightPickupMessage4 = "Keep lookin'. There's a lot more down that trench.";

    //private string lightPickupMessage2 = "Well, hope you lined your whitey tighties with lead! If not that's " +
    //"radiation poisoning in 20 years.";

    //private string lightPickupMessage3 = "Yeah, the R&D boys told me the suit protects you from that stuff, " +
    //"but I've never agreed with science before, and I'm not gonna start now!";

    //private string lightPickupMessage4 = "That said, there's more down there you gotta collect. So keep lookin'.";



    //LIGHT HUNT SEQUENCE --------------------------------------------------------------------

    public sequenceState lightHuntState = sequenceState.WAITING;

    private string lightHuntObjective = "Search for Light";

    private string lightHuntMessage1 = "Oh, and I forgot to mention, there's some wildlife down there that's " +
                                       "attracted to all the Light you're collecting.";

    //private string lightHuntMessage2 = "You'll probably only find small things in this trench, so don't worry too much.";

    //private string lightHuntMessage3 = "But the things out there...";

    //private string lightHuntMessage4 = "They can all get pretty agressive, so the R&D boys cooked up some " +
    //"weapons to defend yourself with.";

    private string lightHuntMessage4 = "That's why the R&D boys cooked up some weapons to defend yourself with.";

    private string lightHuntMessage5 = "First, you can fend things off with a melee strike. Try it out now.";



    //MELEE TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState meleeTutorialState = sequenceState.WAITING;

    private string meleeTutorialPrompt = "Right Click | Melee";

    private string meleeTutorialMessage1 = "That's useful if something gets too close. For longer distances, " +
                                           "you've got a Light-powered Blaster. Fire away.";



    //BLASTER TUTORIAL SEQUENCE --------------------------------------------------------------------

    public sequenceState blasterTutorialState = sequenceState.WAITING;

    private string blasterTutorialPrompt = "Left Click | Shoot Blaster";

    private string blasterTutorialMessage1 = "This beauty will chew through anything in your way, but it also " +
                                             "chews through the Exo-suit's Light reserves.";

    private string blasterTutorialMessage2 = "Check your HUD for a blue bar. That shows how much Light you have.";

    private string blasterTutorialMessage3 = "Your HUD also has a green bar. This shows the suit's Health status.";

    private string blasterTutorialMessage3p5 = "If your Health gets to zero, your suit loses all power. Meanin' you have to " +
                                               "survive completely in the dark. No Light, no Blaster, just your melee.";

    private string blasterTutorialMessage4 = "Even if that does happen though, you can always collect Light to repair " +
                                             "yourself in a pinch.";

    //private string blasterTutorialMessage5 = "Alright, let's keep going. There's gonna be a tight chasm somewhere up ahead. " +
    //"Should be more Light through there.";

    private string blasterTutorialMessage5 = "Anyway, no fish is gonna do that much damage, so don't worry too much.";

    private string blasterTutorialMessage6 = "There's gonna be a tight chasm somewhere up ahead. " +
                                             "Should be more Light through there.";



    //FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger firstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner firstEncounterEnemySpawner;

    private string firstEncounterObjective = "Search for Light";

    [SerializeField] public List<enemyType> firstEncounterEnemyList;

    private List<GameObject> firstEncounterEnemies;



    //ROOM 2 ATTACK SEQUENCE ----------------------------------------------------------------------

    public sequenceState room2AttackState = sequenceState.WAITING;

    [SerializeField] public ControlledEnemySpawner room2AttackEnemySpawner;

    private string room2AttackObjective = "Survive";

    private string room2AttackMessage1 = "What in the hell was that?! You alright, kid?";

    private string room2AttackMessage2 = "That's not the type of wildlife I was talkin' about!";

    private string room2AttackMessage3 = "Hold tight, our scanners are showin' more of those things comin' your way.";

    [SerializeField] public List<enemyType> room2AttackEnemyList1;

    [SerializeField] public List<enemyType> room2AttackEnemyList2;

    [SerializeField] public List<enemyType> room2AttackEnemyList3;

    [SerializeField] public List<enemyType> room2AttackEnemyList4;

    private List<GameObject> room2AttackLastEnemies;

    //private string room2AttackMessage4 = "Alright kid, there's an Artifact in the next chamber up.";

    //private string room2AttackMessage5 = "Time to see if you can learn under pressure.";



    //ROOM 2 ATTACK END SEQUENCE ----------------------------------------------------------------------

    public sequenceState room2AttackEndState = sequenceState.WAITING;

    private string room2AttackEndMessage1 = "Alright kid, we're firin' up the Teleporter to get you back, but it " +
                                            "needs some time before it's ready to go.";

    private string room2AttackEndMessage2 = "You're gonna have to defend yourself until then.";

    private string room2AttackEndMessage3 = "Luckily for you, our scans show that there's an Automaton in the " +
                                            "next chamber up ahead.";

    private string room2AttackEndMessage4 = "If you can reach it, it should help your chances.";



    //ARTIFACT ROOM ATTACK SEQUENCE ----------------------------------------------------------------------

    //public sequenceState artifactRoomAttackState = sequenceState.WAITING; //TODO: ADD TRIGGER BOX
    [SerializeField] public SequenceTrigger artifactRoomAttackTrigger;

    [SerializeField] public ControlledEnemySpawner artifactRoomEnemySpawner;

    private string artifactRoomAttackObjective = "Reach the Automaton";

    [SerializeField] public List<enemyType> artifactRoomAttackEnemyList1;

    [SerializeField] public List<enemyType> artifactRoomAttackEnemyList2;

    private List<GameObject> artifactRoomAttackLastEnemies;



    //ARTIFACT ROOM ATTACK END SEQUENCE -----------------------------------------------------------------

    public sequenceState artifactRoomAttackEndState = sequenceState.WAITING;

    private string artifactRoomAttackEndMessage1 = "You're doin' great. The Automaton is just up ahead.";



    //ARTIFACT FOUND SEQUENCE ---------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger artifactFoundTrigger;

    private string artifactFoundMessage1 = "There it is. Hurry up and scan the Automaton. More are coming.";



    //ARTIFACT SCAN SEQUENCE ---------------------------------------------------------------------------

    public sequenceState artifactScanState = sequenceState.WAITING;

    [SerializeField] public Interactable artifact;

    private string artifactScanObjective = "Scan the Automaton";

    private string artifactScanMessage1 = "The Automaton's data should've saved in the suit's Echo Drive. " +
                                          "Look for it on your HUD just above your Light Bar.";

    private string artifactScanMessage2 = "Your suit can create a Light-powered Echo of the Automaton. It'll help " +
                                          "you fight these things off. Deploy an Echo now.";



    //ECHO TUTORIAL SEQUENCE ---------------------------------------------------------------------------

    public sequenceState echoTutorialState = sequenceState.WAITING;

    private string echoTutorialPrompt = "LEFT SHIFT | Deploy Echo";

    private string echoTutorialMessage1 = "Alright, The Teleporter's just about ready, but you're too far " +
                                          "from the submarine.";

    private string echoTutorialMessage2 = "You're going to need to backtrack. We'll " +
                                          "teleport you when you're close enough. Good luck, kid.";



    //BACKTRACK ARTIFACT ROOM SEQUENCE ------------------------------------------------------------------

    //Artifact Room Attack Trigger

    private string backtrackArtifactRoomObjective = "Backtrack Towards the Submarine";

    private string backtrackArtifactRoomMessage1 = "Here they come!";

    [SerializeField] public List<enemyType> backtrackArtifactRoomEnemyList1;

    [SerializeField] public List<enemyType> backtrackArtifactRoomEnemyList2;



    //BACKTRACK ROOM 3 SEQUENCE ------------------------------------------------------------------

    //First Encounter Trigger

    [SerializeField] public ControlledEnemySpawner room3EnemySpawner;

    [SerializeField] public List<enemyType> backtrackRoom3EnemyList;

    private List<GameObject> backtrackRoom3LastEnemies;



    //BACKTRACK HALLWAY SEQUENCE ------------------------------------------------------------------

    public sequenceState backtrackHallwayState = sequenceState.WAITING;

    [SerializeField] public ControlledEnemySpawner hallwayEnemySpawner;

    private string backtrackHallwayMessage1 = "Almost there, kid. Just at bit further and we'll teleport you outta there.";

    [SerializeField] public List<enemyType> backtrackHallwayEnemyList;



    //BACKTRACK FINAL SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger backtrackFinalTrigger;

    [SerializeField] public ControlledEnemySpawner room1EnemySpawner;

    [SerializeField] public List<enemyType> backtrackFinalEnemyList1;

    [SerializeField] public List<enemyType> backtrackFinalEnemyList2;

    [SerializeField] public List<enemyType> backtrackFinalEnemyList3;

    private string backtrackFinalObjective = "Survive";

    private string backtrackFinalMessage1 = "You're in range now. Fight off the last of these things so we can " +
                                       "teleport you and get the hell out of here!";

    private List<GameObject> backtrackFinalLastEnemies;



    // TELEPORT SEQUENCE --------------------------------------------------------------------

    //[SerializeField] public SequenceTrigger teleportTrigger;

    public sequenceState teleportState = sequenceState.WAITING;

    private bool teleportFailed = false;

    private string teleportMessage1 = "Activating the Teleporter. It takes just a few seconds.";

    private string teleportObjective1 = "Teleporter Activating...   ";

    private string teleportMessage2 = "Real good work there, kid. We're goin' home. No doubt this'll make for one " +
                                      "hell of a st--- - - - - -";

    private string backupSender = "EXO-SUIT BACKUP PROTOCOLS";

    private string teleportMessage3 = "- ERROR REPORT: \n" +
                                      "- - - \n" +
                                      "- HOST SHIP CANNOT BE FOUND \n" +
                                      "- - - \n" +
                                      "- ASP-7 COPILOT DISCONNECTED \n" +
                                      "- - - ";

    private string teleportMessage4 = "- DIRECTIVE: \n" +
                                      "- - - \n" +
                                      "- RETURN TO HOST SHIP IMMEDIATELY \n" +
                                      "- - - \n" +
                                      "- RECONNECT TO ASP-7 COPILOT \n" +
                                      "- - - ";

    private string teleportObjective2 = "Return to the Submarine";






    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- TRIGGERS -------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



    // Start is called before the first frame update
    void Start()
    {
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
        if (lightSearchTrigger.sequenceState == sequenceState.READY)
        {
            if (lightSearchTrigger.triggered)
            {
                lightSearchTrigger.sequenceState = sequenceState.RUNNING;
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
            if (playerLightResource.currentLight >= 30)
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

        //First Encounter Trigger
        if (firstEncounterTrigger.sequenceState == sequenceState.READY &&
            echoTutorialState == sequenceState.WAITING)
        {
            if (firstEncounterTrigger.triggered)
            {
                StartCoroutine(firstEncounterSequence());
            }
        }

        //Room 2 Attack Trigger
        if (room2AttackState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(firstEncounterEnemies))
            {
                StartCoroutine(room2AttackSequence());
            }
        }

        //Room 2 Attack End Trigger
        if (room2AttackEndState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(room2AttackLastEnemies))
            {
                StartCoroutine(room2AttackEndSequence());
            }
        }

        //Artifact Room Attack Trigger
        if (artifactRoomAttackTrigger.sequenceState == sequenceState.READY &&
            echoTutorialState == sequenceState.WAITING)
        {
            if (artifactRoomAttackTrigger.triggered)
            {
                StartCoroutine(artifactRoomAttackSequence());
            }
        }

        //Artifact Room Attack End Trigger
        if (artifactRoomAttackEndState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(artifactRoomAttackLastEnemies))
            {
                StartCoroutine(artifactRoomAttackEndSequence());
            }
        }

        //Artifact Found Trigger
        if (artifactFoundTrigger.sequenceState == sequenceState.READY)
        {
            if (artifactFoundTrigger.triggered)
            {
                StartCoroutine(artifactFoundSequence());
            }
        }

        //Artifact Scan Trigger
        if (artifactScanState == sequenceState.READY)
        {
            if (artifact.isUsed)
            {
                StartCoroutine(artifactScanSequence());
            }
        }

        //Echo Tutorial Trigger
        if (echoTutorialState == sequenceState.READY)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(echoTutorialSequence());
            }
        }

        //Backtrack Artifact Room Trigger
        if (artifactRoomAttackTrigger.sequenceState == sequenceState.READY &&
            echoTutorialState == sequenceState.COMPLETE)
        {
            if (artifactRoomAttackTrigger.triggered)
            {
                StartCoroutine(backtrackArtifactRoomSequence());
            }
        }

        //Backtrack Room 3 Trigger
        if (firstEncounterTrigger.sequenceState == sequenceState.READY &&
            echoTutorialState == sequenceState.COMPLETE)
        {
            if (firstEncounterTrigger.triggered)
            {
                StartCoroutine(backtrackRoom3Sequence());
            }
        }

        //Backtrack Hallway Trigger
        if (backtrackHallwayState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(backtrackRoom3LastEnemies))
            {
                StartCoroutine(backtrackHallwaySequence());
            }
        }

        //Backtrack Final Trigger
        if (backtrackFinalTrigger.sequenceState == sequenceState.READY)
        {
            if (backtrackFinalTrigger.triggered)
            {
                StartCoroutine(backtrackFinalSequence());
            }
        }

        //Teleport Trigger
        if (teleportState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(backtrackFinalLastEnemies))
            {
                StartCoroutine(teleportSequence());
            }
        }

    }





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- SEQUENCES ------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



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
        //lightSearchState = sequenceState.READY;
        lightSearchTrigger.sequenceState = sequenceState.READY;
        //lightSearchTrigger.enabled = true;
        //sequencePhase++;
    }



    //LIGHT SEARCH SEQUENCE --------------------------------------------------------------------
    public IEnumerator lightSearchSequence()
    {
        //lightSearchState = sequenceState.RUNNING;
        lightSearchTrigger.sequenceState = sequenceState.RUNNING;

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

        //lightSearchState = sequenceState.COMPLETE;
        lightSearchTrigger.sequenceState = sequenceState.COMPLETE;
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
            //yield return messanger.showMessage(lightHuntMessage2, tutorialSender, false);
            //yield return new WaitForSeconds(1.25f);
            //yield return messanger.showMessage(lightHuntMessage3, tutorialSender, false);
            //yield return new WaitForSeconds(2f);
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
            yield return new WaitForSeconds(2.5f);
            yield return messanger.showMessage(blasterTutorialMessage3, tutorialSender, false);
            yield return new WaitForSeconds(2.5f);
            yield return messanger.showMessage(blasterTutorialMessage3p5, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(blasterTutorialMessage4, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(blasterTutorialMessage5, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(blasterTutorialMessage6, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(firstEncounterObjective);
        //objectivePrompt.hidePrompt();
        //interactPrompt.showPrompt(TutorialPrompt);
        blasterTutorialState = sequenceState.COMPLETE;
        //firstEncounterState = sequenceState.READY;
        firstEncounterTrigger.sequenceState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator firstEncounterSequence()
    {
        firstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        firstEncounterEnemies = firstEncounterEnemySpawner.spawnEnemyWave(firstEncounterEnemyList);
        yield return new WaitForSeconds(0.75f);
        firstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
        room2AttackState = sequenceState.READY;
    }



    //ROOM 2 ATTACK SEQUENCE --------------------------------------------------------------------
    public IEnumerator room2AttackSequence()
    {
        room2AttackState = sequenceState.RUNNING;

        yield return new WaitForSeconds(1f);
        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(room2AttackMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(room2AttackMessage2, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(room2AttackMessage3, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(room2AttackObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList1);
        yield return new WaitForSeconds(3f);
        room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList2);
        yield return new WaitForSeconds(5f);
        room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList2);
        yield return new WaitForSeconds(5f);
        room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList3);
        yield return new WaitForSeconds(8f);
        room2AttackLastEnemies = room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList4);
        yield return new WaitForSeconds(0.75f);
        room2AttackState = sequenceState.COMPLETE;
        room2AttackEndState = sequenceState.READY;
    }



    //ROOM 2 ATTACK END SEQUENCE --------------------------------------------------------------------
    public IEnumerator room2AttackEndSequence()
    {
        room2AttackEndState = sequenceState.RUNNING;

        yield return new WaitForSeconds(0.75f);
        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(room2AttackEndMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(room2AttackEndMessage2, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(room2AttackEndMessage3, tutorialSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(room2AttackEndMessage4, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(artifactRoomAttackObjective);
        yield return new WaitForSeconds(0.75f);
        room2AttackEndState = sequenceState.COMPLETE;
        artifactRoomAttackTrigger.sequenceState = sequenceState.READY;
        messanger.hideMessage();
    }



    //ARTIFACT ROOM ATTACK SEQUENCE --------------------------------------------------------------------
    public IEnumerator artifactRoomAttackSequence()
    {
        artifactRoomAttackTrigger.sequenceState = sequenceState.RUNNING;
        artifactRoomEnemySpawner.spawnEnemyWave(artifactRoomAttackEnemyList1);
        yield return new WaitForSeconds(8f);
        artifactRoomAttackLastEnemies = artifactRoomEnemySpawner.spawnEnemyWave(artifactRoomAttackEnemyList2);
        yield return new WaitForSeconds(0.75f);
        artifactRoomAttackTrigger.sequenceState = sequenceState.COMPLETE;
        artifactRoomAttackEndState = sequenceState.READY;
    }



    //ARTIFACT ROOM ATTACK END SEQUENCE --------------------------------------------------------------------
    public IEnumerator artifactRoomAttackEndSequence()
    {
        artifactRoomAttackEndState = sequenceState.RUNNING;

        yield return new WaitForSeconds(0.75f);
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(artifactRoomAttackEndMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        artifactRoomAttackEndState = sequenceState.COMPLETE;
        artifactFoundTrigger.sequenceState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //ARTIFACT FOUND SEQUENCE --------------------------------------------------------------------
    public IEnumerator artifactFoundSequence()
    {
        artifactFoundTrigger.sequenceState = sequenceState.RUNNING;

        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.25f);
            yield return messanger.showMessage(artifactFoundMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(artifactScanObjective);
        artifactFoundTrigger.sequenceState = sequenceState.COMPLETE;
        artifactScanState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //ARTIFACT SCAN SEQUENCE --------------------------------------------------------------------
    public IEnumerator artifactScanSequence()
    {
        artifactScanState = sequenceState.RUNNING;

        objectivePrompt.hidePrompt();
        yield return new WaitForSeconds(0.5f);
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(artifactScanMessage1, tutorialSender, false);
            yield return new WaitForSeconds(2.5f);
            yield return messanger.showMessage(artifactScanMessage2, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        interactPrompt.showPrompt(echoTutorialPrompt);
        artifactScanState = sequenceState.COMPLETE;
        echoTutorialState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //ECHO TUTORIAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator echoTutorialSequence()
    {
        echoTutorialState = sequenceState.RUNNING;

        interactPrompt.hidePrompt();
        yield return new WaitForSeconds(0.25f);
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(echoTutorialMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(echoTutorialMessage2, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(backtrackArtifactRoomObjective);
        echoTutorialState = sequenceState.COMPLETE;
        artifactRoomAttackTrigger.triggered = false;
        artifactRoomAttackTrigger.sequenceState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //BACKTRACK ARTIFACT ROOM SEQUENCE --------------------------------------------------------------------
    public IEnumerator backtrackArtifactRoomSequence()
    {
        artifactRoomAttackTrigger.sequenceState = sequenceState.RUNNING;

        artifactRoomEnemySpawner.spawnEnemyWave(backtrackArtifactRoomEnemyList1);
        artifactRoomEnemySpawner.passiveSpawnActive = true;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(backtrackArtifactRoomMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        messanger.hideMessage();
        yield return new WaitForSeconds(3f);
        artifactRoomEnemySpawner.passiveWaveSpawnActive = true;
        yield return new WaitForSeconds(10f);

        room2AttackEnemySpawner.spawnEnemyWave(backtrackArtifactRoomEnemyList2);
        room2AttackEnemySpawner.passiveSpawnActive = true;
        yield return new WaitForSeconds(5f);
        artifactRoomEnemySpawner.passiveSpawnActive = false;
        artifactRoomEnemySpawner.passiveWaveSpawnActive = false;
        room2AttackEnemySpawner.passiveWaveSpawnActive = true;

        artifactRoomAttackTrigger.sequenceState = sequenceState.COMPLETE;
        firstEncounterTrigger.triggered = false;
        firstEncounterTrigger.sequenceState = sequenceState.READY;

        yield return new WaitForSeconds(15f);
        //artifactRoomEnemySpawner.passiveSpawnActive = false;
        //artifactRoomEnemySpawner.passiveWaveSpawnActive = false;
        room2AttackEnemySpawner.passiveSpawnActive = false;
        room2AttackEnemySpawner.passiveWaveSpawnActive = false;
    }



    //BACKTRACK ROOM 3 SEQUENCE --------------------------------------------------------------------
    public IEnumerator backtrackRoom3Sequence()
    {
        firstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        artifactRoomEnemySpawner.passiveSpawnActive = false;
        artifactRoomEnemySpawner.passiveWaveSpawnActive = false;
        room2AttackEnemySpawner.passiveSpawnActive = false;
        room2AttackEnemySpawner.passiveWaveSpawnActive = false;

        room3EnemySpawner.spawnEnemyWave(backtrackRoom3EnemyList);
        room3EnemySpawner.passiveSpawnActive = true;
        yield return new WaitForSeconds(8f);
        room3EnemySpawner.passiveSpawnActive = false;
        backtrackRoom3LastEnemies = room3EnemySpawner.spawnEnemyWave(backtrackRoom3EnemyList);
        firstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
        backtrackHallwayState = sequenceState.READY;
    }



    //BACKTRACK HALLWAY SEQUENCE --------------------------------------------------------------------
    public IEnumerator backtrackHallwaySequence()
    {
        backtrackHallwayState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(backtrackHallwayMessage1, tutorialSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        messanger.hideMessage();
        yield return new WaitForSeconds(2f);
        hallwayEnemySpawner.spawnEnemyWave(backtrackHallwayEnemyList);
        yield return new WaitForSeconds(3f);
        hallwayEnemySpawner.passiveSpawnActive = true;
        yield return new WaitForSeconds(10f);
        backtrackHallwayState = sequenceState.COMPLETE;
        backtrackFinalTrigger.sequenceState = sequenceState.READY;
        yield return new WaitForSeconds(10f);
        hallwayEnemySpawner.passiveSpawnActive = false;
    }



    //BACKTRACK FINAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator backtrackFinalSequence()
    {
        //backtrackHallwayState = sequenceState.RUNNING;
        backtrackFinalTrigger.sequenceState = sequenceState.RUNNING;

        hallwayEnemySpawner.passiveSpawnActive = false;
        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(backtrackFinalMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(backtrackFinalObjective);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList1);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        yield return new WaitForSeconds(2.25f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList1);
        yield return new WaitForSeconds(3f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        room1EnemySpawner.passiveSpawnActive = true;

        yield return new WaitForSeconds(12f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(4f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(4f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList3);

        room1EnemySpawner.passiveSpawnActive = false;
        yield return new WaitForSeconds(12f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(2.5f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(3f);
        backtrackFinalLastEnemies = room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList3);

        backtrackFinalTrigger.sequenceState = sequenceState.COMPLETE;
        teleportState = sequenceState.READY;
    }



    //TELEPORT SEQUENCE --------------------------------------------------------------------
    public IEnumerator teleportSequence()
    {
        teleportState = sequenceState.RUNNING;

        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(teleportMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
            StartCoroutine(teleportActivatingSequence());
            yield return new WaitForSeconds(0.5f);
            messanger.hideMessage();
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(teleportMessage2, tutorialSender, false);
            //StopCoroutine(teleportActivatingSequence());
            teleportFailed = true;
            objectivePrompt.showPrompt(teleportObjective1 + "[ERR]");
            yield return new WaitForSeconds(2.5f);

            yield return messanger.showMessage(teleportMessage3, backupSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(teleportMessage4, backupSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        objectivePrompt.showPrompt(teleportObjective2);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        teleportState = sequenceState.COMPLETE;
        //NEXTState = sequenceState.READY;
    }



    //TELEPORT ACTIVATING SEQUENCE --------------------------------------------------------------------
    public IEnumerator teleportActivatingSequence()
    {
        //int teleportCountdown = 10;
        for (int i = 10; i > 0; i--)
        {
            if (!teleportFailed)
            {
                objectivePrompt.showPrompt(teleportObjective1 + i);
            }
            yield return new WaitForSeconds(1f);
        }
    }



}



public enum sequenceState
{
    WAITING,
    READY,
    RUNNING,
    COMPLETE
}
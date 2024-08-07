using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TutorialDirector : MonoBehaviour
{

    [SerializeField] public bool fastSequencesDEV;
    public int tutorialProgress = 0;
    private bool isLoading = true;

    [SerializeField] public Movement playerMovement;
    [SerializeField] public LightResource playerLightResource;
    [SerializeField] public HealthPoints playerHealthPoints;
    [SerializeField] public playerAttackHandler playerAttackHandler;
    [SerializeField] public EnemySpawner enemySpawner;



    [SerializeField] public MusicConductor musicConductor;
    [SerializeField] public AudioSource hummingSound;
    [SerializeField] public AudioSource damageSound;
    [SerializeField] public AudioSource whaleSound;
    [SerializeField] public AudioSource whaleAttackSound;
    [SerializeField] public AudioSource completionSound;



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

    //[SerializeField] public AudioSource tutorialMusic;

    private string startUpSender = "ASP-7";

    private string startUpMessage1 = "- ASP-Assisted Exo-Suit startup initiated";

    private string startUpMessage2 = "- Exo-Suit E-54 boot protocols in progress \n" +
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
                                     "- Establishing connection to SSM Lawson... \n" +
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

    private string lightHuntMessage1 = "Oh, and I forgot to mention, there's usually some agressive wildlife that's " +
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

    [SerializeField] public GameObject firstEncounterPlayerSpawnPoint;

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

    [SerializeField] public SequenceTrigger artifactRoomAttackTrigger;

    [SerializeField] public ControlledEnemySpawner artifactRoomEnemySpawner;

    private string artifactRoomAttackObjective = "Reach the Automaton";

    [SerializeField] public List<enemyType> artifactRoomAttackEnemyList1;

    [SerializeField] public List<enemyType> artifactRoomAttackEnemyList2;

    private List<GameObject> artifactRoomAttackLastEnemies;



    //ARTIFACT ROOM ATTACK END SEQUENCE -----------------------------------------------------------------

    public sequenceState artifactRoomAttackEndState = sequenceState.WAITING;

    //[SerializeField] public AudioSource lvl1Music;

    private string artifactRoomAttackEndMessage1 = "You're doin' great. The Automaton is just up ahead.";



    //ARTIFACT FOUND SEQUENCE ---------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger artifactFoundTrigger;

    private string artifactFoundMessage1 = "There it is. Hurry up and scan the Automaton. More are coming.";



    //ARTIFACT SCAN SEQUENCE ---------------------------------------------------------------------------

    public sequenceState artifactScanState = sequenceState.WAITING;

    [SerializeField] public Artifact artifact;

    private string artifactScanObjective = "Scan the Automaton";

    private string artifactScanMessage1 = "The Automaton's data should've saved in the suit's Echo Drive. " +
                                          "Look for it on your HUD just above your Light Bar.";

    private string artifactScanMessage2 = "Your suit can create a Light-powered Echo of the Automaton. It'll help " +
                                          "you fight these things off. Deploy an Echo now.";



    //ECHO TUTORIAL SEQUENCE ---------------------------------------------------------------------------

    public sequenceState echoTutorialState = sequenceState.WAITING;

    [SerializeField] public GameObject echoTutorialPlayerSpawnPoint;

    private string echoTutorialPrompt = "LEFT SHIFT | Deploy Echo";

    private string echoTutorialMessage1 = "Alright, The Teleporter's just about ready, but you're too far " +
                                          "from the submarine.";

    private string echoTutorialMessage2 = "You're going to need to backtrack. We'll " +
                                          "teleport you when you're close enough. Good luck, kid.";



    //BACKTRACK ARTIFACT ROOM SEQUENCE ------------------------------------------------------------------

    private string backtrackArtifactRoomObjective = "Backtrack Towards the Submarine";

    private string backtrackArtifactRoomMessage1 = "Here they come!";

    [SerializeField] public List<enemyType> backtrackArtifactRoomEnemyList1;

    [SerializeField] public List<enemyType> backtrackArtifactRoomEnemyList2;



    //BACKTRACK ROOM 3 SEQUENCE ------------------------------------------------------------------

    [SerializeField] public ControlledEnemySpawner room3EnemySpawner;

    [SerializeField] public List<enemyType> backtrackRoom3EnemyList;

    private List<GameObject> backtrackRoom3LastEnemies;



    //BACKTRACK HALLWAY SEQUENCE ------------------------------------------------------------------

    public sequenceState backtrackHallwayState = sequenceState.WAITING;

    [SerializeField] public ControlledEnemySpawner hallwayEnemySpawner;

    private string backtrackHallwayMessage1 = "Almost there, kid. Just a bit further and we'll teleport you outta there.";

    [SerializeField] public List<enemyType> backtrackHallwayEnemyList;



    //BACKTRACK FINAL SEQUENCE --------------------------------------------------------------------

    //[SerializeField] public AudioSource deepOceanBassMusic;

    [SerializeField] public SequenceTrigger backtrackFinalTrigger;

    [SerializeField] public GameObject backtrackFinalPlayerSpawnPoint;

    [SerializeField] public ControlledEnemySpawner room1EnemySpawner;

    [SerializeField] public List<enemyType> backtrackFinalEnemyList1;

    [SerializeField] public List<enemyType> backtrackFinalEnemyList2;

    [SerializeField] public List<enemyType> backtrackFinalEnemyList3;

    private string backtrackFinalObjective = "Defend your position";

    private string backtrackFinalMessage1 = "You're in range now. Fight off the last of these things so we can " +
                                       "teleport you and get the hell out of here!";

    private List<GameObject> backtrackFinalLastEnemies;



    // TELEPORT SEQUENCE --------------------------------------------------------------------

    public sequenceState teleportState = sequenceState.WAITING;

    [SerializeField] public GameObject teleportPlayerSpawnPoint;

    //[SerializeField] public AudioSource deathMusic;
    [SerializeField] public GameObject tutorialGate;
    [SerializeField] public GameObject submarine;
    [SerializeField] public GameObject brokenSubmarine;
    [SerializeField] public GameObject submarineInteractables;

    private bool teleportFailed = false;

    private string teleportMessage1 = "Activating the Teleporter. It takes just a few seconds.";

    private string teleportObjective1 = "Teleporter Activating...   ";

    private string teleportMessage2 = "Real good work there, kid. We're goin' home. No doubt this'll make for one " +
                                      "hell of a st-----";

    private string backupSender = "EXO-SUIT BACKUP PROTOCOLS";

    private string teleportMessage3 = "- - - \n" +
                                      "- ERROR REPORT: \n" +
                                      "- - - \n" +
                                      "- HOST SHIP CANNOT BE FOUND \n" +
                                      "- - - \n" +
                                      "- ASP-7 COPILOT DISCONNECTED \n" +
                                      "- - - ";

    private string teleportMessage4 = "- - - \n" +
                                      "- DIRECTIVE: \n" +
                                      "- - - \n" +
                                      "- RETURN TO HOST SHIP IMMEDIATELY \n" +
                                      "- - - \n" +
                                      "- RECONNECT TO ASP-7 COPILOT \n" +
                                      "- - - ";

    private string teleportObjective2 = "Return to the Submarine";



    // RETURN TRENCH SEQUENCE  --------------------------------------------------------------------

    [SerializeField] public ControlledEnemySpawner trenchEnemySpawner;

    [SerializeField] public List<enemyType> trenchEnemyList1;

    [SerializeField] public List<enemyType> trenchEnemyList2;

    //[SerializeField] public List<enemyType> trenchEnemyList3;

    //private string backtrackFinalObjective = "Survive

    //private string trenchMessage1 = "You're in range now. Fight off the last of these things so we can " +
                                       //"teleport you and get the hell out of here!";

    private List<GameObject> trenchLastEnemies;



    // COLLECT CORE SEQUENCE ---------------------------------------------------------------------------

    //public sequenceState collectCoreState = sequenceState.WAITING;

    [SerializeField] public ControlledEnemySpawner submarineEnemySpawner;

    [SerializeField] public List<enemyType> submarineEnemyList1;

    [SerializeField] public SequenceTrigger collectCoreTrigger;

    [SerializeField] public GameObject aspCore;

    private string collectCoreMessage1 = "- - - \n" +
                                         "- STATUS REPORT: \n" +
                                         "- - - \n" +
                                         "- HOST SHIP DESTROYED \n" +
                                         "- - - \n" +
                                         "- ASP-7 COPILOT CORE FOUND \n" +
                                         "- - - \n";

    private string collectCoreMessage2 = "- - - \n" +
                                         "- DIRECTIVE: \n" +
                                         "- - - \n" +
                                         "- MANUALLY INSTALL ASP-7 CORE \n" +
                                         "- - - ";

    private string collectCoreObjective = "Install ASP-7 Core";



    // ASP INSTALL SEQUENCE ---------------------------------------------------------------------------

    public sequenceState aspInstallState = sequenceState.WAITING;

    [SerializeField] public GameObject aspInstallPlayerSpawnPoint;

    [SerializeField] public ControlledEnemySpawner aspInstallEnemySpawner;

    [SerializeField] public List<enemyType> aspInstallEnemyList1;

    [SerializeField] public List<enemyType> aspInstallEnemyList2;

    [SerializeField] public List<enemyType> aspInstallEnemyList3;

    private List<GameObject> aspInstallLastEnemies;

    private string aspSender = "ASP-7";

    private string aspInstallObjective = "Defend your position";

    private string aspInstallMessage1 = "- ASP-7 installation initiated";

    private string aspInstallMessage2 = "- Unknown host detected: Exo-suit E-54 \n" + 
                                        "- - - - - - \n" + 
                                        "Connecting... \n" + 
                                        "- - - - - - - - - - - - - - \n" + 
                                        "New host connection established \n" + 
                                        "- - - - -";

    private string aspInstallMessage3 = "- Autonomous Support Pilot online \n" + 
                                        "- - - - - \n" + 
                                        "Explorer, I am here to help.";

    private string aspInstallMessage3p5 = "Remain near the SSM Lawson while my reboot completes. Its remaining Light " +
                                        "power will hasten the process.";

    private string aspInstallMessage4 = "During this time, it is logical to assume that multiple hostile entities " + 
                                        "will be attracted to this position. Echo deployment is advised.";

    private string aspInstallMessage5 = "- Reinitializing ASP-7 critical systems... \n" + 
                                        "- - - - -";

    private string aspInstallMessageLoading = " - - - - - - - - - - - -";

    private string aspInstallMessage6 = "- Exo-suit guidance system online \n" +
                                        "- - - - -";

    private string aspInstallMessage7 = "- Area mapping system online \n" +
                                        "- - - - -";

    private string aspInstallMessage8 = "- Low power sonar engaged \n" +
                                        "- - - - -";

    private string aspInstallMessage9 = "- Optical input device connected \n" +
                                        "- - - - -";

    private string aspInstallMessage10 = "- Audio input device connected \n" +
                                         "- - - - -";

    private string aspInstallMessage11 = "- Remote signal transmitter offline \n" + 
                                         "- ERR: device could not be connected \n" +
                                         "- - - - -";

    private string aspInstallMessage12 = "- Remote signal receiver connected \n" +
                                         "- - - - -";

    private string aspInstallMessage13 = "- Auxiliary power connected \n" +
                                         "- - - - -";

    private string aspInstallMessage14 = "- - - - - \n" + 
                                         "- REBOOT COMPLETE \n" +
                                         "- - - - -";

    private string aspInstallMessage15 = "Explorer, I detect one more wave of hostile entities approaching. " +
                                         "Neutralize them, and then I will determine our next objective.";

    /*private string aspInstallMessage5 = "- WARNING: MULTIPLE ENTITIES APPROACHING \n" + 
                                        "- - - - - \n" + 
                                        "- Explorer, you must defend this position. Echo deployment is advised.";*/



    //SCAN TUTORIAL SEQUENCE

    public sequenceState scanTutorialState = sequenceState.WAITING;
    
    private string scanTutorialPrompt = "SPACE | Use Sonar Pulse";

    private string scanTutorialMessage1 = "Hostile entities neutralized. Good work.";

    private string scanTutorialMessage2 = "The SSM Lawson has been rendered inoperable, and my radio transmitter " + 
                                          "is offline. We are stranded.";

    private string scanTutorialMessage3 = "Additionally, Your suit's life support systems are not designed for long-term use.";
    
    private string scanTutorialMessage4 = "We must act quickly in order to survive.";

    private string scanTutorialMessage5 = "The Exo-suit is equipped with a high-power sonar device. I will use " + 
                                          "this to map the surrounding area and identify points of interest.";

    private string scanTutorialMessage6 = "Release a sonar pulse now.";



    //THE PLAN SEQUENCE

    public sequenceState thePlanState = sequenceState.WAITING;

    private string thePlanMessage1 = "Analyzing... \n" + 
                                     "- - - - - - - - - - - -";

    private string thePlanMessage2 = "- Multiple light reservoir pylons found \n" +
                                     "- - - - - \n" +
                                     "- Multiple defense-grade atomatons found \n" +
                                     "- - - - - \n" +
                                     "- Long-range homebound teleporter found \n" +
                                     "- - - - -";

    private string thePlanMessage3 = "Explorer, I have a plan.";

    private string thePlanMessage4 = "There are structures rich in light-power scattered across the sea floor.";

    private string thePlanMessage5 = "Many of these structures, or \"pylons\" as they are called, lie between " + 
                                     "us and a sunken teleporter, which we can use to escape.";

    private string thePlanMessage6 = "As we travel to the teleporter, you must activate each pylon. This will " + 
                                     "inductively charge the teleporter with light.";

    private string thePlanMessage7 = "Exit this trench through the newly bored tunnel to the left. The closest " + 
                                     "pylon is near the opening.";

    private string thePlanObjective = "Exit the trench";



    //TUNNEL SEQUENCE

    [SerializeField] public SequenceTrigger tunnelTrigger;

    private string tunnelMessage1 = "I am unaware of any creature capable of tunneling this far through solid rock.";

    private string tunnelMessage2 = "Whatever it is, it is large, highly destructive, and nearly undetectable.";

    private string tunnelMessage3 = "Extreme caution advised. Many dangerous unknown entities are likely " +
                                    "to be roaming the open waters.";





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
        //StartCoroutine(startUpSequence());
        /*SaveManager saveManager = FindObjectOfType<SaveManager>();
        if (saveManager != null)
        {
            //
        }
        else
        {
            StartCoroutine(startUpSequence());
        }*/
        isLoading = true;
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.05f);

        //Set Sequence States
        if (tutorialProgress >= 1) //Before First Encounter
        {
            startUpState = sequenceState.COMPLETE;
            movementTutorialState = sequenceState.COMPLETE;
            lightSearchTrigger.sequenceState = sequenceState.COMPLETE;
            lightSearchTrigger.triggered = true;
            lightPickupState = sequenceState.COMPLETE;
            lightHuntState = sequenceState.COMPLETE;
            meleeTutorialState = sequenceState.COMPLETE;
            blasterTutorialState = sequenceState.COMPLETE;
            firstEncounterTrigger.sequenceState = sequenceState.READY;
        }
        if (tutorialProgress >= 2) //Before Echo Deployment
        {
            firstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
            firstEncounterTrigger.triggered = true;
            room2AttackState = sequenceState.COMPLETE;
            room2AttackEndState = sequenceState.COMPLETE;
            artifactRoomAttackTrigger.sequenceState = sequenceState.COMPLETE;
            artifactRoomAttackTrigger.triggered = true;
            artifactRoomAttackEndState = sequenceState.COMPLETE;
            artifactFoundTrigger.sequenceState = sequenceState.COMPLETE;
            artifactFoundTrigger.triggered = true;
            artifactScanState = sequenceState.COMPLETE;
            echoTutorialState = sequenceState.READY;
        }
        if (tutorialProgress >= 3) //Before Backtrack Final
        {
            echoTutorialState = sequenceState.COMPLETE;
            artifactRoomAttackTrigger.sequenceState = sequenceState.COMPLETE;
            artifactRoomAttackTrigger.triggered = true;
            firstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
            firstEncounterTrigger.triggered = true;
            backtrackHallwayState = sequenceState.COMPLETE;
            backtrackFinalTrigger.sequenceState = sequenceState.READY;
        }
        if (tutorialProgress >= 4) //After Teleporter Destruction
        {
            backtrackFinalTrigger.sequenceState = sequenceState.COMPLETE;
            backtrackFinalTrigger.triggered = true;
            teleportState = sequenceState.COMPLETE;
            lightSearchTrigger.sequenceState = sequenceState.READY;
            lightSearchTrigger.triggered = false;
            //New Submarine Room Geometry
            Destroy(tutorialGate);
            brokenSubmarine.transform.position = new Vector3(-289.74f, 6.15f, 782.82f);
            submarineInteractables.transform.position = brokenSubmarine.transform.position;
            Destroy(submarine);
            aspCore.GetComponent<Collider>().enabled = false;
        }
        if (tutorialProgress >= 5) //Before ASP-7 Core Install
        {
            lightSearchTrigger.sequenceState = sequenceState.COMPLETE;
            lightSearchTrigger.triggered = true;
            collectCoreTrigger.sequenceState = sequenceState.COMPLETE;
            collectCoreTrigger.triggered = true;
            aspInstallState = sequenceState.READY;
            aspCore.GetComponent<Collider>().enabled = true;
        }
        if (tutorialProgress >= 6) //Exiting Opening
        {
            aspInstallState = sequenceState.COMPLETE;
            scanTutorialState = sequenceState.COMPLETE;
            thePlanState = sequenceState.COMPLETE;
            //TODO: MORE NEW STATES HERE
        }
        
        //Set other unaccounted for settings
        switch (tutorialProgress)
        {
            case 0:
                StartCoroutine(startUpSequence());
                break;
            case 1:
                objectivePrompt.showPrompt(lightHuntObjective);
                musicConductor.crossfade(0f, musicConductor.tutorialTrack, 3f, 0f, 22.3f);
                break;
            case 2:
                interactPrompt.showPrompt(echoTutorialPrompt);
                musicConductor.crossfade(0f, musicConductor.lvl1Track, 3f, 0f, 25f);
                break;
            case 3:
                objectivePrompt.showPrompt(backtrackArtifactRoomObjective);
                musicConductor.crossfade(0f, musicConductor.lvl1Track, 0.5f, 0f, 78f);
                break;
            case 4:
                objectivePrompt.showPrompt(teleportObjective2);
                musicConductor.crossfade(0f, musicConductor.hummingTrack, 2f, 0f, 0f);
                break;
            case 5:
                objectivePrompt.showPrompt(collectCoreObjective);
                musicConductor.crossfade(0f, musicConductor.hummingTrack, 2f, 0f, 0f);
                break;
            case 6:
                objectivePrompt.showPrompt("TODO: INSERT PROPER OBJECTIVE PROMPT");
                musicConductor.crossfade(0f, musicConductor.deathTrack, 3f, 0f, 0f);
                break;
            default:
                StartCoroutine(startUpSequence());
                break;
        }
        isLoading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            return;
        }

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
        if (lightSearchTrigger.sequenceState == sequenceState.READY &&
            echoTutorialState == sequenceState.WAITING)
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

        //Return Trench Trigger
        if (lightSearchTrigger.sequenceState == sequenceState.READY &&
            echoTutorialState == sequenceState.COMPLETE)
        {
            if (lightSearchTrigger.triggered)
            {
                lightSearchTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(returnTrenchSequence());
            }
        }

        //Collect Core Trigger
        if (collectCoreTrigger.sequenceState == sequenceState.READY)
        {
            if (collectCoreTrigger.triggered)
            {
                collectCoreTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(collectCoreSequence());
            }
        }

        //Asp Install Trigger
        if (aspInstallState == sequenceState.READY)
        {
            if (aspCore.GetComponent<Interactable>().isUsed)
            {
                StartCoroutine(aspInstallSequence());
            }
        }

        //Scan Tutorial Trigger
        if (scanTutorialState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(aspInstallLastEnemies))
            {
                StartCoroutine(scanTutorialSequence());
            }
        }

        //The Plan Trigger
        if (thePlanState == sequenceState.READY)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(thePlanSequence());
            }
        }

        //Tunnel Trigger
        if (tunnelTrigger.sequenceState == sequenceState.READY)
        {
            if (tunnelTrigger.triggered)
            {
                tunnelTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(tunnelSequence());
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
            StartCoroutine(playRampingHummingSound(25f, 0.5f, 0.75f));
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

        /*
        tutorialMusic.Play();
        tutorialMusic.time = 25f;
        lvl1Music.Stop();
        deepOceanBassMusic.Stop();
        deathMusic.Stop();
        */

        //Fade into scene
        musicConductor.crossfade(0f, musicConductor.tutorialTrack, 4f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        Color fadeScreenColor = fadeScreenImage.color;
        for (int i = 0; i < 100; i++)
        {
            fadeScreenColor.a -= 0.01f;
            fadeScreenImage.color = fadeScreenColor;
            yield return new WaitForSeconds(0.03f);
        }
        fadeScreenImage.enabled = false;
        hummingSound.Stop();

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
        //Save Game
        tutorialProgress = 1;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(firstEncounterPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator firstEncounterSequence()
    {
        firstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        musicConductor.hummingTrack.track1.pitch = 3f;
        musicConductor.hummingTrack.track2.pitch = 3f;
        musicConductor.crossfade(2f, musicConductor.hummingTrack, 1f, 0f, 0f);
        //musicConductor.crossfade(3f, musicConductor.titleTrack, 1f, 0f, 1f);
        //musicConductor.tutorialTrack.loops = false;
        //musicConductor.crossfade(3f, musicConductor.tutorialTrack, 1.5f, 0f, 124f);
        //StartCoroutine(playRampingHummingSound(5f, 0.5f, 3f));
        firstEncounterEnemies = firstEncounterEnemySpawner.spawnEnemyWave(firstEncounterEnemyList);
        yield return new WaitForSeconds(0.75f);
        firstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
        room2AttackState = sequenceState.READY;
        yield return new WaitForSeconds(0.5f);
        //musicConductor.hummingTrack.track1.pitch = 2.5f;
        //musicConductor.hummingTrack.track2.pitch = 2.5f;
    }



    //ROOM 2 ATTACK SEQUENCE --------------------------------------------------------------------
    public IEnumerator room2AttackSequence()
    {
        room2AttackState = sequenceState.RUNNING;

        //hummingSound.Stop();
        //musicConductor.hummingTrack.track1.pitch = 1f;
        //musicConductor.hummingTrack.track2.pitch = 1f;
        musicConductor.crossfade(3f, musicConductor.tutorialTrack, 8f, 0f, 55f);
        yield return new WaitForSeconds(1f);
        //musicConductor.crossfade(15f, musicConductor.tutorialTrack, 10f, 0f, 55f);
        //musicConductor.crossfade(10f, musicConductor.tutorialTrack, 8f, 0f, 55f);
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
        yield return new WaitForSeconds(1.5f);
        room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList1);
        yield return new WaitForSeconds(2.5f);
        room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList2);
        yield return new WaitForSeconds(4f);
        room2AttackLastEnemies = room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList2);
        //yield return new WaitForSeconds(8f);
        //room2AttackLastEnemies = room2AttackEnemySpawner.spawnEnemyWave(room2AttackEnemyList4);
        yield return new WaitForSeconds(0.75f);
        //musicConductor.tutorialTrack.loops = true;
        room2AttackState = sequenceState.COMPLETE;
        room2AttackEndState = sequenceState.READY;
        musicConductor.hummingTrack.track1.pitch = 0.5f;
        musicConductor.hummingTrack.track2.pitch = 0.5f;
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
        yield return new WaitForSeconds(2f);
        artifactRoomAttackLastEnemies = artifactRoomEnemySpawner.spawnEnemyWave(artifactRoomAttackEnemyList2);
        yield return new WaitForSeconds(0.75f);
        artifactRoomAttackTrigger.sequenceState = sequenceState.COMPLETE;
        artifactRoomAttackEndState = sequenceState.READY;
    }



    //ARTIFACT ROOM ATTACK END SEQUENCE --------------------------------------------------------------------
    public IEnumerator artifactRoomAttackEndSequence()
    {
        artifactRoomAttackEndState = sequenceState.RUNNING;

        /*
        tutorialMusic.Stop();
        lvl1Music.Play();
        deepOceanBassMusic.Stop();
        deathMusic.Stop();
        */

        yield return new WaitForSeconds(0.75f);
        musicConductor.crossfade(5f, musicConductor.lvl1Track, 0f, 4.5f, 0f);
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
        //Save Game
        tutorialProgress = 2;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(echoTutorialPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
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
        //Save Game
        tutorialProgress = 3;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(backtrackFinalPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
    }



    //BACKTRACK FINAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator backtrackFinalSequence()
    {
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
        
        /*
        tutorialMusic.Stop();
        lvl1Music.Stop();
        deepOceanBassMusic.Play();
        deathMusic.Stop();
        */

        objectivePrompt.showPrompt(backtrackFinalObjective);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList1);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        //yield return new WaitForSeconds(1.25f);
        //room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList1);
        yield return new WaitForSeconds(0.75f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(3f);
        room1EnemySpawner.passiveSpawnActive = true;

        yield return new WaitForSeconds(3f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(4f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        //yield return new WaitForSeconds(4f);
        //room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList3);
        //room1EnemySpawner.passiveSpawnActive = false;

        yield return new WaitForSeconds(9f);
        room1EnemySpawner.passiveSpawnActive = false;
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(2.5f);
        room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList2);
        yield return new WaitForSeconds(4f);
        backtrackFinalLastEnemies = room1EnemySpawner.spawnEnemyWave(backtrackFinalEnemyList3);

        backtrackFinalTrigger.sequenceState = sequenceState.COMPLETE;
        teleportState = sequenceState.READY;
    }



    //TELEPORT SEQUENCE --------------------------------------------------------------------
    public IEnumerator teleportSequence()
    {
        teleportState = sequenceState.RUNNING;

        playerMovement.enabled = false;
        playerAttackHandler.enabled = false;

        objectivePrompt.hidePrompt();
        musicConductor.crossfade(5f, musicConductor.nullTrack, 0f, 0f, 0f);

        //New Submarine Room Geometry
        Destroy(tutorialGate);
        brokenSubmarine.transform.position = new Vector3(-289.74f, 6.15f, 782.82f);
        submarineInteractables.transform.position = brokenSubmarine.transform.position;
        Destroy(submarine);
        aspCore.GetComponent<Collider>().enabled = false;

        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", tutorialSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(teleportMessage1, tutorialSender, false);
            yield return new WaitForSeconds(0.75f);
            StartCoroutine(playRampingHummingSound(15f, 1f, 2.5f));
            StartCoroutine(teleportActivatingSequence());
            yield return new WaitForSeconds(0.5f);
            messanger.hideMessage();
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(teleportMessage2, tutorialSender, false);
            //StopCoroutine(teleportActivatingSequence());
            teleportFailed = true;
           // StartCoroutine(playRampingHummingSound(10f, 0.5f, 0.5f));
            hummingSound.Stop();
            //hummingSound.pitch = 0.5f;
            //hummingSound.Play();
            musicConductor.crossfade(0f, musicConductor.hummingTrack, 2f, 0f, 0f);
            objectivePrompt.showPrompt(teleportObjective1 + "[ERR]");
            whaleAttackSound.Play();
            damageSound.Play();
            yield return new WaitForSeconds(1.5f);
            messanger.hideMessage();
            yield return new WaitForSeconds(2.25f);
            whaleSound.Play();
            yield return new WaitForSeconds(1.25f);
            objectivePrompt.hidePrompt();
            yield return new WaitForSeconds(0.5f);

            yield return messanger.showMessage(teleportMessage3, backupSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(teleportMessage4, backupSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        objectivePrompt.showPrompt(teleportObjective2);
        playerMovement.enabled = true;
        playerAttackHandler.enabled = true;
        yield return new WaitForSeconds(1.5f);
        messanger.hideMessage();
        //musicConductor.crossfade(3f, musicConductor.deathTrack, 3f, 0f, 0f);

        teleportState = sequenceState.COMPLETE;
        lightSearchTrigger.triggered = false;
        lightSearchTrigger.sequenceState = sequenceState.READY;
        //Save Game
        tutorialProgress = 4;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(teleportPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();

        //yield return new WaitForSeconds(3f);
        //hummingSound.Stop();

        /*
        tutorialMusic.Stop();
        lvl1Music.Stop();
        deepOceanBassMusic.Stop();
        deathMusic.Play();
        deathMusic.Stop();
        */
    }



    //TELEPORT ACTIVATING SUBSEQUENCE --------------------------------------------------------------------
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



    //RETURN TRENCH SEQUENCE --------------------------------------------------------------------
    public IEnumerator returnTrenchSequence()
    {
        lightSearchTrigger.sequenceState = sequenceState.RUNNING;

        trenchEnemySpawner.spawnEnemyWave(trenchEnemyList1);
        yield return new WaitForSeconds(12f);
        trenchLastEnemies = trenchEnemySpawner.spawnEnemyWave(trenchEnemyList2);

        lightSearchTrigger.sequenceState = sequenceState.COMPLETE;
        collectCoreTrigger.sequenceState = sequenceState.READY;
    }



    //COLLECT CORE SEQUENCE --------------------------------------------------------------------
    public IEnumerator collectCoreSequence()
    {
        collectCoreTrigger.sequenceState = sequenceState.RUNNING;

        submarineEnemySpawner.spawnEnemyWave(submarineEnemyList1);
        objectivePrompt.hidePrompt();
        yield return new WaitForSeconds(7f);
        yield return messanger.showMessage("", backupSender, false);
        yield return new WaitForSeconds(0.5f);
        //musicConductor.crossfade(3f, musicConductor.deathTrack, 3f, 0f, 0f);
        whaleSound.volume = 0.04f;
        whaleSound.Play();
        yield return messanger.showMessage(collectCoreMessage1, backupSender, false);
        yield return new WaitForSeconds(1.25f);
        yield return messanger.showMessage(collectCoreMessage2, backupSender, false);
        yield return new WaitForSeconds(1.25f);
        objectivePrompt.showPrompt(collectCoreObjective);
        aspCore.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        messanger.hideMessage();

        collectCoreTrigger.sequenceState = sequenceState.COMPLETE;
        aspInstallState = sequenceState.READY;
        //Save Game
        tutorialProgress = 5;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(aspInstallPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
        //NEXTState = sequenceState.READY;
    }



    //ASP INSTALL SEQUENCE --------------------------------------------------------------------
    public IEnumerator aspInstallSequence()
    {
        aspInstallState = sequenceState.RUNNING;

        playerMovement.enabled = false;
        playerAttackHandler.enabled = false;

        Destroy(aspCore);
        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return new WaitForSeconds(1f);
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.75f);
            yield return messanger.showMessage(aspInstallMessage1, aspSender, false);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(aspInstallMessage2, aspSender, false);
            yield return new WaitForSeconds(0.25f);
            yield return messanger.showMessage(aspInstallMessage3, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(aspInstallMessage3p5, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(aspInstallMessage4, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        playerMovement.enabled = true;
        playerAttackHandler.enabled = true;
        int installProgress = 0;
        int installMaxProgress = 8;
        objectivePrompt.showPrompt(aspInstallObjective);
        yield return new WaitForSeconds(1.75f);
        messanger.hideMessage();
        yield return new WaitForSeconds(1.25f);
        //aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList1);
        yield return messanger.showMessage(aspInstallMessage5, aspSender, false);
        yield return new WaitForSeconds(1.25f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList1);
        aspInstallEnemySpawner.passiveSpawnActive = true;
        StartCoroutine(playRampingHummingSound(120f, 0.5f, 2f));

        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList1);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 1f + (((float) installProgress / (float) installMaxProgress) * 1f);
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage6, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList1);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 1f + (((float) installProgress / (float) installMaxProgress) * 1f);
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage7, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList2);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 1f + (((float) installProgress / (float) installMaxProgress) * 1f);
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage8, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList1);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 1f + (((float) installProgress / (float) installMaxProgress) * 1f);
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage9, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList2);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 1f + (((float) installProgress / (float) installMaxProgress) * 1f);
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage10, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        //StartCoroutine(playRampingHummingSound(50f, 1f, 3f));
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList3);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 0.8f;
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage11, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList1);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.pitch = 1f + (((float) installProgress / (float) installMaxProgress) * 1f);
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage12, aspSender, false);

        yield return new WaitForSeconds(0.25f);
        aspInstallEnemySpawner.passiveSpawnActive = false;
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList3);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        yield return messanger.showMessage("- " + installProgress + " of " + installMaxProgress + aspInstallMessageLoading, aspSender, false);
        completionSound.volume = 0.5f;
        completionSound.pitch = 0.25f;
        completionSound.Play();
        installProgress++;
        yield return messanger.showMessage(aspInstallMessage13, aspSender, false);
        //hummingSound.Stop();

        yield return new WaitForSeconds(0.25f);
        hummingSound.Stop();
        yield return messanger.showMessage(aspInstallMessage14, aspSender, false);
        yield return new WaitForSeconds(1.5f);
        yield return messanger.showMessage(aspInstallMessage15, aspSender, false);
        yield return new WaitForSeconds(2f);
        messanger.hideMessage();
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList3);
        yield return new WaitForSeconds(4f);
        aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList3);
        yield return new WaitForSeconds(4f);
        aspInstallLastEnemies = aspInstallEnemySpawner.spawnEnemyWave(aspInstallEnemyList3);

        aspInstallState = sequenceState.COMPLETE;
        scanTutorialState = sequenceState.READY;
    }



    //SCAN TUTORIAL SEQUENCE --------------------------------------------------------------------

    public IEnumerator scanTutorialSequence()
    {
        scanTutorialState = sequenceState.RUNNING;

        playerMovement.enabled = false;
        playerAttackHandler.enabled = false;

        //Destroy(submarineInteractables);
        hummingSound.Stop();
        objectivePrompt.hidePrompt();
        musicConductor.crossfade(10f, musicConductor.deathTrack, 3f, 3f, 0f);
        if (!fastSequencesDEV)
        {
            yield return new WaitForSeconds(1f);
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(scanTutorialMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(scanTutorialMessage2, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(scanTutorialMessage3, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(scanTutorialMessage4, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(scanTutorialMessage5, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(scanTutorialMessage6, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        interactPrompt.showPrompt(scanTutorialPrompt);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        scanTutorialState = sequenceState.COMPLETE;
        thePlanState = sequenceState.READY;
    }



    //THE PLAN SEQUENCE --------------------------------------------------------------------

    public IEnumerator thePlanSequence()
    {
        thePlanState = sequenceState.RUNNING;

        interactPrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return new WaitForSeconds(1f);
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(thePlanMessage1, aspSender, false);
            yield return new WaitForSeconds(0.25f);
            yield return messanger.showMessage(thePlanMessage2, aspSender, false);
            yield return new WaitForSeconds(0.25f);
            yield return messanger.showMessage(thePlanMessage3, aspSender, false);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(thePlanMessage4, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(thePlanMessage5, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(thePlanMessage6, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(thePlanMessage7, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        playerMovement.enabled = true;
        playerAttackHandler.enabled = true;

        foreach (LightOrb lightOrb in submarineInteractables.GetComponentsInChildren<LightOrb>())
        {
            lightOrb.regenerationCooldown = 4000;
        }

        objectivePrompt.showPrompt(thePlanObjective);
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();

        thePlanState = sequenceState.COMPLETE;
        tunnelTrigger.sequenceState = sequenceState.READY;
    }



    //TUNNEL SEQUENCE --------------------------------------------------------------------

    public IEnumerator tunnelSequence()
    {
        tunnelTrigger.sequenceState = sequenceState.RUNNING;

        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(tunnelMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(tunnelMessage2, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(tunnelMessage3, aspSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        messanger.hideMessage();

        tunnelTrigger.sequenceState = sequenceState.COMPLETE;
        //NEXTState = sequenceState.READY;
    }





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- UTILITIES ------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------

    //Hum pitcher
    public IEnumerator playRampingHummingSound(float duration, float initialPitch, float finalPitch)
    {
        hummingSound.Stop();
        hummingSound.pitch = initialPitch;
        hummingSound.Play();
        for (int i = 0; i < 50; i++)
        {
            //if (!teleportFailed)
            //{
            hummingSound.pitch += (finalPitch - initialPitch) / 50;
            //}
            yield return new WaitForSeconds(duration / 50);
        }
        //if (!teleportFailed)
        //{
        hummingSound.Stop();
        //}
    }



}



public enum sequenceState
{
    WAITING,
    READY,
    RUNNING,
    COMPLETE
}
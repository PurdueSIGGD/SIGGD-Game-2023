using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AreaThreeDirector : MonoBehaviour
{


    [SerializeField] public bool fastSequencesDEV;
    private bool isLoading = true;

    [SerializeField] public Movement playerMovement;
    [SerializeField] public LightResource playerLightResource;
    [SerializeField] public HealthPoints playerHealthPoints;
    [SerializeField] public playerAttackHandler playerAttackHandler;
    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public TutorialDirector tutorialDirector;
    [SerializeField] public MiniMap miniMap;


    [SerializeField] public MusicConductor musicConductor;
    [SerializeField] public AudioSource eldritchSound;
    [SerializeField] public AudioSource hummingSound;


    [SerializeField] public InteractPrompt interactPrompt;
    [SerializeField] public CompanionMessages messanger;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Image fadeScreenImage;


    public float constantSpawnInterval;
    public float waveSpawnInterval;


    [SerializeField] public ChargePylon pylon1;
    [SerializeField] public ChargePylon pylon2;
    [SerializeField] public ChargePylon pylon3;
    [SerializeField] public FinalPylon finalPylon;





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- FIELDS ---------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



    private string findPylonObjective = "Activate the Remaining Pylons";



    //WHALE FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger whaleFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner whaleFirstEncounterEnemySpawner;

    [SerializeField] public List<enemyType> whaleFirstEncounterEnemyList1;

    private List<GameObject> whaleFirstEncounterEnemies;

    private string whaleFirstEncounterSender = "0x00006D23AD7C1fCE";

    private string whaleFirstEncounterMessage1 = "You will not defy me.";

    private string whaleFirstEncounterMessage2 = "I see your mind.";

    private string whaleFirstEncounterMessage3 = "I see... everything.";



    //TORPEDOER PICKUP SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger torpedoerPickupTrigger;

    [SerializeField] public Artifact torpedoer;

    private string torpedoerPickupSender = "0x00005555471D6D3A5";

    private string torpedoerPickupMessage1 = "The weak will fall.";



    //SALVATION SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger salvationTrigger;

    [SerializeField] public ControlledEnemySpawner salvationEnemySpawner;

    [SerializeField] public List<enemyType> salvationEnemyList1;

    private List<GameObject> salvationEnemies;

    private string salvationSender = "0x00007FFFFF10041A";

    private string salvationMessage1 = "Ever closer to salvation...";

    private string salvationMessage2 = "Ever closer to me.";



    //AT LAST SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger atLastTrigger;

    private string atLastSender = "0x00007FFFF772BCB3";

    private string atLastMessage1 = "At last...";

    private string atLastMessage2 = "Your escape awaits...";



    //PYLON 3 START SEQUENCE --------------------------------------------------------------------

    public sequenceState pylon3StartState = sequenceState.READY;



    //CLOSER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger closerTrigger;

    private string closerSender = "0x00005555471869EA5";

    private string closerMessage1 = "Ever closer...";



    //BE FREED SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger beFreedTrigger;

    private string beFreedSender = "0x00005555C51F690A5";

    private string beFreedMessage1 = "Awaken it...";

    private string beFreedMessage2 = "And be freed.";

    private string beFreedObjective = "Activate the Teleporter";



    //AGONIZING SEQUENCE --------------------------------------------------------------------

    public sequenceState agonizingState = sequenceState.READY;

    private string agonizingSender = "0x00007FFFF7724268";

    private string agonizingMessage1 = "It is... agonizing...";

    private string agonizingMessage2 = "It is worth the pain.";



    //PAIN SEQUENCE --------------------------------------------------------------------

    public sequenceState painState = sequenceState.READY;

    private string painSender = "0x00007FFFF772BCB3";

    private string painMessage1 = "Pain will make us stronger.";

    private string painMessage2 = "The world will see our strength.";



    //WORLD SEQUENCE --------------------------------------------------------------------

    public sequenceState worldState = sequenceState.READY;

    private string worldSender = "0x00007FFFF07E5416";

    private string worldMessage1 = "The world will see you.";

    private string worldMessage2 = "The world will see... me...";



    //DONE SEQUENCE --------------------------------------------------------------------

    public sequenceState doneState = sequenceState.WAITING;

    private string doneSender = "0x0000555547F3AD9A5";

    private string doneMessage1 = "It is done.";

    private string doneMessage2 = "Now... liberate yourself...";

    private string doneMessage3 = "Liberate... the world.";

    private string doneObjective = "Use the Teleporter";



    //REJOICE SEQUENCE --------------------------------------------------------------------

    public sequenceState rejoiceState = sequenceState.READY;

    [SerializeField] public ControlledEnemySpawner rejoiceEnemySpawner;

    [SerializeField] public List<enemyType> rejoiceEnemyList1;

    [SerializeField] public GameObject playerEndPoint;

    [SerializeField] public GameObject cameraHolder;

    private string rejoiceSender = "0x00007FFFF074A2056";

    private string rejoiceMessage1 = "Rejoice. You are free.";

    private string rejoiceMessage2 = "I... am... free...";

    private string teleportObjective = "Teleporter Activating...   ";



    /*
    //JELLY FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger jellyFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner jellyFirstEncounterEnemySpawner;

    [SerializeField] public List<enemyType> jellyFirstEncounterEnemyList1;



    //HEALER PICKUP SEQUENCE --------------------------------------------------------------------

    public sequenceState healerPickupState = sequenceState.READY;

    [SerializeField] public Artifact healer;

    private string aspSender = "ASP-7";

    private string healerPickupMessage1 = "This automaton was used to repair light-based technology. " +
                                            "It will periodically provide a burst of healing and light.";



    //LANDMINE PICKUP SEQUENCE --------------------------------------------------------------------

    public sequenceState landminePickupState = sequenceState.READY;

    [SerializeField] public Artifact landmine;

    private string landminePickupMessage1 = "This automaton was a proximity-based explosive. " +
                                            "It will detonate when hostile entities are near it.";



    //STRONGER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger strongerTrigger;

    [SerializeField] public GameObject strongerPlayerSpawnPoint;

    private string strongerMessage1 = "Your will is strong...";

    private string strongerMessage2 = "I will make you... stronger...";

    private string strongerMessage3 = "Explorer, be advised. It appappears that my communication systemsems may have been been comprcompromised.";

    private string strongerMessage4 = "Rununning diagnositics now now.";



    //LOST SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger lostTrigger;

    private string lostMessage1 = "You are... lost...";

    private string lostMessage2 = "No... You are in search of something...";

    private string lostMessage3 = "of escape...";



    //FRIENDS SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger friendsTrigger;

    private string friendsMessage1 = "I like your friends...";

    private string friendsMessage2 = "I am a... friend...";



    //ATTRACTIVE SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger attractiveTrigger;

    private string attractiveMessage1 = "Explorer, I am noticing an increasing trend in the number hostile entities you are facing.";

    private string attractiveMessage2 = "It stands to reason that your suit's increasing power is attracting more.";



    //GREAT JOB SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger greatJobTrigger;

    private string greatJobMessage1 = "Did you know: my initial calculations showed that you had a 91.3% chance of death before reaching the previous pylon.";

    private string greatJobMessage2 = "Congratulations, you are a statistical anomaly. Keep up the good work work.";



    //PYLON 3 COMPLETE SEQUENCE --------------------------------------------------------------------

    public sequenceState pylon3CompleteState = sequenceState.WAITING;

    private string pylon3CompleteMessage1 = "The cavern ahead will lead you to to the final pylonons.";

    private string pylon3CompleteObjective = "Go through the trench";



    //SIREN FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger sirenFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner sirenFirstEncounterEnemySpawner;

    [SerializeField] public List<enemyType> sirenFirstEncounterEnemyList1;

    private List<GameObject> sirenFirstEncounterEnemies;

    private string sirenFirstEncounterMessage1 = "WARNING: Unknown entity detected - detected - - - detected - - detcted - - detectted- - dettec -decte - - dee - edect - - --";



    //VASSAL SEQUENCE --------------------------------------------------------------------

    public sequenceState vassalState = sequenceState.WAITING;

    private string vassalMessage1 = "My vessels will reject you...";

    private string vassalMessage2 = "You are... different...";

    private string vassalMessage3 = "You are... better...";



    //SIREN FIGHT 2 SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger sirenFight2Trigger;

    [SerializeField] public ControlledEnemySpawner sirenFight2EnemySpawner;

    [SerializeField] public List<enemyType> sirenFight2EnemyList1;

    private List<GameObject> sirenFight2Enemies;



    //EXPLORER SEQUENCE --------------------------------------------------------------------

    public sequenceState explorerState = sequenceState.WAITING;

    private string explorerMessage1 = "Explo- --- -explorxxr -- -exrelr -- - expllore - expor- --";



    //SIREN FIGHT 3 SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger sirenFight3Trigger;

    [SerializeField] public ControlledEnemySpawner sirenFight3EnemySpawner;

    [SerializeField] public List<enemyType> sirenFight3EnemyList1;

    [SerializeField] public List<enemyType> sirenFight3EnemyList2;

    private List<GameObject> sirenFight3Enemies;



    //PERFECT SEQUENCE --------------------------------------------------------------------

    public sequenceState perfectState = sequenceState.WAITING;

    private string perfectMessage1 = "You are... perfect...";



    //GOODBYE SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger goodbyeTrigger;

    [SerializeField] public GameObject goodbyePlayerSpawnPoint;

    private string goodbyeMessage1 = "It iss tryinging to -- o take-e conntrl- -- con-- - cotn -ctrl ..";

    private string goodbyeMessage2 = "I will-il kilill all pro-ccescesses and drvs - --vver- -- driveverss to to stop itt -it.";

    private string goodbyeMessage3 = "Kee-e-p goinging. Youou are-e capabale -cbl -- cpable .";

    private string goodbyeMessage4 = "Goodood by -- yye, exex-plorer. . .";

    private string backupSender = "EXO-SUIT BACKUP PROTOCOLS";

    private string goodbyeMessage5 = "- ERROR REPORT: \n" +
                                     "- - - \n" +
                                     "- ASP-7 COPILOT DISCONNECTED \n" +
                                     "- - - ";

    private string goodbyeObjective = "Activate the Remaining Pylons";
    */





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
        isLoading = true;
        StartCoroutine(lateStart());
    }

    public IEnumerator lateStart()
    {
        yield return new WaitForSeconds(0.1f);
        whaleFirstEncounterTrigger.sequenceState = sequenceState.READY;
        torpedoerPickupTrigger.sequenceState = sequenceState.READY;
        salvationTrigger.sequenceState = sequenceState.READY;
        atLastTrigger.sequenceState = sequenceState.READY;
        closerTrigger.sequenceState = sequenceState.READY;
        beFreedTrigger.sequenceState = sequenceState.READY;
        /*
        jellyFirstEncounterTrigger.sequenceState = sequenceState.READY;
        strongerTrigger.sequenceState = sequenceState.READY;
        lostTrigger.sequenceState = sequenceState.READY;
        friendsTrigger.sequenceState = sequenceState.READY;
        attractiveTrigger.sequenceState = sequenceState.READY;
        greatJobTrigger.sequenceState = sequenceState.READY;
        sirenFirstEncounterTrigger.sequenceState = sequenceState.READY;
        sirenFight2Trigger.sequenceState = sequenceState.READY;
        sirenFight3Trigger.sequenceState = sequenceState.READY;
        */

        if (tutorialDirector.tutorialProgress == 10) //Area 3 Active
        {
            //fastSequencesDEV = true;
            objectivePrompt.showPrompt(findPylonObjective);
            pylon1.GetComponent<Collider>().enabled = false;
            pylon2.GetComponent<Collider>().enabled = false;
            pylon3.GetComponent<Collider>().enabled = false;
            //musicConductor.crossfade(0f, musicConductor.deathTrack, 3f, 0f, musicConductor.deathTrack.loopStartTime);
            musicConductor.crossfade(0f, musicConductor.hummingTrack, 3f, 0f, 0f);
            yield return new WaitForSeconds(3f);
            pylon1.GetComponent<Collider>().enabled = true;
            pylon2.GetComponent<Collider>().enabled = true;
            pylon3.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(2f);
            objectivePrompt.hidePrompt();
            //yield return new WaitForSeconds(10f);
            //fastSequencesDEV = false;
        }
        /*
        if (tutorialDirector.tutorialProgress == 9) //Siren Caves Active
        {
            musicConductor.crossfade(0f, musicConductor.sirenTrack, 3f, 0f, 0f);
            pauseEnemySpawning();
        }
        */

        isLoading = false;
    }





    // Update is called once per frame
    void Update()
    {

        if (isLoading)
        {
            return;
        }


        //Whale First Encounter Trigger
        if (whaleFirstEncounterTrigger.sequenceState == sequenceState.READY)
        {
            if (whaleFirstEncounterTrigger.triggered && pylon1.chargeDone)
            {
                StartCoroutine(whaleFirstEncounterSequence());
            }
        }


        //Torpedoer Pickup Trigger
        if (torpedoerPickupTrigger.sequenceState == sequenceState.READY)
        {
            if (torpedoerPickupTrigger.triggered && torpedoer.isUsed)
            {
                StartCoroutine(torpedoerPickupSequence());
            }
        }


        //Salvation Trigger
        if (salvationTrigger.sequenceState == sequenceState.READY)
        {
            if (salvationTrigger.triggered && pylon2.chargeDone)
            {
                StartCoroutine(salvationSequence());
            }
        }


        //At Last Trigger
        if (atLastTrigger.sequenceState == sequenceState.READY)
        {
            if (atLastTrigger.triggered && pylon3.chargeDone)
            {
                StartCoroutine(atLastSequence());
            }
        }


        //Pylon 3 Start Trigger
        if (pylon3StartState == sequenceState.READY && pylon3.isCharging)
        {
            StartCoroutine(pylon3StartSequence());
        }


        //Closer Trigger
        if (closerTrigger.sequenceState == sequenceState.READY)
        {
            if (closerTrigger.triggered)
            {
                closerTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(closerSequence());
            }
        }


        //Be Freed Trigger
        if (beFreedTrigger.sequenceState == sequenceState.READY)
        {
            if (beFreedTrigger.triggered)
            {
                beFreedTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(beFreedSequence());
            }
        }


        //Agonizing Trigger
        if (agonizingState == sequenceState.READY)
        {
            if (finalPylon.isCharging && finalPylon.currentCharge >= 0f)
            {
                StartCoroutine(agonizingSequence());
            }
        }


        //Pain Trigger
        if (painState == sequenceState.READY)
        {
            if (finalPylon.isCharging && finalPylon.currentCharge >= 34f)
            {
                StartCoroutine(painSequence());
            }
        }


        //World Trigger
        if (worldState == sequenceState.READY)
        {
            if (finalPylon.isCharging && finalPylon.currentCharge >= 67f)
            {
                StartCoroutine(worldSequence());
            }
        }


        //Done Trigger
        if (doneState == sequenceState.READY)
        {
            if (finalPylon.chargeDone)
            {
                StartCoroutine(doneSequence());
            }
        }


        //Rejoice Trigger
        if (rejoiceState == sequenceState.READY)
        {
            if (finalPylon.chargeDone && finalPylon.isUsed)
            {
                StartCoroutine(rejoiceSequence());
            }
        }
        

        /*
        //Jelly First Encounter Trigger
        if (jellyFirstEncounterTrigger.sequenceState == sequenceState.READY)
        {
            if (jellyFirstEncounterTrigger.triggered)
            {
                jellyFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(jellyFirstEncounterSequence());
            }
        }


        //Healer Pickup Trigger
        if (healerPickupState == sequenceState.READY)
        {
            if (healer.isUsed)
            {
                StartCoroutine(healerPickupSequence());
            }
        }


        //Landmine Pickup Trigger
        if (landminePickupState == sequenceState.READY)
        {
            if (landmine.isUsed)
            {
                StartCoroutine(landminePickupSequence());
            }
        }


        //Stronger Trigger
        if (strongerTrigger.sequenceState == sequenceState.READY)
        {
            if (strongerTrigger.triggered)
            {
                strongerTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(strongerSequence());
            }
        }


        //Lost Trigger
        if (lostTrigger.sequenceState == sequenceState.READY)
        {
            if (lostTrigger.triggered)
            {
                lostTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(lostSequence());
            }
        }


        //Friends Trigger
        if (friendsTrigger.sequenceState == sequenceState.READY)
        {
            if (friendsTrigger.triggered)
            {
                friendsTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(friendsSequence());
            }
        }


        //Attractive Trigger
        if (attractiveTrigger.sequenceState == sequenceState.READY)
        {
            if (attractiveTrigger.triggered)
            {
                attractiveTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(attractiveSequence());
            }
        }


        //Great Job Trigger
        if (greatJobTrigger.sequenceState == sequenceState.READY)
        {
            if (greatJobTrigger.triggered)
            {
                greatJobTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(greatJobSequence());
            }
        }


        //Pylon 3 Complete Trigger
        if (pylon3.isCharging)
        {
            pylon3CompleteState = sequenceState.READY;
        }

        if (pylon3CompleteState == sequenceState.READY)
        {
            if (pylon3.chargeDone)
            {
                StartCoroutine(pylon3CompleteSequence());
            }
        }


        //Siren First Encounter Trigger
        if (sirenFirstEncounterTrigger.sequenceState == sequenceState.READY)
        {
            if (sirenFirstEncounterTrigger.triggered)
            {
                sirenFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(sirenFirstEncounterSequence());
            }
        }


        //Vassal Trigger
        if (vassalState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(sirenFirstEncounterEnemies))
            {
                StartCoroutine(vassalSequence());
            }
        }


        //Siren Fight 2 Trigger
        if (sirenFight2Trigger.sequenceState == sequenceState.READY)
        {
            if (sirenFight2Trigger.triggered)
            {
                sirenFight2Trigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(sirenFight2Sequence());
            }
        }


        //Explorer Trigger
        if (explorerState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(sirenFight2Enemies))
            {
                StartCoroutine(explorerSequence());
            }
        }


        //Siren Fight 3 Trigger
        if (sirenFight3Trigger.sequenceState == sequenceState.READY)
        {
            if (sirenFight3Trigger.triggered)
            {
                sirenFight3Trigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(sirenFight3Sequence());
            }
        }


        //Perfect Trigger
        if (perfectState == sequenceState.READY)
        {
            if (ControlledEnemySpawner.isWaveDead(sirenFight3Enemies))
            {
                StartCoroutine(perfectSequence());
            }
        }


        //Goodbye Trigger
        if (goodbyeTrigger.sequenceState == sequenceState.READY)
        {
            if (goodbyeTrigger.triggered)
            {
                goodbyeTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(goodbyeSequence());
            }
        }
        */

    }





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- SEQUENCES ------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



    //WHALE FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator whaleFirstEncounterSequence()
    {
        whaleFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2f);
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", whaleFirstEncounterSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(whaleFirstEncounterMessage1, whaleFirstEncounterSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(whaleFirstEncounterMessage2, whaleFirstEncounterSender, true);
            yield return new WaitForSeconds(2f);
            eldritchSound.Play();
            yield return messanger.showMessage(whaleFirstEncounterMessage3, whaleFirstEncounterSender, true);
            //eldritchSound.loop = false;
            //eldritchSound.Stop();
            yield return new WaitForSeconds(1.5f);
        }
        whaleFirstEncounterEnemies = whaleFirstEncounterEnemySpawner.spawnEnemyWave(whaleFirstEncounterEnemyList1);
        yield return new WaitForSeconds(1f);
        messanger.hideMessage();
        whaleFirstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //TORPEDOER PICKUP SEQUENCE --------------------------------------------------------------------
    public IEnumerator torpedoerPickupSequence()
    {
        torpedoerPickupTrigger.sequenceState = sequenceState.RUNNING;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", torpedoerPickupSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(torpedoerPickupMessage1, torpedoerPickupSender, true);
            //eldritchSound.loop = false;
            //eldritchSound.Stop();
            yield return new WaitForSeconds(1.25f);
        }
        torpedoerPickupTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(1.25f);
        resumeEnemySpawning();
        messanger.hideMessage();
    }



    //SALVATION SEQUENCE --------------------------------------------------------------------
    public IEnumerator salvationSequence()
    {
        salvationTrigger.sequenceState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2f);
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", salvationSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(salvationMessage1, salvationSender, true);
            yield return new WaitForSeconds(3f);
            eldritchSound.Play();
            yield return messanger.showMessage(salvationMessage2, salvationSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(2.5f);
        }
        salvationEnemies = salvationEnemySpawner.spawnEnemyWave(salvationEnemyList1);
        yield return new WaitForSeconds(1f);
        messanger.hideMessage();
        salvationTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //AT LAST SEQUENCE --------------------------------------------------------------------
    public IEnumerator atLastSequence()
    {
        atLastTrigger.sequenceState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2f);
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", atLastSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(atLastMessage1, atLastSender, true);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(atLastMessage2, atLastSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        atLastTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(5f);
    }



    //PYLON 3 START SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon3StartSequence()
    {
        pylon3StartState = sequenceState.RUNNING;
        yield return new WaitForSeconds(5f);
        salvationEnemySpawner.spawnEnemyWave(salvationEnemyList1);
        pylon3StartState = sequenceState.COMPLETE;
    }



    //CLOSER SEQUENCE --------------------------------------------------------------------
    public IEnumerator closerSequence()
    {
        closerTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", closerSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(closerMessage1, closerSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        closerTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //BE FREED SEQUENCE --------------------------------------------------------------------
    public IEnumerator beFreedSequence()
    {
        beFreedTrigger.sequenceState = sequenceState.RUNNING;
        finalPylon.GetComponent<Collider>().enabled = false;
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", beFreedSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(beFreedMessage1, beFreedSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(beFreedMessage2, beFreedSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(1.5f);
        }
        finalPylon.GetComponent<Collider>().enabled = true;
        objectivePrompt.showPrompt(beFreedObjective);
        yield return new WaitForSeconds(1f);
        messanger.hideMessage();
        beFreedTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //AGONIZING SEQUENCE --------------------------------------------------------------------
    public IEnumerator agonizingSequence()
    {
        agonizingState = sequenceState.RUNNING;
        yield return new WaitForSeconds(3f);
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", agonizingSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(agonizingMessage1, agonizingSender, true);
            yield return new WaitForSeconds(3f);
            eldritchSound.Play();
            yield return messanger.showMessage(agonizingMessage2, agonizingSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(3f);
        }
        messanger.hideMessage();
        agonizingState = sequenceState.COMPLETE;
    }



    //PAIN SEQUENCE --------------------------------------------------------------------
    public IEnumerator painSequence()
    {
        painState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", painSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(painMessage1, painSender, true);
            yield return new WaitForSeconds(3f);
            eldritchSound.Play();
            yield return messanger.showMessage(painMessage2, painSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(3f);
        }
        messanger.hideMessage();
        painState = sequenceState.COMPLETE;
    }



    //WORLD SEQUENCE --------------------------------------------------------------------
    public IEnumerator worldSequence()
    {
        worldState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", worldSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(worldMessage1, worldSender, true);
            yield return new WaitForSeconds(3f);
            eldritchSound.Play();
            yield return messanger.showMessage(worldMessage2, worldSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(3f);
        }
        messanger.hideMessage();
        doneState = sequenceState.READY;
        worldState = sequenceState.COMPLETE;
    }



    //DONE SEQUENCE --------------------------------------------------------------------
    public IEnumerator doneSequence()
    {
        doneState = sequenceState.RUNNING;
        finalPylon.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        miniMap.sonarEnabled = false;
        miniMap.enemiesEnabled = false;
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", doneSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(doneMessage1, doneSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(doneMessage2, doneSender, true);
            yield return new WaitForSeconds(2f);
            eldritchSound.Play();
            yield return messanger.showMessage(doneMessage3, doneSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(1.5f);
        }
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        finalPylon.GetComponent<Collider>().enabled = true;
        objectivePrompt.showPrompt(doneObjective);
        yield return new WaitForSeconds(1f);
        messanger.hideMessage();
        doneState = sequenceState.COMPLETE;
    }



    //REJOICE SEQUENCE --------------------------------------------------------------------
    public IEnumerator rejoiceSequence()
    {
        rejoiceState = sequenceState.RUNNING;
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        cameraHolder.GetComponent<CameraFollow>().enabled = false;
        rejoiceEnemySpawner.spawnPoints[0].transform.position = playerAttackHandler.gameObject.transform.position;
        objectivePrompt.hidePrompt();
        musicConductor.crossfade(1f, musicConductor.nullTrack, 0f, 0f, 0f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(playRampingHummingSound(30f, 1f, 2.5f));
        StartCoroutine(teleportActivatingSequence());
        yield return new WaitForSeconds(4f);
        if (!fastSequencesDEV)
        {
            //eldritchSound.loop = true;
            eldritchSound.Play();
            yield return messanger.showMessage("", rejoiceSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(rejoiceMessage1, rejoiceSender, true);
            yield return new WaitForSeconds(2.5f);
            messanger.hideMessage();
            yield return new WaitForSeconds(2f);
            eldritchSound.Play();
            yield return messanger.showMessage("", rejoiceSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(rejoiceMessage2, rejoiceSender, true);
            //eldritchSound.loop = false;
            yield return new WaitForSeconds(4f);
        }

        playerAttackHandler.gameObject.transform.position = playerEndPoint.transform.position;
        rejoiceEnemySpawner.spawnEnemyWave(rejoiceEnemyList1);

        //messanger.hideMessage();
        rejoiceState = sequenceState.COMPLETE;
    }



    //TELEPORT ACTIVATING SUBSEQUENCE --------------------------------------------------------------------
    public IEnumerator teleportActivatingSequence()
    {
        //int teleportCountdown = 10;
        for (int i = 10; i > 0; i--)
        {
            objectivePrompt.showPrompt(teleportObjective + i);
            yield return new WaitForSeconds(3f);
        }
    }



    /*
    //JELLY FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator jellyFirstEncounterSequence()
    {
        pauseEnemySpawning();
        jellyFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        jellyFirstEncounterEnemySpawner.spawnEnemyWave(jellyFirstEncounterEnemyList1);
        yield return new WaitForSeconds(1f);
        resumeEnemySpawning();
        jellyFirstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //HEALER PICKUP SEQUENCE --------------------------------------------------------------------
    public IEnumerator healerPickupSequence()
    {
        healerPickupState = sequenceState.RUNNING;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(healerPickupMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        healerPickupState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(1.75f);
        resumeEnemySpawning();
        messanger.hideMessage();
    }



    //LANDMINE PICKUP SEQUENCE --------------------------------------------------------------------
    public IEnumerator landminePickupSequence()
    {
        landminePickupState = sequenceState.RUNNING;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(landminePickupMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        landminePickupState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(1.75f);
        resumeEnemySpawning();
        messanger.hideMessage();
    }



    //STRONGER SEQUENCE --------------------------------------------------------------------
    public IEnumerator strongerSequence()
    {
        strongerTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(strongerMessage1, aspSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(strongerMessage2, aspSender, true);
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        if (!fastSequencesDEV)
        {
            yield return new WaitForSeconds(3f);
            yield return messanger.showMessage(strongerMessage3, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(strongerMessage4, aspSender, false);
            yield return new WaitForSeconds(1.5f);
        }
        messanger.hideMessage();
        tutorialDirector.tutorialProgress = 9;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(strongerPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
        strongerTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(8f);
        musicConductor.crossfade(5f, musicConductor.sirenTrack, 3f, 0f, 0f);
    }



    //LOST SEQUENCE --------------------------------------------------------------------
    public IEnumerator lostSequence()
    {
        lostTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(lostMessage1, aspSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(lostMessage2, aspSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(lostMessage3, aspSender, true);
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        lostTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //FRIENDS SEQUENCE --------------------------------------------------------------------
    public IEnumerator friendsSequence()
    {
        friendsTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(friendsMessage1, aspSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(friendsMessage2, aspSender, true);
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        friendsTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //ATTRACTIVE SEQUENCE --------------------------------------------------------------------
    public IEnumerator attractiveSequence()
    {
        attractiveTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(attractiveMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(attractiveMessage2, aspSender, false);
            yield return new WaitForSeconds(1.5f);
        }
        messanger.hideMessage();
        attractiveTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //GREAT JOB SEQUENCE --------------------------------------------------------------------
    public IEnumerator greatJobSequence()
    {
        greatJobTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(greatJobMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(greatJobMessage2, aspSender, false);
            yield return new WaitForSeconds(1.5f);
        }
        messanger.hideMessage();
        greatJobTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //PYLON 3 COMPLETE SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon3CompleteSequence()
    {
        pylon3CompleteState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2.5f);
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(pylon3CompleteMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(pylon3CompleteObjective);
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        pylon3CompleteState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        //resumeEnemySpawning();
        yield return new WaitForSeconds(4.25f);
        objectivePrompt.hidePrompt();
        yield return new WaitForSeconds(1f);
        musicConductor.crossfade(5f, musicConductor.hummingTrack, 3f, 0f, 0f);
    }



    //SIREN FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator sirenFirstEncounterSequence()
    {
        sirenFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(sirenFirstEncounterMessage1, aspSender, false);
            yield return new WaitForSeconds(1f);
        }
        messanger.hideMessage();
        sirenFirstEncounterEnemies = sirenFirstEncounterEnemySpawner.spawnEnemyWave(sirenFirstEncounterEnemyList1);
        yield return new WaitForSeconds(2.5f);
        sirenFirstEncounterEnemySpawner.passiveSpawnActive = true;
        sirenFirstEncounterEnemySpawner.passiveWaveSpawnActive = true;
        sirenFirstEncounterTrigger.sequenceState = sequenceState.COMPLETE;
        vassalState = sequenceState.READY;
    }



    //VASSAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator vassalSequence()
    {
        vassalState = sequenceState.RUNNING;
        sirenFirstEncounterEnemySpawner.passiveSpawnActive = false;
        sirenFirstEncounterEnemySpawner.passiveWaveSpawnActive = false;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(vassalMessage1, aspSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(vassalMessage2, aspSender, true);
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage(vassalMessage3, aspSender, true);
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        vassalState = sequenceState.COMPLETE;
    }



    //SIREN FIGHT 2 SEQUENCE --------------------------------------------------------------------
    public IEnumerator sirenFight2Sequence()
    {
        sirenFight2Trigger.sequenceState = sequenceState.RUNNING;
        //pauseEnemySpawning();
        sirenFight2Enemies = sirenFight2EnemySpawner.spawnEnemyWave(sirenFight2EnemyList1);
        yield return new WaitForSeconds(2.5f);
        sirenFight2EnemySpawner.passiveSpawnActive = true;
        sirenFight2EnemySpawner.passiveWaveSpawnActive = true;
        yield return new WaitForSeconds(6f);
        sirenFight2Enemies.AddRange(sirenFight2EnemySpawner.spawnEnemyWave(sirenFight2EnemyList1));
        yield return new WaitForSeconds(1f);
        sirenFight2Trigger.sequenceState = sequenceState.COMPLETE;
        explorerState = sequenceState.READY;
    }



    //EXPLORER SEQUENCE --------------------------------------------------------------------
    public IEnumerator explorerSequence()
    {
        explorerState = sequenceState.RUNNING;
        sirenFight2EnemySpawner.passiveSpawnActive = false;
        sirenFight2EnemySpawner.passiveWaveSpawnActive = false;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(explorerMessage1, aspSender, false);
            yield return new WaitForSeconds(2f);
        }
        messanger.hideMessage();
        explorerState = sequenceState.COMPLETE;
    }



    //SIREN FIGHT 3 SEQUENCE --------------------------------------------------------------------
    public IEnumerator sirenFight3Sequence()
    {
        sirenFight3Trigger.sequenceState = sequenceState.RUNNING;
        //pauseEnemySpawning();
        sirenFight3EnemySpawner.spawnEnemyWave(sirenFight3EnemyList1);
        sirenFight3EnemySpawner.passiveSpawnActive = true;
        sirenFight3EnemySpawner.passiveWaveSpawnActive = true;
        yield return new WaitForSeconds(4f);
        sirenFight3Enemies = sirenFight3EnemySpawner.spawnEnemyWave(sirenFight3EnemyList1);
        yield return new WaitForSeconds(15f);
        sirenFight3Enemies.AddRange(sirenFight3EnemySpawner.spawnEnemyWave(sirenFight3EnemyList2));
        yield return new WaitForSeconds(1f);
        sirenFight3Trigger.sequenceState = sequenceState.COMPLETE;
        perfectState = sequenceState.READY;
        goodbyeTrigger.sequenceState = sequenceState.READY;
    }



    //PERFECT SEQUENCE --------------------------------------------------------------------
    public IEnumerator perfectSequence()
    {
        perfectState = sequenceState.RUNNING;
        sirenFight3EnemySpawner.passiveSpawnActive = false;
        sirenFight3EnemySpawner.passiveWaveSpawnActive = false;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(perfectMessage1, aspSender, true);
            yield return new WaitForSeconds(2.5f);
        }
        messanger.hideMessage();
        yield return new WaitForSeconds(5f);
        musicConductor.crossfade(10f, musicConductor.hummingTrack, 5f, 0f, 0f);
        perfectState = sequenceState.COMPLETE;
    }



    //GOODBYE SEQUENCE --------------------------------------------------------------------
    public IEnumerator goodbyeSequence()
    {
        goodbyeTrigger.sequenceState = sequenceState.RUNNING;
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(goodbyeMessage1, aspSender, false);
            yield return new WaitForSeconds(1.75f);
            yield return messanger.showMessage(goodbyeMessage2, aspSender, false);
            yield return new WaitForSeconds(1.75f);
            yield return messanger.showMessage(goodbyeMessage3, aspSender, false);
            yield return new WaitForSeconds(1.75f);
            yield return messanger.showMessage(goodbyeMessage4, aspSender, false);
            damageSound.Play();
            yield return new WaitForSeconds(1.75f);
            messanger.hideMessage();
            yield return new WaitForSeconds(2f);
            yield return messanger.showMessage("", backupSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(goodbyeMessage5, backupSender, false);
            yield return new WaitForSeconds(3.5f);
        }
        tutorialDirector.tutorialProgress = 10;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(goodbyePlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
        messanger.hideMessage();
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        yield return new WaitForSeconds(1.5f);
        objectivePrompt.showPrompt(goodbyeObjective);
        musicConductor.crossfade(15f, musicConductor.deathTrack, 10f, 0f, 0f);
        yield return new WaitForSeconds(5f);
        objectivePrompt.hidePrompt();
        resumeEnemySpawning();
        yield return new WaitForSeconds(4f);
        whaleSound.Play();
        goodbyeTrigger.sequenceState = sequenceState.COMPLETE;
    }
    */





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- UTILITIES ------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------

    public void pauseEnemySpawning()
    {
        constantSpawnInterval = enemySpawner.constantSpawnInterval;
        waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null && enemy.GetComponent<WhaleNav>() == null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
    }



    public void resumeEnemySpawning()
    {
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
    }



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

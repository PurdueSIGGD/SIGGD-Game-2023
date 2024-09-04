using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AreaTwoDirector : MonoBehaviour
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
    [SerializeField] public AudioSource damageSound;
    [SerializeField] public AudioSource whaleSound;
    [SerializeField] public AudioSource ambushSound;


    [SerializeField] public InteractPrompt interactPrompt;
    [SerializeField] public CompanionMessages messanger;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Image fadeScreenImage;


    public float constantSpawnInterval;
    public float waveSpawnInterval;


    [SerializeField] public ChargePylon pylon1;
    [SerializeField] public ChargePylon pylon2;
    [SerializeField] public ChargePylon pylon3;

    [SerializeField] public Collider area2FinalWall;
    [SerializeField] public Collider siren1Wall;
    [SerializeField] public Collider siren2Wall;
    [SerializeField] public Collider siren3Wall;





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- FIELDS ---------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



    private string findPylonObjective = "Activate the Pylons";



    //JELLY FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger jellyFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner jellyFirstEncounterEnemySpawner;

    [SerializeField] public List<enemyType> jellyFirstEncounterEnemyList1;



    //HEALER PICKUP SEQUENCE --------------------------------------------------------------------

    //public sequenceState healerPickupState = sequenceState.READY;

    [SerializeField] public SequenceTrigger healerPickupTrigger;

    [SerializeField] public Artifact healer;

    private string aspSender = "ASP-7";

    //private string healerPickupMessage1 = "This automaton was used to repair light-based technology. " + 
    //"It will periodically provide a burst of healing and light.";

    private string healerPickupMessage1 = "This automaton was used to repair light-based technology. " +
                                            "It will provide a large burst of healing for a short time.";



    //LANDMINE PICKUP SEQUENCE --------------------------------------------------------------------

    //public sequenceState landminePickupState = sequenceState.READY;

    [SerializeField] public SequenceTrigger landminePickupTrigger;

    [SerializeField] public Artifact landmine;

    private string landminePickupMessage1 = "This automaton was a proximity-based explosive. " + 
                                            "It will detonate when hostile entities are near it.";



    //STRONGER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger strongerTrigger;

    [SerializeField] public GameObject strongerPlayerSpawnPoint;

    private string strongerMessage1 = "Your will is strong...";

    private string strongerMessage2 = "I will make you... stronger.";

    private string strongerMessage3 = "Explorer, be advised. It appappears that my communication systemsems may have been been comprcompromised.";

    private string strongerMessage4 = "Rununning diagnositics now now.";



    //LOST SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger lostTrigger;

    private string lostMessage1 = "You are... lost.";

    private string lostMessage2 = "No... You are in search of something.";

    private string lostMessage3 = "of escape...";



    //FRIENDS SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger friendsTrigger;

    private string friendsMessage1 = "I like your friends...";

    private string friendsMessage2 = "I am a... friend.";



    //ATTRACTIVE SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger attractiveTrigger;

    private string attractiveMessage1 = "Explorer, I am noticing an increasinging trend in the number hostile entities you are facing.";

    private string attractiveMessage2 = "It stands to reason that your suit's increasing power is is attracting more.";



    //GREAT JOB SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger greatJobTrigger;

    private string greatJobMessage1 = "Did you know: My initial calculations showed that you had a 91.3% chance of death before reaching the previous pylon.";

    private string greatJobMessage2 = "Congratulations, you are a statistical anomaly. Keep up the good work- wr-k.";



    //PYLON 3 COMPLETE SEQUENCE --------------------------------------------------------------------

    //public sequenceState pylon3CompleteState = sequenceState.WAITING;

    [SerializeField] public SequenceTrigger pylon3CompleteTrigger;

    private string pylon3CompleteMessage1 = "The cavern ahead will will lead you to the fna -fl -- na- fina-- final-l pylonons.";

    private string pylon3CompleteObjective = "Go through the trench";



    //SIREN FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger sirenFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner sirenFirstEncounterEnemySpawner;

    [SerializeField] public List<enemyType> sirenFirstEncounterEnemyList1;

    private List<GameObject> sirenFirstEncounterEnemies;

    private string sirenFirstEncounterMessage1 = "WARNING: Unknown entity detected - detected - - - detected - - detcted - - detectted- - dettec -decte - - dee - edect - - --";



    //VASSAL SEQUENCE --------------------------------------------------------------------

    public sequenceState vassalState = sequenceState.WAITING;

    private string vassalMessage1 = "My vessels will reject you.";

    private string vassalMessage2 = "You are... different.";

    private string vassalMessage3 = "You are... better.";



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

    private string goodbyeMessage3p5 = "Nevev-ever -stop -o p figt- -- gi -fgh - ffightinging- - .";

    private string goodbyeMessage4 = "Goodood by -- yye, exex-plorer. . .";

    private string backupSender = "EXO-SUIT BACKUP PROTOCOLS";

    private string goodbyeMessage5 = "- - - ERROR - - - \n" + 
                                     "- ASP-7 COPILOT DISCONNECTED . . .";

    private string goodbyeObjective = "Activate the Remaining Pylons";



    //BARRY AMBUSH SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger barryAmbushTrigger;

    [SerializeField] public ControlledEnemySpawner barryAmbushEnemySpawner;

    [SerializeField] public List<enemyType> barryAmbushEnemyList1;
    [SerializeField] public List<enemyType> barryAmbushEnemyList2;



    //BARRY 1 SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger barry1Trigger;

    [SerializeField] public ControlledEnemySpawner barry1EnemySpawner;

    [SerializeField] public List<enemyType> barry1EnemyList1;
    [SerializeField] public List<enemyType> barry1EnemyList2;



    //BARRY 2 SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger barry2Trigger;

    [SerializeField] public ControlledEnemySpawner barry2EnemySpawner;

    [SerializeField] public List<enemyType> barry2EnemyList1;
    [SerializeField] public List<enemyType> barry2EnemyList2;





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
        jellyFirstEncounterTrigger.sequenceState = sequenceState.READY;
        healerPickupTrigger.sequenceState = sequenceState.READY;
        landminePickupTrigger.sequenceState= sequenceState.READY;
        strongerTrigger.sequenceState = sequenceState.READY;
        lostTrigger.sequenceState = sequenceState.READY;
        friendsTrigger.sequenceState = sequenceState.READY;
        attractiveTrigger.sequenceState = sequenceState.READY;
        greatJobTrigger.sequenceState = sequenceState.READY;
        pylon3CompleteTrigger.sequenceState = sequenceState.READY;
        sirenFirstEncounterTrigger.sequenceState = sequenceState.READY;
        sirenFight2Trigger.sequenceState = sequenceState.READY;
        sirenFight3Trigger.sequenceState = sequenceState.READY;
        barryAmbushTrigger.sequenceState = sequenceState.READY;
        barry1Trigger.sequenceState = sequenceState.READY;
        barry2Trigger.sequenceState = sequenceState.READY;

        if (tutorialDirector.tutorialProgress == 8) //Area 2 Active
        {
            //fastSequencesDEV = true;
            objectivePrompt.showPrompt(findPylonObjective);
            pylon1.GetComponent<Collider>().enabled = false;
            pylon2.GetComponent<Collider>().enabled = false;
            pylon3.GetComponent<Collider>().enabled = false;
            musicConductor.crossfade(0f, musicConductor.titleTrack, 3f, 0f, musicConductor.titleTrack.loopStartTime);
            yield return new WaitForSeconds(3f);
            isLoading = false;
            pylon1.GetComponent<Collider>().enabled = true;
            pylon2.GetComponent<Collider>().enabled = true;
            pylon3.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(7f);
            objectivePrompt.hidePrompt();
            //yield return new WaitForSeconds(10f);
            //fastSequencesDEV = false;
        }
        if (tutorialDirector.tutorialProgress == 9) //Siren Caves Active
        {
            miniMap.sonarEnabled = true;
            miniMap.enemiesEnabled = false;
            musicConductor.crossfade(0f, musicConductor.sirenTrack, 3f, 0f, 0f);
            pauseEnemySpawning();
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


        //Jelly First Encounter Trigger
        if (jellyFirstEncounterTrigger.sequenceState == sequenceState.READY)
        {
            if (jellyFirstEncounterTrigger.triggered && !healer.isUsed)
            {
                jellyFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(jellyFirstEncounterSequence());
            }
        }


        /*
        //Healer Pickup Trigger
        if (healerPickupState == sequenceState.READY)
        {
            if (healer.isUsed)
            {
                StartCoroutine(healerPickupSequence());
            }
        }
        */


        //Healer Pickup Trigger
        if (healerPickupTrigger.sequenceState == sequenceState.READY)
        {
            if (healerPickupTrigger.triggered && healer.isUsed && !(landmine.isUsed || pylon1.chargeDone || pylon2.chargeDone))
            {
                StartCoroutine(healerPickupSequence());
            }
        }


        /*
        //Landmine Pickup Trigger
        if (landminePickupState == sequenceState.READY)
        {
            if (landmine.isUsed)
            {
                StartCoroutine(landminePickupSequence());
            }
        }
        */


        //Landmine Pickup Trigger
        if (landminePickupTrigger.sequenceState == sequenceState.READY)
        {
            if (landminePickupTrigger.triggered && landmine.isUsed)
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


        /*
        //Lost Trigger
        if (lostTrigger.sequenceState == sequenceState.READY)
        {
            if (lostTrigger.triggered)
            {
                lostTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(lostSequence());
            }
        }
        */


        //Lost Trigger
        if (lostTrigger.sequenceState == sequenceState.READY)
        {
            if (lostTrigger.triggered && !pylon2.chargeDone)
            {
                lostTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(lostSequence());
            }
        }


        //Friends Trigger
        if (friendsTrigger.sequenceState == sequenceState.READY)
        {
            if (friendsTrigger.triggered && !pylon3.chargeDone)
            {
                friendsTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(friendsSequence());
            }
        }


        //Attractive Trigger
        if (attractiveTrigger.sequenceState == sequenceState.READY)
        {
            if (attractiveTrigger.triggered && pylon1.chargeDone)
            {
                attractiveTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(attractiveSequence());
            }
        }


        //Great Job Trigger
        if (greatJobTrigger.sequenceState == sequenceState.READY)
        {
            if (greatJobTrigger.triggered && pylon2.chargeDone)
            {
                greatJobTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(greatJobSequence());
            }
        }


        /*
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
        */


        //Pylon 3 Complete Trigger
        if (pylon3CompleteTrigger.sequenceState == sequenceState.READY)
        {
            if (pylon3CompleteTrigger.triggered && pylon3.chargeDone)
            {
                pylon3CompleteTrigger.sequenceState = sequenceState.RUNNING;
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


        //Barry Ambush Trigger
        if (barryAmbushTrigger.sequenceState == sequenceState.READY)
        {
            if (barryAmbushTrigger.triggered)
            {
                barryAmbushTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(barryAmbushSequence());
            }
        }


        //Barry 1 Trigger
        if (barry1Trigger.sequenceState == sequenceState.READY)
        {
            if (barry1Trigger.triggered)
            {
                barry1Trigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(barry1Sequence());
            }
        }


        //Barry 2 Trigger
        if (barry2Trigger.sequenceState == sequenceState.READY)
        {
            if (barry2Trigger.triggered)
            {
                barry2Trigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(barry2Sequence());
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



    //JELLY FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator jellyFirstEncounterSequence()
    {
        miniMap.sonarEnabled = true;
        miniMap.enemiesEnabled = true;
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
        healerPickupTrigger.sequenceState = sequenceState.RUNNING;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(healerPickupMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        healerPickupTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(2.25f);
        resumeEnemySpawning();
        messanger.hideMessage();
    }



    //LANDMINE PICKUP SEQUENCE --------------------------------------------------------------------
    public IEnumerator landminePickupSequence()
    {
        landminePickupTrigger.sequenceState = sequenceState.RUNNING;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(landminePickupMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        landminePickupTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(2.25f);
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
        if (!fastSequencesDEV) {
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
        pylon3CompleteTrigger.sequenceState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2.5f);
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        miniMap.sonarEnabled = false;
        miniMap.enemiesEnabled = false;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(pylon3CompleteMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        area2FinalWall.enabled = false;
        objectivePrompt.showPrompt(pylon3CompleteObjective);
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        miniMap.sonarEnabled = true;
        miniMap.enemiesEnabled = false;
        pylon3CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        //resumeEnemySpawning();
        yield return new WaitForSeconds(5.25f);
        musicConductor.crossfade(10f, musicConductor.hummingTrack, 5f, 0f, 0f);
        yield return new WaitForSeconds(4f);
        objectivePrompt.hidePrompt();
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
        siren1Wall.enabled = false;
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
        siren2Wall.enabled = false;
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
        siren3Wall.enabled = false;
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
        miniMap.sonarEnabled = false;
        miniMap.enemiesEnabled = false;
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
            yield return messanger.showMessage(goodbyeMessage3p5, aspSender, false);
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
        miniMap.sonarEnabled = true;
        miniMap.enemiesEnabled = true;
        yield return new WaitForSeconds(1.5f);
        objectivePrompt.showPrompt(goodbyeObjective);
        //musicConductor.crossfade(15f, musicConductor.deathTrack, 10f, 0f, 0f);
        yield return new WaitForSeconds(10f);
        objectivePrompt.hidePrompt();
        resumeEnemySpawning();
        yield return new WaitForSeconds(4f);
        whaleSound.Play();
        goodbyeTrigger.sequenceState = sequenceState.COMPLETE;
    }



    //BARRY AMBUSH SEQUENCE --------------------------------------------------------------------
    public IEnumerator barryAmbushSequence()
    {
        barryAmbushTrigger.sequenceState = sequenceState.RUNNING;
        ambushSound.Play();
        barryAmbushEnemySpawner.spawnEnemyWave(barryAmbushEnemyList1);
        yield return new WaitForSeconds(7f);
        barryAmbushEnemySpawner.spawnEnemyWave(barryAmbushEnemyList2);
        yield return new WaitForSeconds(40f);
        barryAmbushTrigger.triggered = false;
        barryAmbushTrigger.sequenceState = sequenceState.READY;
    }



    //BARRY 1 SEQUENCE --------------------------------------------------------------------
    public IEnumerator barry1Sequence()
    {
        barry1Trigger.sequenceState = sequenceState.RUNNING;
        barry1EnemySpawner.spawnEnemyWave(barry1EnemyList1);
        yield return new WaitForSeconds(13f);
        barry1EnemySpawner.spawnEnemyWave(barry1EnemyList2);
        yield return new WaitForSeconds(40f);
        barry1Trigger.triggered = false;
        barry1Trigger.sequenceState = sequenceState.READY;
    }



    //BARRY 1 SEQUENCE --------------------------------------------------------------------
    public IEnumerator barry2Sequence()
    {
        barry2Trigger.sequenceState = sequenceState.RUNNING;
        barry2EnemySpawner.spawnEnemyWave(barry2EnemyList1);
        yield return new WaitForSeconds(13f);
        barry2EnemySpawner.spawnEnemyWave(barry2EnemyList2);
        yield return new WaitForSeconds(40f);
        barry2Trigger.triggered = false;
        barry2Trigger.sequenceState = sequenceState.READY;
    }





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
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
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



}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AreaOneDirector : MonoBehaviour
{


    [SerializeField] public bool fastSequencesDEV;
    private bool isLoading = true;

    [SerializeField] public Movement playerMovement;
    [SerializeField] public LightResource playerLightResource;
    [SerializeField] public HealthPoints playerHealthPoints;
    [SerializeField] public playerAttackHandler playerAttackHandler;
    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public TutorialDirector tutorialDirector;


    [SerializeField] public MusicConductor musicConductor;


    [SerializeField] public InteractPrompt interactPrompt;
    [SerializeField] public CompanionMessages messanger;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Image fadeScreenImage;


    public float constantSpawnInterval;
    public float waveSpawnInterval;


    [SerializeField] public ChargePylon pylon1;
    [SerializeField] public ChargePylon pylon2;
    [SerializeField] public ChargePylon pylon3;





    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------- FIELDS ---------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------



    private string findPylonObjective = "Activate the Pylons";



    //BARACUDA FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger baracudaFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner baracudaFirstEncounterEnemySpawner;

    //private string firstEncounterObjective = "Search for Light";

    [SerializeField] public List<enemyType> baracudaFirstEncounterEnemyList1;
    [SerializeField] public List<enemyType> baracudaFirstEncounterEnemyList2;

    private List<GameObject> baracudaFirstEncounterEnemies;

    private string baracudaFirstEncounterMessage1 = "WARNING: A group of unknown entities is rapidly approaching";



    //PYLON 1 ARRIVAL SEQUENCE ---------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger pylon1ArrivalTrigger;

    //[SerializeField] public ChargePylon pylon1;

    private string aspSender = "ASP-7";

    private string pylon1ArrivalMessage1 = "There it is. You must initiate the pylon's activation sequence.";

    private string pylon1ArrivalMessage2 = "Be warned. It will take some time to fully come online. During this time, the pylon will almost certainly " + 
                                           "attract a large number of hostile entities.";

    private string pylon1ArrivalMessage3 = "Go to the base of the pylon and activate it now.";

    private string pylon1ArrivalObjective = "Activate the Pylon";



    //PYLON 1 COMPLETE SEQUENCE --------------------------------------------------------------------------

    public sequenceState pylon1CompleteState = sequenceState.WAITING;

    private string pylon1CompleteMessage1 = "Good job, explorer. This pylon is now fully functional.";

    private string pylon1CompleteMessage2 = "Additionally, it appears the pylon's activation has enhanced the Exo-suit's power levels.";

    private string pylon1CompleteMessage3 = "Light capacity and suit integrity are increased, and the suit is now slowly regenerating power on its own.";

    private string pylon1CompleteMessage4 = "Now, you must continue on. I detect a pylon and a new automaton nearby. " +
                                            "I recommend you travel to both. You need any edge you can get down here.";

    private string pylon1CompleteObjective = "Travel to a point of interest";



    //SPLITTER NEAR SEQUENCE ---------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger splitterNearTrigger;

    private string splitterNearMessage1 = "The automaton is nearby";



    //SPLITTER PICKUP SEQUENCE ---------------------------------------------------------------------------

    public sequenceState splitterPickupState = sequenceState.WAITING;

    [SerializeField] public Artifact splitter;

    private string splitterPickupMessage1 = "This automaton was capable of splitting light blasts that passed through its prismic lens.";

    private string splitterPickupMessage2 = "Deploy the new echo, then shoot it for a more powerful attack.";

    private string splitterPickupPrompt = "SCROLL WHEEL | Select New Echo";



    //SPLITTER TUTORIAL SEQUENCE ---------------------------------------------------------------------------

    public sequenceState splitterTutorialState = sequenceState.WAITING;

    private string splitterTutorialPrompt = "LEFT SHIFT | Deploy Echo";



    //SPLITTER PLACED SEQUENCE ---------------------------------------------------------------------------

    public sequenceState splitterPlacedState = sequenceState.WAITING;

    [SerializeField] public ControlledEnemySpawner splitterPlacedEnemySpawner;
    
    [SerializeField] public List<enemyType> splitterPlacedEnemyList1;
    [SerializeField] public List<enemyType> splitterPlacedEnemyList2;

    private string splitterPlacedMessage1 = "Shoot the echo to hit many enemies at once. Try it now.";



    //PYLON 3 COMPLETE SEQUENCE --------------------------------------------------------------------------

    public sequenceState pylon3CompleteState = sequenceState.WAITING;

    //[SerializeField] public ChargePylon pylon3;

    private string pylon3CompleteMessage1 = "Well done. That was the final pylon in this area.";

    private string pylon3CompleteMessage2 = "Now we must pass through the trench ahead to progress.";

    private string pylon3CompleteMessage3 = "More pylons and atomatons are located on the other side.";

    private string pylon3CompleteObjective = "Go through the trench";



    //AWAKENING SEQUENCE --------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger awakeningTrigger;

    [SerializeField] public GameObject awakeningPlayerSpawnPoint;

    private string awakeningMessage1 = "I can... feel you...";

    private string awakeningMessage2 = "I'm detecting an atomaton at the end of the trench, explorer.";

    private string awakeningMessage3 = "I'm also detecting a new entity. Proceed with caution.";





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
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.READY;
        pylon1ArrivalTrigger.sequenceState = sequenceState.READY;
        splitterNearTrigger.sequenceState = sequenceState.READY;
        awakeningTrigger.sequenceState = sequenceState.READY;

        if (tutorialDirector.tutorialProgress == 7) //Area 1 Active
        {
            fastSequencesDEV = true;
            objectivePrompt.showPrompt(findPylonObjective);
            pylon1.GetComponent<Collider>().enabled = false;
            pylon2.GetComponent<Collider>().enabled = false;
            pylon3.GetComponent<Collider>().enabled = false;
            musicConductor.crossfade(0f, musicConductor.deathTrack, 3f, 0f, 0f);
            yield return new WaitForSeconds(3f);
            pylon1.GetComponent<Collider>().enabled = true;
            pylon2.GetComponent<Collider>().enabled = true;
            pylon3.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(2f);
            objectivePrompt.hidePrompt();
            yield return new WaitForSeconds(10f);
            fastSequencesDEV = false;
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



        //Barracuda First Encounter Trigger
        if (baracudaFirstEncounterTrigger.sequenceState == sequenceState.READY)
        {
            if (baracudaFirstEncounterTrigger.triggered)
            {
                baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(baracudaFirstEncounterSequence());
            }
        }



        //Pylon 1 Arrival Trigger
        if (pylon1ArrivalTrigger.sequenceState == sequenceState.READY)
        {
            if (pylon1ArrivalTrigger.triggered && !pylon1.chargeDone && !pylon1.isCharging)
            {
                pylon1ArrivalTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(pylon1ArrivalSequence());
            }
        }



        //Pylon 1 Complete Trigger
        if (pylon1CompleteState == sequenceState.READY)
        {
            if (pylon1.chargeDone)
            {
                StartCoroutine(pylon1CompleteSequence());
            }
        }



        //Splitter Near Trigger
        if (splitterNearTrigger.sequenceState == sequenceState.READY)
        {
            if (splitterNearTrigger.triggered && !splitter.isUsed)
            {
                splitterNearTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(splitterNearSequence());
            }
        }



        //Splitter Pickup Trigger
        if (splitterPickupState == sequenceState.READY)
        {
            if (splitter.isUsed)
            {
                StartCoroutine(splitterPickupSequence());
            }
        }



        //Splitter Tutorial Trigger
        if (splitterTutorialState == sequenceState.READY)
        {
            if (Input.mouseScrollDelta.magnitude != 0f)
            {
                StartCoroutine(splitterTutorialSequence());
            }
        }



        //Splitter Placed Trigger
        if (splitterPlacedState == sequenceState.READY)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(splitterPlacedSequence());
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


        //Awakening Trigger
        if (awakeningTrigger.sequenceState == sequenceState.READY)
        {
            if (awakeningTrigger.triggered)
            {
                awakeningTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(awakeningSequence());
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



    //BARACUDA FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator baracudaFirstEncounterSequence()
    {
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(baracudaFirstEncounterMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
        }
        messanger.hideMessage();
        baracudaFirstEncounterEnemies = baracudaFirstEncounterEnemySpawner.spawnEnemyWave(baracudaFirstEncounterEnemyList1);
        yield return new WaitForSeconds(20f);
        baracudaFirstEncounterEnemies = baracudaFirstEncounterEnemySpawner.spawnEnemyWave(baracudaFirstEncounterEnemyList2);
        yield return new WaitForSeconds(40f);
        baracudaFirstEncounterTrigger.triggered = false;
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.READY;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 1 ARRIVAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon1ArrivalSequence()
    {
        pylon1ArrivalTrigger.sequenceState = sequenceState.RUNNING;
        pylon1.GetComponent<Collider>().enabled = false;

        pauseEnemySpawning();
        yield return new WaitForSeconds(0.75f);
        objectivePrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(pylon1ArrivalMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon1ArrivalMessage2, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon1ArrivalMessage3, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(pylon1ArrivalObjective);
        pylon1.GetComponent<Collider>().enabled = true;
        pylon1ArrivalTrigger.sequenceState = sequenceState.COMPLETE;
        pylon1CompleteState = sequenceState.READY;
        resumeEnemySpawning();
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //PYLON 1 COMPLETE SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon1CompleteSequence()
    {
        pylon1CompleteState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2.5f);
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(pylon1CompleteMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon1CompleteMessage2, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon1CompleteMessage3, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon1CompleteMessage4, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(pylon1CompleteObjective);
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        pylon1CompleteState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        resumeEnemySpawning();
        yield return new WaitForSeconds(4.25f);
        objectivePrompt.hidePrompt();
    }



    //SPLITTER NEAR SEQUENCE --------------------------------------------------------------------
    public IEnumerator splitterNearSequence()
    {
        splitterNearTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(splitterNearMessage1, aspSender, false);
            yield return new WaitForSeconds(1.5f);
        }
        messanger.hideMessage();
        splitterNearTrigger.sequenceState = sequenceState.COMPLETE;
        splitterPickupState = sequenceState.READY;
    }



    //SPLITTER PICKUP SEQUENCE --------------------------------------------------------------------
    public IEnumerator splitterPickupSequence()
    {
        splitterPickupState = sequenceState.RUNNING;
        playerMovement.rooted = true;
        playerAttackHandler.enabled = false;
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(splitterPickupMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(splitterPickupMessage2, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        interactPrompt.showPrompt(splitterPickupPrompt);
        splitterPickupState = sequenceState.COMPLETE;
        splitterTutorialState = sequenceState.READY;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
    }



    //SPLITTER TUTORIAL SEQUENCE --------------------------------------------------------------------
    public IEnumerator splitterTutorialSequence()
    {
        splitterTutorialState = sequenceState.RUNNING;
        yield return new WaitForSeconds(0.75f);
        interactPrompt.hidePrompt();
        yield return new WaitForSeconds(0.5f);
        interactPrompt.showPrompt(splitterTutorialPrompt);
        splitterTutorialState = sequenceState.COMPLETE;
        splitterPlacedState = sequenceState.READY;
    }



    //SPLITTER PLACED SEQUENCE --------------------------------------------------------------------
    public IEnumerator splitterPlacedSequence()
    {
        splitterPlacedState = sequenceState.RUNNING;
        yield return new WaitForSeconds(0.5f);
        interactPrompt.hidePrompt();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(splitterPlacedMessage1, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        splitterPlacedEnemySpawner.spawnEnemyWave(splitterPlacedEnemyList1);
        yield return new WaitForSeconds(4f);
        splitterPlacedEnemySpawner.spawnEnemyWave(splitterPlacedEnemyList2);
        yield return new WaitForSeconds(4f);
        resumeEnemySpawning();
        splitterPlacedState = sequenceState.COMPLETE;
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
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon3CompleteMessage2, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon3CompleteMessage3, aspSender, false);
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



    //AWAKENING SEQUENCE --------------------------------------------------------------------
    public IEnumerator awakeningSequence()
    {
        awakeningTrigger.sequenceState = sequenceState.RUNNING;
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(1.5f);
            yield return messanger.showMessage(awakeningMessage1, aspSender, true);
            yield return new WaitForSeconds(2.5f);
            messanger.hideMessage();
            yield return new WaitForSeconds(6.5f);
            yield return messanger.showMessage(awakeningMessage2, aspSender, false);
            yield return new WaitForSeconds(1.75f);
            yield return messanger.showMessage(awakeningMessage3, aspSender, false);
            yield return new WaitForSeconds(1.5f);
        }
        messanger.hideMessage();
        awakeningTrigger.sequenceState = sequenceState.COMPLETE;

        tutorialDirector.tutorialProgress = 8;
        SaveManager saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(awakeningPlayerSpawnPoint.transform.position);
        saveManager.SaveGame();
        yield return new WaitForSeconds(5f);
        musicConductor.crossfade(15f, musicConductor.titleTrack, 10f, 0f, musicConductor.titleTrack.loopStartTime);
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

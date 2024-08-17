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

    private string baracudaFirstEncounterMessage1 = "WARNING: A group of unknown entities are rapidly approaching";



    //PYLON 1 ARRIVAL SEQUENCE ---------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger pylon1ArrivalTrigger;

    [SerializeField] public ChargePylon pylon1;

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



    /*
    //PYLON 1 SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylon1State = sequenceState.READY;

    [SerializeField] public ChargePylon pylon1;

    [SerializeField] public ControlledEnemySpawner pylon1EnemySpawner;

    [SerializeField] public List<enemyType> pylon1EnemyList1;
    [SerializeField] public List<enemyType> pylon1EnemyList2;
    [SerializeField] public List<enemyType> pylon1EnemyList3;
    [SerializeField] public List<enemyType> pylon1EnemyList4;
    [SerializeField] public List<enemyType> pylon1EnemyList5;

    private List<GameObject> pylon1Enemies;



    //PYLON 2 SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylon2State = sequenceState.READY;

    [SerializeField] public ChargePylon pylon2;

    [SerializeField] public ControlledEnemySpawner pylon2EnemySpawner;

    [SerializeField] public List<enemyType> pylon2EnemyList1;
    [SerializeField] public List<enemyType> pylon2EnemyList2;
    [SerializeField] public List<enemyType> pylon2EnemyList3;
    [SerializeField] public List<enemyType> pylon2EnemyList4;
    [SerializeField] public List<enemyType> pylon2EnemyList5;

    private List<GameObject> pylon2Enemies;



    //PYLON 3 SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylon3State = sequenceState.READY;

    [SerializeField] public ChargePylon pylon3;

    [SerializeField] public ControlledEnemySpawner pylon3EnemySpawner;

    [SerializeField] public List<enemyType> pylon3EnemyList1;
    [SerializeField] public List<enemyType> pylon3EnemyList2;
    [SerializeField] public List<enemyType> pylon3EnemyList3;
    [SerializeField] public List<enemyType> pylon3EnemyList4;
    [SerializeField] public List<enemyType> pylon3EnemyList5;

    private List<GameObject> pylon3Enemies;



    //PYLON 3 SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylon4State = sequenceState.READY;

    [SerializeField] public ChargePylon pylon4;

    [SerializeField] public ControlledEnemySpawner pylon4EnemySpawner;

    [SerializeField] public List<enemyType> pylon4EnemyList1;
    [SerializeField] public List<enemyType> pylon4EnemyList2;
    [SerializeField] public List<enemyType> pylon4EnemyList3;
    [SerializeField] public List<enemyType> pylon4EnemyList4;
    [SerializeField] public List<enemyType> pylon4EnemyList5;

    private List<GameObject> pylon4Enemies;



    //PYLON 3 SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylon5State = sequenceState.READY;

    [SerializeField] public ChargePylon pylon5;

    [SerializeField] public ControlledEnemySpawner pylon5EnemySpawner;

    [SerializeField] public List<enemyType> pylon5EnemyList1;
    [SerializeField] public List<enemyType> pylon5EnemyList2;
    [SerializeField] public List<enemyType> pylon5EnemyList3;
    [SerializeField] public List<enemyType> pylon5EnemyList4;
    [SerializeField] public List<enemyType> pylon5EnemyList5;

    private List<GameObject> pylon5Enemies;



    //PYLON 3 SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylon6State = sequenceState.READY;

    [SerializeField] public ChargePylon pylon6;

    [SerializeField] public ControlledEnemySpawner pylon6EnemySpawner;

    [SerializeField] public List<enemyType> pylon6EnemyList1;
    [SerializeField] public List<enemyType> pylon6EnemyList2;
    [SerializeField] public List<enemyType> pylon6EnemyList3;
    [SerializeField] public List<enemyType> pylon6EnemyList4;
    [SerializeField] public List<enemyType> pylon6EnemyList5;

    private List<GameObject> pylon6Enemies;
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
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.READY;
        pylon1ArrivalTrigger.sequenceState = sequenceState.READY;
        splitterNearTrigger.sequenceState = sequenceState.READY;

        if (tutorialDirector.tutorialProgress == 7) //Area 1 Active
        {
            fastSequencesDEV = true;
            objectivePrompt.showPrompt(findPylonObjective);
            musicConductor.crossfade(0f, musicConductor.deathTrack, 3f, 0f, 0f);
            yield return new WaitForSeconds(5f);
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



        //First Encounter Trigger
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
            if (pylon1ArrivalTrigger.triggered && !pylon1.chargeDone)
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



        /*
        //Pylon 1 Trigger
        if (pylon1State == sequenceState.READY)
        {
            if (pylon1.isUsed)
            {
                pylon1State = sequenceState.RUNNING;
                StartCoroutine(pylon1Sequence());
            }
        }



        //Pylon 2 Trigger
        if (pylon2State == sequenceState.READY)
        {
            if (pylon2.isUsed)
            {
                pylon2State = sequenceState.RUNNING;
                StartCoroutine(pylon2Sequence());
            }
        }



        //Pylon 3 Trigger
        if (pylon3State == sequenceState.READY)
        {
            if (pylon3.isUsed)
            {
                pylon3State = sequenceState.RUNNING;
                StartCoroutine(pylon3Sequence());
            }
        }



        //Pylon 3 Trigger
        if (pylon4State == sequenceState.READY)
        {
            if (pylon4.isUsed)
            {
                pylon4State = sequenceState.RUNNING;
                StartCoroutine(pylon4Sequence());
            }
        }



        //Pylon 3 Trigger
        if (pylon5State == sequenceState.READY)
        {
            if (pylon5.isUsed)
            {
                pylon5State = sequenceState.RUNNING;
                StartCoroutine(pylon5Sequence());
            }
        }



        //Pylon 3 Trigger
        if (pylon6State == sequenceState.READY)
        {
            if (pylon6.isUsed)
            {
                pylon6State = sequenceState.RUNNING;
                StartCoroutine(pylon6Sequence());
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



    /*
    //PYLON 1 SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon1Sequence()
    {
        pylon1State = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon1EnemySpawner.passiveSpawnActive = true;
        pylon1EnemySpawner.passiveWaveSpawnActive = true;
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList3);
        yield return new WaitForSeconds(14f);

        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList2);
        yield return new WaitForSeconds(8f);
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList3);
        yield return new WaitForSeconds(8f);
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList4);
        yield return new WaitForSeconds(10f);
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList2);
        yield return new WaitForSeconds(10f);
        pylon1EnemySpawner.passiveSpawnActive = false;
        pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon1Enemies = pylon1EnemySpawner.spawnEnemyWave(pylon1EnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylon1Enemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylon1State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 2 SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon2Sequence()
    {
        pylon2State = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon2EnemySpawner.passiveSpawnActive = true;
        pylon2EnemySpawner.passiveWaveSpawnActive = true;
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList3);
        yield return new WaitForSeconds(14f);

        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList2);
        yield return new WaitForSeconds(8f);
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList3);
        yield return new WaitForSeconds(8f);
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList4);
        yield return new WaitForSeconds(10f);
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList2);
        yield return new WaitForSeconds(10f);
        pylon2EnemySpawner.passiveSpawnActive = false;
        pylon2EnemySpawner.passiveWaveSpawnActive = false;
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylon2Enemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylon2State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 3 SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon3Sequence()
    {
        pylon3State = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon3EnemySpawner.passiveSpawnActive = true;
        pylon3EnemySpawner.passiveWaveSpawnActive = true;
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList3);
        yield return new WaitForSeconds(14f);

        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList2);
        yield return new WaitForSeconds(8f);
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList3);
        yield return new WaitForSeconds(8f);
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList4);
        yield return new WaitForSeconds(10f);
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList2);
        yield return new WaitForSeconds(10f);
        pylon3EnemySpawner.passiveSpawnActive = false;
        pylon3EnemySpawner.passiveWaveSpawnActive = false;
        pylon3Enemies = pylon3EnemySpawner.spawnEnemyWave(pylon3EnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylon3Enemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylon3State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }




    //PYLON 4 SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon4Sequence()
    {
        pylon4State = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon4EnemySpawner.passiveSpawnActive = true;
        pylon4EnemySpawner.passiveWaveSpawnActive = true;
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList3);
        yield return new WaitForSeconds(14f);

        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList2);
        yield return new WaitForSeconds(8f);
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList3);
        yield return new WaitForSeconds(8f);
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList4);
        yield return new WaitForSeconds(10f);
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList2);
        yield return new WaitForSeconds(10f);
        pylon4EnemySpawner.passiveSpawnActive = false;
        pylon4EnemySpawner.passiveWaveSpawnActive = false;
        pylon4Enemies = pylon4EnemySpawner.spawnEnemyWave(pylon4EnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylon4Enemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylon4State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 5 SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon5Sequence()
    {
        pylon5State = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon5EnemySpawner.passiveSpawnActive = true;
        pylon5EnemySpawner.passiveWaveSpawnActive = true;
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList3);
        yield return new WaitForSeconds(14f);

        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList2);
        yield return new WaitForSeconds(8f);
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList3);
        yield return new WaitForSeconds(8f);
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList4);
        yield return new WaitForSeconds(10f);
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList2);
        yield return new WaitForSeconds(10f);
        pylon5EnemySpawner.passiveSpawnActive = false;
        pylon5EnemySpawner.passiveWaveSpawnActive = false;
        pylon5Enemies = pylon5EnemySpawner.spawnEnemyWave(pylon5EnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylon5Enemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylon5State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 6 SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon6Sequence()
    {
        pylon6State = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList1);
        yield return new WaitForSeconds(8f);
        pylon6EnemySpawner.passiveSpawnActive = true;
        pylon6EnemySpawner.passiveWaveSpawnActive = true;
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList3);
        yield return new WaitForSeconds(14f);

        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList2);
        yield return new WaitForSeconds(8f);
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList3);
        yield return new WaitForSeconds(8f);
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList4);
        yield return new WaitForSeconds(10f);
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList2);
        yield return new WaitForSeconds(10f);
        pylon6EnemySpawner.passiveSpawnActive = false;
        pylon6EnemySpawner.passiveWaveSpawnActive = false;
        pylon6Enemies = pylon6EnemySpawner.spawnEnemyWave(pylon6EnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylon6Enemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylon6State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }
    */



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

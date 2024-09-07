using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
//using UnityEditor.Experimental.GraphView;
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
    [SerializeField] public MiniMap miniMap;


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

    [SerializeField] public Collider area1FinalWall;

    private int pylonsCompleted = 0;
    private bool pylon1Checked = false;
    private bool pylon2Checked = false;
    private bool pylon3Checked = false;

    [SerializeField] public GameObject splitterMinimapIcon;
    [SerializeField] public GameObject pylon2MinimapIcon;
    [SerializeField] public GameObject pylon3MinimapIcon;





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
                                            "I recommend you travel to both. You need any edge you can get.";

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

    public sequenceState pylon3CompleteState = sequenceState.READY;

    [SerializeField] public SequenceTrigger pylon3CompleteTrigger;
    
    //[SerializeField] public ChargePylon pylon3;

    private string pylon3CompleteMessage1 = "Well done. That was the final pylon in this area.";

    private string pylon3CompleteMessage2 = "Now you must pass through the trench ahead to progress.";

    private string pylon3CompleteMessage3 = "More pylons and atomatons are located on the other side.";

    private string pylon3CompleteObjective = "Go through the trench";



    //AWAKENING SEQUENCE --------------------------------------------------------------------------

    [SerializeField] public SequenceTrigger awakeningTrigger;

    [SerializeField] public GameObject awakeningPlayerSpawnPoint;

    private string awakeningMessage1 = "I can... feel you...";

    private string awakeningMessage2 = "I'm detecting an atomaton at the end of the trench, explorer.";

    private string awakeningMessage3 = "I'm also detecting a new entity. Proceed with caution.";



    //PYLON 2 COMPLETE SEQUENCE --------------------------------------------------------------------------

    public sequenceState pylon2CompleteState = sequenceState.READY;

    [SerializeField] public SequenceTrigger pylon2CompleteTrigger;

    //private string pylon2CompleteMessage1 = "There is one more pylon nearby. Keep moving.";

    private string pylon2CompleteMessage1 = "Activation successful. Pylon online.";

    private string pylon2CompleteMessage2A = "There is one more pylon nearby. Keep moving.";

    private string pylon2CompleteMessage2B1 = "I suggest that you travel to the automaton next.";

    private string pylon2CompleteMessage2B2 = "It will be marked as a round dot on your sonar map when you release a sonar ping.";



    //BARRY SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger barryTrigger;

    [SerializeField] public ControlledEnemySpawner barryEnemySpawner;

    [SerializeField] public List<enemyType> barryEnemyList1;
    [SerializeField] public List<enemyType> barryEnemyList2;





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
        pylonsCompleted = 0;
        pylon1Checked = false;
        pylon2Checked = false;
        pylon3Checked = false;
        isLoading = true;
        StartCoroutine(lateStart());
    }

    public IEnumerator lateStart()
    {
        yield return new WaitForSeconds(0.1f);
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.READY;
        pylon1ArrivalTrigger.sequenceState = (pylon1.chargeDone) ? sequenceState.COMPLETE : sequenceState.READY;
        splitterNearTrigger.sequenceState = (splitter.isUsed) ? sequenceState.COMPLETE : sequenceState.READY;
        pylon3CompleteTrigger.sequenceState = sequenceState.READY;
        awakeningTrigger.sequenceState = sequenceState.READY;
        pylon2CompleteTrigger.sequenceState = sequenceState.READY;
        barryTrigger.sequenceState = sequenceState.READY;

        if (tutorialDirector.tutorialProgress == 7) //Area 1 Active
        {
            fastSequencesDEV = true;
            objectivePrompt.showPrompt(findPylonObjective);
            pylon1.GetComponent<Collider>().enabled = false;
            pylon2.GetComponent<Collider>().enabled = false;
            pylon3.GetComponent<Collider>().enabled = false;
            musicConductor.crossfade(0f, musicConductor.deathTrack, 3f, 0f, 0f);
            yield return new WaitForSeconds(2f);
            isLoading = false;
            yield return new WaitForSeconds(1f);
            fastSequencesDEV = false;
            pylon1.GetComponent<Collider>().enabled = true;
            pylon2.GetComponent<Collider>().enabled = true;
            pylon3.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(7f);
            objectivePrompt.hidePrompt();
            //yield return new WaitForSeconds(10f);
            //fastSequencesDEV = false;
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



        if (!pylon1Checked && pylon1.chargeDone)
        {
            //if pylon2 minimap icon disabled
            //splitter minimap icon enabled
            //pylon2 minimap icon enabled
            //pylon3 minimap icon enabled
            splitterMinimapIcon.transform.localScale = new Vector3(560f, 560f, 560f);
            pylon2MinimapIcon.transform.localScale = new Vector3(560f, 560f, 560f);
            pylon3MinimapIcon.transform.localScale = new Vector3(560f, 560f, 560f);
            pylonsCompleted++;
            pylon1Checked = true;
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



        if (!pylon3Checked && pylon3.chargeDone)
        {
            pylonsCompleted++;
            pylon3Checked = true;
        }

        //Pylon 3 Complete Trigger
        if (pylon3CompleteTrigger.sequenceState == sequenceState.READY)
        {
            if (pylon3CompleteTrigger.triggered && pylon3.chargeDone)
            {
                pylon3CompleteTrigger.sequenceState = sequenceState.RUNNING;
                if (pylonsCompleted == 2 && pylon2CompleteState == sequenceState.READY)
                {
                    StartCoroutine(pylon2CompleteSequence());
                }
                else if (pylonsCompleted == 3 && pylon3CompleteState == sequenceState.READY)
                {
                    StartCoroutine(pylon3CompleteSequence());
                }
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



        /*
        //Pylon 2 Complete Trigger
        if (pylon2.isCharging)
        {
            pylon2CompleteState = sequenceState.READY;
        }

        if (pylon2CompleteState == sequenceState.READY)
        {
            if (pylon2.chargeDone)
            {
                StartCoroutine(pylon2CompleteSequence());
            }
        }
        */



        if (!pylon2Checked && pylon2.chargeDone)
        {
            pylonsCompleted++;
            pylon2Checked = true;
        }

        //Pylon 2 Complete Trigger
        if (pylon2CompleteTrigger.sequenceState == sequenceState.READY)
        {
            if (pylon2CompleteTrigger.triggered && pylon2.chargeDone)
            {
                pylon2CompleteTrigger.sequenceState = sequenceState.RUNNING;
                if (pylonsCompleted == 2 && pylon2CompleteState == sequenceState.READY)
                {
                    StartCoroutine(pylon2CompleteSequence());
                }
                else if (pylonsCompleted == 3 && pylon3CompleteState == sequenceState.READY)
                {
                    StartCoroutine(pylon3CompleteSequence());
                }
                //StartCoroutine(pylon2CompleteSequence());
            }
        }



        //Barry Trigger
        if (barryTrigger.sequenceState == sequenceState.READY)
        {
            if (barryTrigger.triggered)
            {
                barryTrigger.sequenceState = sequenceState.RUNNING;
                StartCoroutine(barrySequence());
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
        miniMap.sonarEnabled = false;
        miniMap.enemiesEnabled = false;
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
        miniMap.sonarEnabled = true;
        miniMap.enemiesEnabled = true;
        pylon1CompleteState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        resumeEnemySpawning();
        yield return new WaitForSeconds(9.25f);
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
        miniMap.sonarEnabled = false;
        miniMap.enemiesEnabled = false;
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
        miniMap.sonarEnabled = true;
        miniMap.enemiesEnabled = true;
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
        //pylon3CompleteTrigger.sequenceState = sequenceState.RUNNING;
        pylon3CompleteState = sequenceState.RUNNING;
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
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon3CompleteMessage2, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            yield return messanger.showMessage(pylon3CompleteMessage3, aspSender, false);
            yield return new WaitForSeconds(0.75f);
        }
        objectivePrompt.showPrompt(pylon3CompleteObjective);
        area1FinalWall.enabled = false;
        playerMovement.rooted = false;
        playerAttackHandler.enabled = true;
        miniMap.sonarEnabled = true;
        miniMap.enemiesEnabled = false;
        if (pylon2CompleteTrigger.sequenceState == sequenceState.RUNNING) pylon2CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        if (pylon3CompleteTrigger.sequenceState == sequenceState.RUNNING) pylon3CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        pylon3CompleteState = sequenceState.COMPLETE;
        //pylon3CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        //resumeEnemySpawning();
        yield return new WaitForSeconds(5.25f);
        musicConductor.crossfade(10f, musicConductor.hummingTrack, 5f, 0f, 0f);
        yield return new WaitForSeconds(4f);
        objectivePrompt.hidePrompt();
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
            yield return new WaitForSeconds(7.5f);
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



    //PYLON 2 COMPLETE SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylon2CompleteSequence()
    {
        //pylon2CompleteTrigger.sequenceState = sequenceState.RUNNING;
        pylon2CompleteState = sequenceState.RUNNING;
        yield return new WaitForSeconds(2.5f);
        pauseEnemySpawning();
        if (!fastSequencesDEV)
        {
            yield return messanger.showMessage("", aspSender, false);
            yield return new WaitForSeconds(0.5f);
            yield return messanger.showMessage(pylon2CompleteMessage1, aspSender, false);
            yield return new WaitForSeconds(1.25f);
            if (!splitter.isUsed)
            {
                yield return messanger.showMessage(pylon2CompleteMessage2B1, aspSender, false);
                yield return new WaitForSeconds(1.25f);
                yield return messanger.showMessage(pylon2CompleteMessage2B2, aspSender, false);
                yield return new WaitForSeconds(0.75f);
            }
            else
            {
                yield return messanger.showMessage(pylon2CompleteMessage2A, aspSender, false);
                yield return new WaitForSeconds(0.75f);
            }
        }
        if (pylon2CompleteTrigger.sequenceState == sequenceState.RUNNING) pylon2CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        if (pylon3CompleteTrigger.sequenceState == sequenceState.RUNNING) pylon3CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        pylon2CompleteState = sequenceState.COMPLETE;
        //pylon2CompleteTrigger.sequenceState = sequenceState.COMPLETE;
        yield return new WaitForSeconds(0.75f);
        messanger.hideMessage();
        resumeEnemySpawning();
    }



    //BARRY SEQUENCE --------------------------------------------------------------------
    public IEnumerator barrySequence()
    {
        barryTrigger.sequenceState = sequenceState.RUNNING;
        barryEnemySpawner.spawnEnemyWave(barryEnemyList1);
        yield return new WaitForSeconds(20f);
        barryEnemySpawner.spawnEnemyWave(barryEnemyList2);
        yield return new WaitForSeconds(40f);
        barryTrigger.triggered = false;
        barryTrigger.sequenceState = sequenceState.READY;
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

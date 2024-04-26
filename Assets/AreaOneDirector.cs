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



    //BARACUDA FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------

    [SerializeField] public SequenceTrigger baracudaFirstEncounterTrigger;

    [SerializeField] public ControlledEnemySpawner baracudaFirstEncounterEnemySpawner;

    //private string firstEncounterObjective = "Search for Light";

    [SerializeField] public List<enemyType> baracudaFirstEncounterEnemyList1;
    [SerializeField] public List<enemyType> baracudaFirstEncounterEnemyList2;

    private List<GameObject> baracudaFirstEncounterEnemies;



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





    // Start is called before the first frame update
    void Start()
    {
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.READY;
    }

    // Update is called once per frame
    void Update()
    {


        //First Encounter Trigger
        if (baracudaFirstEncounterTrigger.sequenceState == sequenceState.READY)
        {
            if (baracudaFirstEncounterTrigger.triggered)
            {
                StartCoroutine(baracudaFirstEncounterSequence());
            }
        }



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


    }



    //BARACUDA FIRST ENCOUNTER SEQUENCE --------------------------------------------------------------------
    public IEnumerator baracudaFirstEncounterSequence()
    {
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        baracudaFirstEncounterEnemies = baracudaFirstEncounterEnemySpawner.spawnEnemyWave(baracudaFirstEncounterEnemyList1);
        yield return new WaitForSeconds(20f);
        baracudaFirstEncounterEnemies = baracudaFirstEncounterEnemySpawner.spawnEnemyWave(baracudaFirstEncounterEnemyList2);
        //yield return new WaitForSeconds(20f);
        //baracudaFirstEncounterEnemies = baracudaFirstEncounterEnemySpawner.spawnEnemyWave(baracudaFirstEncounterEnemyList2);
        yield return new WaitForSeconds(40f);
        baracudaFirstEncounterTrigger.triggered = false;
        baracudaFirstEncounterTrigger.sequenceState = sequenceState.READY;
        //room2AttackState = sequenceState.READY;
    }



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
            if (enemy != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
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
            if (enemy != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
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
        pylon2Enemies = pylon2EnemySpawner.spawnEnemyWave(pylon2EnemyList4);
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
            if (enemy != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        pylon3State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }


}

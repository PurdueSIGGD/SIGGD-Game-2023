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




    //PYLON 3 SEQUENCE --------------------------------------------------------------------
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
            if (enemy != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        pylon4State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 3 SEQUENCE --------------------------------------------------------------------
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
            if (enemy != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        pylon5State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }



    //PYLON 3 SEQUENCE --------------------------------------------------------------------
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
            if (enemy != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
            }
        }
        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        pylon6State = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }


}

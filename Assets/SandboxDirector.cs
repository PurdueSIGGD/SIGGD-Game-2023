using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SandboxDirector : MonoBehaviour
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





    //PYLON SEQUENCE ---------------------------------------------------------------------------

    public sequenceState pylonState = sequenceState.READY;

    [SerializeField] public ChargePylon pylon;

    [SerializeField] public ControlledEnemySpawner pylonEnemySpawner;

    [SerializeField] public List<enemyType> pylonEnemyList1;
    [SerializeField] public List<enemyType> pylonEnemyList2;
    [SerializeField] public List<enemyType> pylonEnemyList3;
    [SerializeField] public List<enemyType> pylonEnemyList4;
    [SerializeField] public List<enemyType> pylonEnemyList5;

    private List<GameObject> pylonEnemies;




    //DEV ENEMY SPAWNER SEQUENCE -----------------------------------------------------------------

    public sequenceState DEVEnemySpawnerState = sequenceState.READY;

    [SerializeField] public ControlledEnemySpawner DEVEnemySpawner;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pylon Trigger
        if (pylonState == sequenceState.READY)
        {
            if (pylon.isUsed)
            {
                pylonState = sequenceState.RUNNING;
                StartCoroutine(pylonSequence());
            }
        }

        //DEV Enemy Spawner Trigger
        if (Input.GetKeyDown(KeyCode.K) && pylonState == sequenceState.READY)
        {
            StartCoroutine(DEVEnemySpawnerSequence());
        }
    }





    //PYLON SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylonSequence()
    {
        pylonState = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        float constantSpawnInterval = enemySpawner.constantSpawnInterval;
        float waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1);
        yield return new WaitForSeconds(8f);
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1);
        yield return new WaitForSeconds(8f);
        pylonEnemySpawner.passiveSpawnActive = true;
        pylonEnemySpawner.passiveWaveSpawnActive = true;
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3);
        yield return new WaitForSeconds(14f);

        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2);
        yield return new WaitForSeconds(8f);
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3);
        yield return new WaitForSeconds(8f);
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4);
        yield return new WaitForSeconds(14f);

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4);
        yield return new WaitForSeconds(10f);
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2);
        yield return new WaitForSeconds(10f);
        pylonEnemySpawner.passiveSpawnActive = false;
        pylonEnemySpawner.passiveWaveSpawnActive = false;
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList5);
        yield return new WaitForSeconds(10f);

        foreach (GameObject enemy in pylonEnemies)
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
        pylonState = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;
    }




    //DEV ENEMY SPAWNER SEQUENCE -----------------------------------------------------------------------
    public IEnumerator DEVEnemySpawnerSequence()
    {
        DEVEnemySpawnerState = sequenceState.RUNNING;
        DEVEnemySpawner.spawnWaveDEV = true;
        yield return new WaitForSeconds(0.5f);
        DEVEnemySpawnerState = sequenceState.READY;
    }

}

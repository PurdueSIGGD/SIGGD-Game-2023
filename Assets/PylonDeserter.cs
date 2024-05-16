using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonDeserter : MonoBehaviour
{

    [SerializeField] public int deserterTime;

    [SerializeField] public ChargePylon pylon;

    public bool isDeserting = false;
    public IEnumerator deserterCountdownCoroutine;
    public IEnumerator pylonEnemyCoroutine;


    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
    [SerializeField] public EnemySpawner enemySpawner;
    public float constantSpawnInterval;
    public float waveSpawnInterval;

    public IEnumerator pylonCoroutine;
    public sequenceState pylonState = sequenceState.READY;

    //[SerializeField] public ChargePylon pylon;

    [SerializeField] public ControlledEnemySpawner pylonEnemySpawner;

    [SerializeField] public List<enemyType> pylonEnemyList1;
    [SerializeField] public List<enemyType> pylonEnemyList2;
    [SerializeField] public List<enemyType> pylonEnemyList3;
    [SerializeField] public List<enemyType> pylonEnemyList4;
    [SerializeField] public List<enemyType> pylonEnemyList5;

    public List<GameObject> pylonEnemies = new List<GameObject>();
    //------------------------------------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pylon Enemy Sequence
        if (pylonState == sequenceState.READY)
        {
            if (pylon.isUsed)
            {
                pylonState = sequenceState.RUNNING;
                //pylonDeserter.pylonEnemyCoroutine = pylonSequence();
                pylonCoroutine = pylonSequence();
                StartCoroutine(pylonCoroutine);
                //StartCoroutine(pylonDeserter.pylonEnemyCoroutine);
                //StartCoroutine(pylonSequence());
            }
        }
    }


    //Start Deserting
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && pylon.isCharging)
        {
            deserterCountdownCoroutine = deserterCountdownSequence();
            StartCoroutine(deserterCountdownCoroutine);
        }
    }


    //Stop Deserting
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && pylon.isCharging && isDeserting)
        {
            StopCoroutine(deserterCountdownCoroutine);
            pylon.interactPrompt.hidePrompt();
            isDeserting = false;
        }
    }


    //Deserter Countdown
    public IEnumerator deserterCountdownSequence()
    {
        isDeserting = true;
        for (int i = deserterTime; i > 0; i--)
        {
            pylon.interactPrompt.showPrompt("Return to the Pylon | " + i);
            yield return new WaitForSeconds(1f);
        }
        pylon.interactPrompt.hidePrompt();
        isDeserting = false;
        //Kill Pylon Enemy Spawner
        //StopCoroutine(pylonEnemyCoroutine);
        Debug.Log("COROUTINE: " + pylonCoroutine.ToString());
        StopCoroutine(pylonCoroutine);
        //pylon.pylonCoroutine = null;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylonState = sequenceState.READY;
        pylon.isCharging = false;
        pylon.isUsed = false;
        pylon.objectivePrompt.hidePrompt();
        pylon.interactPrompt.showPrompt("Pylon Activation Failed");
        yield return new WaitForSeconds(2.5f);
        pylon.interactPrompt.hidePrompt();
    }





    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylonSequence()
    {
        Debug.Log("INSIDE THIS COROUTINE 1: " + pylonCoroutine.ToString());

        pylonState = sequenceState.RUNNING;
        //enemySpawner.enabled = false;
        constantSpawnInterval = enemySpawner.constantSpawnInterval;
        waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;
        //baracudaFirstEncounterTrigger.sequenceState = sequenceState.RUNNING;
        //pylon1State = sequenceState.RUNNING;
        pylonEnemies = pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1);
        yield return new WaitForSeconds(8f);
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1));
        yield return new WaitForSeconds(8f);
        pylonEnemySpawner.passiveSpawnActive = true;
        pylonEnemySpawner.passiveWaveSpawnActive = true;
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3));
        yield return new WaitForSeconds(14f);

        Debug.Log("INSIDE THIS COROUTINE 2: " + pylonCoroutine.ToString());

        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2));
        yield return new WaitForSeconds(8f);
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3));
        yield return new WaitForSeconds(8f);
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4));
        yield return new WaitForSeconds(14f);

        Debug.Log("INSIDE THIS COROUTINE 3: " + pylonCoroutine.ToString());

        //pylon1EnemySpawner.passiveSpawnActive = false;
        //pylon1EnemySpawner.passiveWaveSpawnActive = false;
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4));
        yield return new WaitForSeconds(10f);
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2));
        yield return new WaitForSeconds(10f);
        pylonEnemySpawner.passiveSpawnActive = false;
        pylonEnemySpawner.passiveWaveSpawnActive = false;
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList5));
        yield return new WaitForSeconds(10f);

        Debug.Log("INSIDE THIS COROUTINE 4: " + pylonCoroutine.ToString());

        string enemiesList = "ENEMIES: ";
        foreach (GameObject enemy in pylonEnemies)
        {
            if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
            {
                enemy.GetComponent<HealthPoints>().damageEntity(1000f);
                enemiesList += enemy.name + " | ";
            }
        }
        Debug.Log(enemiesList);

        //enemySpawner.enabled = true;
        enemySpawner.constantSpawnTimer = 0f;
        enemySpawner.waveSpawnTimer = 0f;
        enemySpawner.constantSpawnInterval = constantSpawnInterval;
        enemySpawner.waveSpawnInterval = waveSpawnInterval;
        pylonState = sequenceState.COMPLETE;
        //room2AttackState = sequenceState.READY;

        Debug.Log("INSIDE THIS COROUTINE 5: " + pylonCoroutine.ToString());
    }


}

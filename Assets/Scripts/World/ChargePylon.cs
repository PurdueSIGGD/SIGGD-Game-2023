using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargePylon : Interactable
{

    [SerializeField] private float chargeTime = 90f;
    [SerializeField] public PlayerLevel playerLevel;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Light activatedLight;
    [SerializeField] public Light orbLight;
    //[SerializeField] public PylonDeserter pylonDeserter;

    public float currentCharge;

    public float tickRate;
    public float previousTickTime;

    public bool isCharging;
    public bool chargeDone;


    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
    /*[SerializeField] public EnemySpawner enemySpawner;
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

    public List<GameObject> pylonEnemies = new List<GameObject>();*/
    //------------------------------------------------------------------------------------------



    // Start is called before the first frame update
    public override void Start()
    {
        currentCharge = 0f;
        tickRate = 0.05f;
        //previousTickTime = Time.time;
        isCharging = false;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {

        //Pylon Enemy Sequence
        /*if (pylonState == sequenceState.READY)
        {
            if (isUsed)
            {
                pylonState = sequenceState.RUNNING;
                //pylonDeserter.pylonEnemyCoroutine = pylonSequence();
                pylonCoroutine = pylonSequence();
                StartCoroutine(pylonCoroutine);
                //StartCoroutine(pylonDeserter.pylonEnemyCoroutine);
                //StartCoroutine(pylonSequence());
            }
        }*/

        //Pylon Charging
        if (isUsed && isCharging && (Time.time - previousTickTime >= tickRate))
        {
            currentCharge += ((Time.time - previousTickTime) /*tickRate*/ / chargeTime) * 100f;
            objectivePrompt.showPrompt("Pylon Charging...   " + Mathf.FloorToInt(currentCharge) + "%");
            if (activatedLight != null)
            {
                activatedLight.intensity = currentCharge * 2f;
            }
            if (orbLight != null)
            {
                orbLight.intensity = currentCharge * 0.1f;
            }
            previousTickTime = Time.time;
        }


        //Pylon Completed
        if (currentCharge >= 100f && isCharging)
        {
            StartCoroutine(activationFlare());

            markPylonDone();
            
            // Mark the objective as completed and save the game once automatically
            SavePylon();
        }

        base.Update();
    }

    private void SavePylon()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(transform.position + Vector3.right * 10f);
        saveManager.MarkObjective(gameObject, SaveManager.ObjectiveType.Pylon);
    }

    public void markPylonDone()
    {
        isCharging = false;
        playerLevel.levelUp();
        objectivePrompt.hidePrompt();
        chargeDone = true;
        isUsed = false;
        promptMessage = "E | Save Game";
        Debug.Log("Done");
    }

    public override void interact()
    {
        if (!chargeDone)
        {
            isCharging = true;
            previousTickTime = Time.time;
        }
        else
        {
            // Save the game
            isUsed = false;
            SavePylon();
        }

        base.interact();
    }



    public IEnumerator activationFlare()
    {
        if (orbLight != null)
        {
            orbLight.intensity = 10f;
        }
        if (activatedLight != null)
        {
            float lightRange = activatedLight.range;
            activatedLight.range = 1000f;
            activatedLight.intensity = 200f;
            for (int i = 0; i < 100; i++)
            {
                activatedLight.intensity += 48;
                yield return new WaitForSeconds(0.0005f);
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 100; i++)
            {
                activatedLight.intensity -= 45;
                yield return new WaitForSeconds(0.015f);
            }
            activatedLight.range = lightRange;
            activatedLight.intensity = 500f;
        }
    }


    
    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
    /*public IEnumerator pylonSequence()
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
    }*/


}

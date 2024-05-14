using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPylon : ChargePylon
{
    [SerializeField] public EnemySpawner enemySpawner;


    public sequenceState pylonState = sequenceState.READY;

    //[SerializeField] public ChargePylon pylon;

    [SerializeField] public ControlledEnemySpawner pylonEnemySpawner;

    [SerializeField] public List<enemyType> pylonEnemyList1;
    [SerializeField] public List<enemyType> pylonEnemyList2;
    [SerializeField] public List<enemyType> pylonEnemyList3;
    [SerializeField] public List<enemyType> pylonEnemyList4;
    [SerializeField] public List<enemyType> pylonEnemyList5;

    private List<GameObject> pylonEnemies = new List<GameObject>();





    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {


        if (pylonState == sequenceState.READY)
        {
            if (isUsed)
            {
                //pylonState = sequenceState.RUNNING;
                StartCoroutine(pylonSequence());
            }
        }


        base.Update();
    }



    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
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
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1));
        yield return new WaitForSeconds(8f);
        pylonEnemySpawner.passiveSpawnActive = true;
        pylonEnemySpawner.passiveWaveSpawnActive = true;
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3));
        yield return new WaitForSeconds(14f);

        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2));
        yield return new WaitForSeconds(8f);
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3));
        yield return new WaitForSeconds(8f);
        pylonEnemies.AddRange(pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4));
        yield return new WaitForSeconds(14f);

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
    }


}

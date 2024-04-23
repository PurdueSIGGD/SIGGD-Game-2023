using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledEnemySpawner : MonoBehaviour
{

    [SerializeField] public GameObject piranha;
    [SerializeField] public GameObject barracuda;
    [SerializeField] public GameObject jelly;
    [SerializeField] public GameObject siren;
    [SerializeField] public GameObject leviathan;

    [SerializeField] public List<GameObject> spawnPoints;

    [SerializeField] public bool passiveSpawnActive;
    [SerializeField] public float passiveSpawnCooldown;
    [SerializeField] public int passiveSpawnBarracudaChance;

    [SerializeField] public bool passiveWaveSpawnActive;
    [SerializeField] public int passiveWaveSpawnCount;
    [SerializeField] public float passiveWaveSpawnCooldown;
    [SerializeField] public int passiveWaveSpawnBarracudaChance;

    public bool canPassiveSpawn;
    public bool canPassiveWaveSpawn;

    [SerializeField] public bool spawnWaveDEV;
    [SerializeField] public List<enemyType> waveEnemyTypesDEV;

    // Start is called before the first frame update
    void Start()
    {
        canPassiveSpawn = false;
        canPassiveWaveSpawn = false;
        StartCoroutine(passiveSpawnCooldownTimer());
        StartCoroutine(passiveWaveSpawnCooldownTimer());

        spawnWaveDEV = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnWaveDEV)
        {
            spawnWaveDEV = false;
            spawnEnemyWave(waveEnemyTypesDEV);
        }

        if (canPassiveSpawn && passiveSpawnActive)
        {
            spawnEnemyPassive();
        }

        if (canPassiveWaveSpawn && passiveWaveSpawnActive)
        {
            StartCoroutine(spawnEnemyWavePassive());
        }

    }

    public void spawnEnemyPassive()
    {
        Debug.Log("spawnEnemyPassive run");
        canPassiveSpawn = false;
        if (Random.Range(0, 100) < passiveSpawnBarracudaChance)
        {
            spawnEnemyRandom(enemyType.BARRACUDA);
        }
        else
        {
            spawnEnemyRandom(enemyType.PIRANHA);
        }
        StartCoroutine(passiveSpawnCooldownTimer());
    }

    public IEnumerator spawnEnemyWavePassive()
    {
        Debug.Log("spawnEnemyWavePassive run");
        canPassiveWaveSpawn = false;
        enemyType spawnType;
        if (Random.Range(0, 100) < passiveWaveSpawnBarracudaChance)
        {
            spawnType = enemyType.BARRACUDA;
        }
        else
        {
            spawnType = enemyType.PIRANHA;
        }
        for (int i = 0; i < passiveWaveSpawnCount; i++)
        {
            //int baracudaSpawn = Random.Range(0, 100);
            spawnEnemyRandom(spawnType);
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(passiveWaveSpawnCooldownTimer());
    }


    public List<GameObject> spawnEnemyWave(List<enemyType> enemyTypeList)
    {
        List<GameObject> enemyList = new List<GameObject>();
        //foreach (enemyType enemyType in enemyTypeList)
        //{
            //enemyList.Add(spawnEnemyRandom(enemyType));
        StartCoroutine(spawnEnemyWaveCo(enemyTypeList, enemyList));
        //}
        return enemyList;
    }


    public IEnumerator spawnEnemyWaveCo(List<enemyType> enemyTypeList, List<GameObject> spawnedEnemies)
    {
        foreach (enemyType enemyType in enemyTypeList)
        {
            spawnedEnemies.Add(spawnEnemyRandom(enemyType));
            yield return new WaitForSeconds(0.5f);
            //StartCoroutine(spawnEnemyWaveDelay());
        }
        //yield return new WaitForSeconds(0.5f);
    }

    public GameObject spawnEnemyRandom(enemyType enemyType)
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        return spawnEnemy(enemyType, spawnPointIndex);
    }

    public GameObject spawnEnemy(enemyType enemyType, int spawnPointIndex)
    {
        GameObject spawnPoint = spawnPoints[spawnPointIndex];
        GameObject enemy;

        switch (enemyType)
        {
            case enemyType.PIRANHA:
                enemy = Instantiate(piranha, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.BARRACUDA:
                enemy = Instantiate(barracuda, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.JELLY:
                enemy = Instantiate(jelly, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.SIREN:
                enemy = Instantiate(siren, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.LEVIATHAN:
                enemy = Instantiate(leviathan, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            default:
                enemy = null;
                break;
        }

        return enemy;
    }

    private IEnumerator passiveSpawnCooldownTimer()
    {
        Debug.Log("passiveSpawnCooldownTimer run");
        canPassiveSpawn = false;
        yield return new WaitForSeconds(passiveSpawnCooldown);
        canPassiveSpawn = true;
        Debug.Log("passiveSpawnCooldownTimer complete");
    }

    private IEnumerator passiveWaveSpawnCooldownTimer()
    {
        Debug.Log("passiveWaveSpawnCooldownTimer run");
        canPassiveWaveSpawn = false;
        yield return new WaitForSeconds(passiveWaveSpawnCooldown);
        canPassiveWaveSpawn = true;
        Debug.Log("passiveWaveSpawnCooldownTimer complete");
    }



    public static bool isWaveDead(List<GameObject> enemies)
    {
        string debug = "";
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                debug += "null | ";
            }
            else
            {
                debug += enemy.name + " | ";
            }
        }

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }
}

public enum enemyType
{
    PIRANHA,
    BARRACUDA,
    JELLY,
    SIREN,
    LEVIATHAN
}

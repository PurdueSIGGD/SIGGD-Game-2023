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
            StartCoroutine(spawnEnemyWave(waveEnemyTypesDEV));
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
        for (int i = 0; i < passiveWaveSpawnCount; i++)
        {
            //int baracudaSpawn = Random.Range(0, 100);
            if (Random.Range(0, 100) < passiveWaveSpawnBarracudaChance)
            {
                spawnEnemyRandom(enemyType.BARRACUDA);
            }
            else
            {
                spawnEnemyRandom(enemyType.PIRANHA);
            }
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(passiveWaveSpawnCooldownTimer());
    }



    public IEnumerator spawnEnemyWave(List<enemyType> enemyTypeList)
    {
        foreach (enemyType enemyType in enemyTypeList)
        {
            spawnEnemyRandom(enemyType);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void spawnEnemyRandom(enemyType enemyType)
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        spawnEnemy(enemyType, spawnPointIndex);
    }

    public void spawnEnemy(enemyType enemyType, int spawnPointIndex)
    {
        GameObject spawnPoint = spawnPoints[spawnPointIndex];

        switch (enemyType)
        {
            case enemyType.PIRANHA:
                Instantiate(piranha, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.BARRACUDA:
                Instantiate(barracuda, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.JELLY:
                Instantiate(jelly, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.SIREN:
                Instantiate(siren, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;

            case enemyType.LEVIATHAN:
                Instantiate(leviathan, spawnPoint.transform.position, spawnPoint.transform.rotation);
                break;
        }
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
}

public enum enemyType
{
    PIRANHA,
    BARRACUDA,
    JELLY,
    SIREN,
    LEVIATHAN
}

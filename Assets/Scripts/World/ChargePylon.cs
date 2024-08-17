using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargePylon : Interactable
{

    [SerializeField] private float chargeTime = 105f;
    [SerializeField] private float wave1PassiveEnemySpawnCooldown = 5f;
    [SerializeField] private float wave2PassiveEnemySpawnCooldown = 5f;
    [SerializeField] private float wave3PassiveEnemySpawnCooldown = 5f;


    [SerializeField] public PlayerLevel playerLevel;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Light activatedLight;
    [SerializeField] public Light orbLight;
    [SerializeField] public PylonDeserter pylonDeserter;
    [SerializeField] public SpriteRenderer rangeRing;

    [SerializeField] public AudioSource pylonStartSFX;
    [SerializeField] public AudioSource pylonHumSFX;
    [SerializeField] public AudioSource pylonEndSFX;
    [SerializeField] public AudioSource pylonMusic;
    [SerializeField] public AudioSourceController pylonMusicController;

    public float currentCharge;

    public float tickRate;
    public float previousTickTime;

    public bool isCharging;
    public bool chargeDone;


    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
    [SerializeField] public EnemySpawner enemySpawner;
    public float constantSpawnInterval;
    public float waveSpawnInterval;

    public IEnumerator pylonCoroutine;
    public sequenceState pylonState = sequenceState.READY;

    [SerializeField] public ControlledEnemySpawner pylonEnemySpawner;

    [SerializeField] public List<enemyType> pylonEnemyList1;
    [SerializeField] public List<enemyType> pylonEnemyList2;
    [SerializeField] public List<enemyType> pylonEnemyList3;
    [SerializeField] public List<enemyType> pylonEnemyList4;
    [SerializeField] public List<enemyType> pylonEnemyList5;

    public List<List<GameObject>> pylonEnemies = new List<List<GameObject>>();
    //------------------------------------------------------------------------------------------


    [SerializeField] public MusicConductor musicConductor;
    [SerializeField] public MusicTrack currentAmbientTrack;



    // Start is called before the first frame update
    public override void Start()
    {
        currentCharge = 0f;
        tickRate = 0.05f;
        isCharging = false;
        Color ringColor = rangeRing.color;
        ringColor.a = 0f;
        rangeRing.color = ringColor;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {

        //Pylon Enemy Sequence
        if (pylonState == sequenceState.READY && !chargeDone)
        {
            if (isUsed)
            {
                pylonState = sequenceState.RUNNING;
                pylonCoroutine = pylonSequence();
                StartCoroutine(pylonCoroutine);
                StartCoroutine(rangeRingFade(true));
            }
        }

        //Pylon Charging
        if (isUsed && isCharging && (Time.time - previousTickTime >= tickRate))
        {
            currentCharge += ((Time.time - previousTickTime) /*tickRate*/ / chargeTime) * 100f;
            objectivePrompt.showPrompt("Pylon Charging...   " + Mathf.FloorToInt(currentCharge) + "%");

            //Activated Light Fader
            if (activatedLight != null)
            {
                activatedLight.intensity = currentCharge * 2f;
            }
            if (orbLight != null)
            {
                orbLight.intensity = currentCharge * 0.1f;
            }

            //Hum Pitch/Volume Fader
            if (currentCharge >= 70f)
            {
                pylonHumSFX.pitch = ((currentCharge - 70f) / 30f) + 1f;
                //pylonHumSFX.volume = (((currentCharge - 70f) / 30f) * 0.6f) + 0.4f;
            }

            previousTickTime = Time.time;
        }


        //Pylon Completed
        if (currentCharge >= 100f && isCharging)
        {
            pylonDeserter.killDeserterCountdownSequence();
            StartCoroutine(activationFlare());
            StartCoroutine(rangeRingFade(false));

            markPylonDone();
            
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

    public void SavePylonCheckpoint()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(transform.position + Vector3.right * 10f);
        saveManager.SaveGame();
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
            SavePylonCheckpoint();
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



    public IEnumerator rangeRingFade(bool fadeIn)
    {
        Color ringColor = rangeRing.color;
        ringColor.a = (fadeIn) ? 0f : 0.2f;
        rangeRing.color = ringColor;
        for (int i = 0; i < 100; i++)
        {
            ringColor.a += (fadeIn) ? 0.002f : -0.002f;
            rangeRing.color = ringColor;
            yield return new WaitForSeconds(0.01f);
        }
        ringColor.a = (fadeIn) ? 0.2f : 0f;
        rangeRing.color = ringColor;
    }




    
    //PYLON CHARGE SEQUENCE --------------------------------------------------------------------
    public IEnumerator pylonSequence()
    {
        List<GameObject> emptyList = new List<GameObject>();

        pylonState = sequenceState.RUNNING;
        constantSpawnInterval = enemySpawner.constantSpawnInterval;
        waveSpawnInterval = enemySpawner.waveSpawnInterval;
        enemySpawner.constantSpawnInterval = 99999f;
        enemySpawner.waveSpawnInterval = 99999f;

        //pylonMusic.Stop();
        //pylonMusicController.audioState = true;
        //pylonMusic.Play();
        musicConductor.crossfade(1f, musicConductor.deapOceanBassTrack, 0f, 0f, 0f);////////////////////////////////////////////////////////////////////////////////////////////
        pylonStartSFX.Play();
        pylonHumSFX.PlayDelayed(6f);
        pylonHumSFX.pitch = 1f;
        //pylonHumSFX.volume = 0.4f;

        //Wave 1
        pylonEnemySpawner.passiveSpawnCooldown = wave1PassiveEnemySpawnCooldown;
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 8f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList1));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 8f);
        pylonEnemySpawner.passiveSpawnActive = (pylonCoroutine == null) ? false : true;
        pylonEnemySpawner.passiveWaveSpawnActive = (pylonCoroutine == null) ? false : true;
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 14f);

        //Wave 2
        pylonEnemySpawner.passiveSpawnCooldown = wave2PassiveEnemySpawnCooldown;
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 8f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList3));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 8f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 14f);

        //Wave 3
        pylonEnemySpawner.passiveSpawnCooldown = wave3PassiveEnemySpawnCooldown;
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 10f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 10f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList4));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 10f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList2));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 5f);
        pylonEnemies.Add((pylonCoroutine == null) ? emptyList : pylonEnemySpawner.spawnEnemyWave(pylonEnemyList5));
        yield return new WaitForSeconds((pylonCoroutine == null) ? 0.1f : 7f);
        pylonEnemySpawner.passiveSpawnActive = false;
        pylonEnemySpawner.passiveWaveSpawnActive = false;

        if (pylonCoroutine != null)
        {
            pylonEndSFX.Play();
            yield return new WaitForSeconds(3.5f);
            pylonHumSFX.Stop();
            //pylonMusicController.audioState = false;
            //pylonMusic.Stop();
            musicConductor.crossfade(0f, currentAmbientTrack, 10f, 0f, currentAmbientTrack.loopStartTime);/////////////////////////////////////////////////////////////////////////

            pylonEnemies = null;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
                {
                    enemy.GetComponent<HealthPoints>().damageEntity(1000f);
                }
            }

            enemySpawner.constantSpawnTimer = 0f;
            enemySpawner.waveSpawnTimer = 0f;
            enemySpawner.constantSpawnInterval = constantSpawnInterval;
            enemySpawner.waveSpawnInterval = waveSpawnInterval;
            pylonState = sequenceState.COMPLETE;

            yield return new WaitForSeconds(1f);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (enemy != null && enemy.GetComponent<HealthPoints>() != null)
                {
                    enemy.GetComponent<HealthPoints>().damageEntity(1000f);
                }
            }
        }

        Debug.Log("IT IS DONE");
    }


}

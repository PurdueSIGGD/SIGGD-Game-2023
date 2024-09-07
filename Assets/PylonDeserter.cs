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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pylon.currentCharge >= 100f && pylon.isCharging && isDeserting)
        {
            Debug.Log("STOP DESERT START");
            StopCoroutine(deserterCountdownCoroutine);
            pylon.interactPrompt.hidePrompt();
            isDeserting = false;
            Debug.Log("STOP DESERT END");
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
            killDeserterCountdownSequence();
        }
    }


    public void killDeserterCountdownSequence()
    {
        if (deserterCountdownCoroutine != null)
        {
            Debug.Log("STOP DESERT START");
            StopCoroutine(deserterCountdownCoroutine);
            pylon.interactPrompt.hidePrompt();
            isDeserting = false;
            deserterCountdownCoroutine = null;
            Debug.Log("STOP DESERT END");
        }
    }


    //Deserter Countdown
    public IEnumerator deserterCountdownSequence()
    {
        //Desertion Timer
        isDeserting = true;
        for (int i = deserterTime; i > 0; i--)
        {
            pylon.interactPrompt.showPrompt("Return to the Pylon | " + i);
            yield return new WaitForSeconds(1f);
        }

        //Deserted Pylon
        pylon.pylonHumSFX.Stop();
        //pylon.pylonMusicController.audioState = false;
        pylon.musicConductor.crossfade(1f, pylon.currentAmbientTrack, 10f, 0f, pylon.currentAmbientTrack.loopStartTime);///////////////////////////////////////////////////////
        pylon.interactPrompt.hidePrompt();
        isDeserting = false;
        pylon.pylonCoroutine = null;
        pylon.pylonEnemySpawner.passiveSpawnActive = false;
        pylon.pylonEnemySpawner.passiveWaveSpawnActive = false;
        pylon.enemySpawner.constantSpawnTimer = 0f;
        pylon.enemySpawner.waveSpawnTimer = 0f;
        pylon.enemySpawner.constantSpawnInterval = pylon.constantSpawnInterval;
        pylon.enemySpawner.waveSpawnInterval = pylon.waveSpawnInterval;
        pylon.pylonState = sequenceState.READY;
        pylon.isCharging = false;
        pylon.isUsed = false;
        pylon.currentCharge = 0f;
        if (pylon.activatedLight != null)
        {
            pylon.activatedLight.intensity = 0f;
        }
        if (pylon.orbLight != null)
        {
            pylon.orbLight.intensity = 0f;
        }
        StartCoroutine(pylon.rangeRingFade(false));
        pylon.objectivePrompt.hidePrompt();
        pylon.objectivePrompt.hideProgressBar();
        pylon.interactPrompt.showPrompt("Pylon Activation Failed");
        yield return new WaitForSeconds(2.5f);
        pylon.interactPrompt.hidePrompt();
        //yield return new WaitForSeconds(1.5f);
        //pylon.pylonMusic.Stop();
    }


}

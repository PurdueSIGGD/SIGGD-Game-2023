using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePylon : Interactable
{

    [SerializeField] private float chargeTime = 120f;
    [SerializeField] public PlayerLevel playerLevel;
    [SerializeField] public ObjectivePrompt objectivePrompt;
    [SerializeField] public Light activatedLight;
    [SerializeField] public Light orbLight;

    public float currentCharge;

    public float tickRate;
    public float previousTickTime;

    public bool isCharging;
    public bool chargeDone;

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
            /*if (activatedLight != null)
            {
                activatedLight.intensity = 500f;
            }
            if (orbLight != null)
            {
                orbLight.intensity = 10f;
            }*/
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


}

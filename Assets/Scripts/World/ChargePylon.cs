using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePylon : Interactable
{

    [SerializeField] private float chargeTime = 120f;
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] public ObjectivePrompt objectivePrompt;

    public float currentCharge;

    private float tickRate;
    private float previousTickTime;

    private bool isCharging;
    private bool chargeDone;

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

        if (isUsed && isCharging && (Time.time - previousTickTime >= tickRate))
        {
            currentCharge += ((Time.time - previousTickTime) /*tickRate*/ / chargeTime) * 100f;
            objectivePrompt.showPrompt("Pylon Charging...   " + Mathf.FloorToInt(currentCharge) + "%");
            previousTickTime = Time.time;
        }

        if (currentCharge >= 100f && isCharging)
        {
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
}

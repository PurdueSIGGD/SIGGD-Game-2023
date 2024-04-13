using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            isCharging = false;
            playerLevel.levelUp();
            objectivePrompt.hidePrompt();
        }

        base.Update();
    }


    public override void interact()
    {
        isCharging = true;
        previousTickTime = Time.time;
        base.interact();
    }
}

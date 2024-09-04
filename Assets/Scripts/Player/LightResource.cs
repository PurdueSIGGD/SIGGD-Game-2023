using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Properties;
using UnityEngine;

public class LightResource : MonoBehaviour
{
    //[SerializeField] private int level1MaximumLight;                //Level 1 - Maximum Light
    //[SerializeField] private int level2MaximumLight;                //Level 2 - Maximum Light
    //[SerializeField] private float level2LightGeneratedPerSecond;   //Level 2 - Passive Light Gain per Second
    //[SerializeField] private int level3MaximumLight;                //Level 3 - Maximum Light
    //[SerializeField] private float level3LightGeneratedPerSecond;   //Level 3 - Passive Light Gain per Second

    [SerializeField] public int maximumLight;
    public float currentLight;
    public bool blackout;
    private bool blackoutInit;


    //private int[] maxLightPerLevel = { 0, 0, 0 };
    //private float[] lightperSecondPerLevel = { 0, 0, 0 };
    
    //[SerializeField] private float currentLight;
    //[SerializeField] private int currentLevel;
    //private int currentMaxLight;
    //private float currentLightGeneratedPerSecond;

    [SerializeField] private int addLightDEV = 0;
    [SerializeField] private int consumeLightDEV = 0;
    //[SerializeField] private bool setLevelDEV = false;

    ////Tick Timer
    //private float tickRate = 0.2f;
    //private float previousTickTime;

    // Start is called before the first frame update
    void Start()
    {
        //maxLightPerLevel[0] = level1MaximumLight;
        //maxLightPerLevel[1] = level2MaximumLight;
        //maxLightPerLevel[2] = level3MaximumLight;

        //lightperSecondPerLevel[0] = 0;
        //lightperSecondPerLevel[1] = level2LightGeneratedPerSecond;
        //lightperSecondPerLevel[2] = level3LightGeneratedPerSecond;

        //currentLevel = 1;
        //currentMaxLight = maxLightPerLevel[0];
        //currentLightGeneratedPerSecond = lightperSecondPerLevel[0];
        currentLight = 0f;
        blackout = false;
        blackoutInit = false;

        //previousTickTime = Time.time;
    }



    /// <summary>
    /// Adds the specified amount of light to the player's light resource pool.
    /// This method enforces the light pool's maximum capacity.
    /// </summary>
    /// <param name="light">
    /// The amount of light to add to the light resource pool
    /// </param>
    /// <returns>
    /// The actual amount of light added to the player's light resource pool
    /// </returns>
    public float addLight(float light)
    {
        if (blackout && light < 10f)
        {
            return 0f;
        }

        //float addedLight = (currentLight + light >= currentMaxLight) ? currentMaxLight - currentLight : light;
        float addedLight = (currentLight + light >= maximumLight) ? maximumLight - currentLight : light;
        currentLight += addedLight;
        return addedLight;
    }



    /// <summary>
    /// Removes the specified amount of light from the player's light resource pool.
    /// No light is removed if there isn't enough available in the player's light resource pool.
    /// </summary>
    /// <param name="light">
    /// The amount of light to remove from the light resource pool
    /// </param>
    /// <returns>
    /// The actual amount of light removed from the player's light resource pool
    /// </returns>
    public float consumeLight(float light)
    {
        if (blackout)
        {
            return 0f;
        }

        //float consumedLight = (currentLight - light <= 0f) ? currentLight : light;
        float consumedLight = (currentLight - light < 0f) ? 0f : light;
        currentLight -= consumedLight;
        return consumedLight;
    }



    /// <summary>
    /// Levels up the player's light generator.
    /// </summary>
    /*public void levelUpGenerator()
    {
        currentMaxLight = maxLightPerLevel[currentLevel];
        currentLightGeneratedPerSecond = lightperSecondPerLevel[currentLevel];
        currentLevel++;
    }*/



    // Update is called once per frame
    void Update()
    {
        /*if (Time.time - previousTickTime >= tickRate)
        {
            addLight(currentLightGeneratedPerSecond * tickRate);
            previousTickTime = Time.time;
        }*/

        if (addLightDEV > 0)
        {
            addLight(addLightDEV);
            addLightDEV = 0;
        }

        if (consumeLightDEV > 0)
        {
            consumeLight(consumeLightDEV);
            consumeLightDEV = 0;
        }

        /*if (setLevelDEV)
        {
            levelUpGenerator();
            setLevelDEV = false;
        }*/


        if (blackout && !blackoutInit)
        {
            currentLight = 0;
            blackoutInit = true;
        }

        if (!blackout && blackoutInit)
        {
            blackoutInit = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class LightResource : MonoBehaviour
{
    [SerializeField] private int level1MaximumLight;                //Level 1 - Maximum Light
    [SerializeField] private int level2MaximumLight;                //Level 2 - Maximum Light
    [SerializeField] private float level2LightGeneratedPerSecond;   //Level 2 - Passive Light Gain per Second
    [SerializeField] private int level3MaximumLight;                //Level 3 - Maximum Light
    [SerializeField] private float level3LightGeneratedPerSecond;   //Level 3 - Passive Light Gain per Second


    private int[] maxLightPerLevel = { 0, 0, 0 };
    private float[] lightperSecondPerLevel = { 0, 0, 0 };
    
    private float currentLight;
    private int currentLevel;
    private int currentMaxLight;
    private float currentLightGeneratedPerSecond;

    //Tick Timer
    private float tickRate = 0.2f;
    private float previousTickTime;

    // Start is called before the first frame update
    void Start()
    {
        maxLightPerLevel[0] = level1MaximumLight;
        maxLightPerLevel[1] = level2MaximumLight;
        maxLightPerLevel[2] = level3MaximumLight;

        lightperSecondPerLevel[0] = 0;
        lightperSecondPerLevel[1] = level2LightGeneratedPerSecond;
        lightperSecondPerLevel[2] = level3LightGeneratedPerSecond;

        currentLevel = 1;
        currentMaxLight = maxLightPerLevel[0];
        currentLightGeneratedPerSecond = lightperSecondPerLevel[0];
        currentLight = 0f;

        previousTickTime = Time.time;
    }


    public void addLight(float light)
    {
        currentLight = (currentLight + light >= currentMaxLight) ? currentMaxLight : currentLight + light;
    }

    public void consumeLight(float light)
    {
        currentLight = (currentLight - light <= 0f) ? 0f : currentLight - light;
    }


    public void levelUpGenerator()
    {
        currentMaxLight = maxLightPerLevel[currentLevel];
        currentLightGeneratedPerSecond = lightperSecondPerLevel[currentLevel];
        currentLevel++;
    }



    // Update is called once per frame
    void Update()
    {
        if (Time.time - previousTickTime >= tickRate)
        {
            addLight(currentLightGeneratedPerSecond * tickRate);
            previousTickTime = Time.time;
        }
    }
}

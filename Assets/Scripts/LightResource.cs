using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class LightResource : MonoBehaviour
{
    [SerializeField] private int level1MaximumLight;
    [SerializeField] private int level2MaximumLight;
    [SerializeField] private int level2LightGeneratedPerTick;
    [SerializeField] private int level2TicksPerSecond;
    [SerializeField] private int level3MaximumLight;
    [SerializeField] private int level3LightGeneratedPerTick;
    [SerializeField] private int level3TicksPerSecond;

    private int[] maxLightPerLevel = { 0, 0, 0 };
    private int[] lightPerTickPerLevel = { 0, 0, 0 };
    private int[] tickRatePerLevel = { 0, 0, 0 };
    
    private float currentLight;
    private int currentLevel;
    private int currentMaxLight;
    private int currentLightGeneratedPerTick;
    private int currentGeneratorTickRate;

    // Start is called before the first frame update
    void Start()
    {
        maxLightPerLevel[0] = level1MaximumLight;
        maxLightPerLevel[1] = level2MaximumLight;
        maxLightPerLevel[2] = level3MaximumLight;

        lightPerTickPerLevel[1] = level2LightGeneratedPerTick;
        lightPerTickPerLevel[2] = level3LightGeneratedPerTick;

        tickRatePerLevel[1] = level2TicksPerSecond;
        tickRatePerLevel[2] = level3TicksPerSecond;

        currentLevel = 1;
        currentMaxLight = maxLightPerLevel[0];
        currentLight = 0f;
    }


    public void addLight(int light)
    {
        currentLight = (currentLight + light >= currentMaxLight) ? currentMaxLight : currentLight + light;
    }

    public void consumeLight(int light)
    {
        currentLight = (currentLight - light <= 0) ? 0 : currentLight - light;
    }


    public void levelUp()
    {
        currentMaxLight = maxLightPerLevel[currentLevel];
        currentLevel++;
    }



    // Update is called once per frame
    void Update()
    {
        


    }
}

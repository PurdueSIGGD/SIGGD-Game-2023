using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class LightResource : MonoBehaviour
{
    [SerializeField] public int maximumLight;
    [SerializeField] public uiBarManager uiBarManager;
    public float currentLight;
    public bool blackout;
    private bool blackoutInit;

    [SerializeField] private int addLightDEV = 0;
    [SerializeField] private int consumeLightDEV = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentLight = 0f;
        blackout = false;
        blackoutInit = false;
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
        bool isFullLight = (currentLight == maximumLight);
        if (blackout && light < 10f)
        {
            return 0f;
        }

        float addedLight = (currentLight + light >= maximumLight) ? maximumLight - currentLight : light;
        currentLight += addedLight;
        if (addedLight > 1f && !isFullLight) uiBarManager.SetLightAddedStatus(addedLight);
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

        float consumedLight = (currentLight - light < 0f) ? 0f : light;
        currentLight -= consumedLight;
        if (consumedLight > 1f) uiBarManager.SetLightConsumedStatus(consumedLight);
        return consumedLight;
    }



    // Update is called once per frame
    void Update()
    {
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

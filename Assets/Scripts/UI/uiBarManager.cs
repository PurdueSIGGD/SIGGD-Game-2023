using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class uiBarManager : MonoBehaviour
{
    [HideInInspector]public bool blackout;
    [HideInInspector] public Color unitLightColor;

    [SerializeField] public PlayerHealthPoints playerHealth;
    [SerializeField] public LightResource playerLight;
    [SerializeField] public PlayerLevel playerLevel;

    private Color unobtainableUnitLight = new Color32(217, 128, 125, 255);
    private Color nonacquirableUnitLight = new Color32(16, 91, 163, 255);
    private Color acquirableUnitLight = new Color32(20, 160, 150, 255);

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider statusSlider;
    [SerializeField] private Slider lightSlider;
    [SerializeField] private TMP_Text lightText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider unitLightSlider;
    [SerializeField] private Slider lightStatusSlider;

    [SerializeField] private Color damageStatusColor;
    [SerializeField] private Color healingStatusColor;
    [SerializeField] private Color lightConsumedColor;
    [SerializeField] private Color lightAddedColor;
    [SerializeField] private float statusFadeTime;

    // CIRCULAR SLIDERS GO FROM 0.0 TO 0.25

    void UpdateHealth() {
        healthSlider.value = 0.25f * playerHealth.currentHealth / (float) playerHealth.maximumHealth;
        healthText.SetText("" + Mathf.CeilToInt(playerHealth.currentHealth));
    }

    void UpdateLight() {
        lightSlider.value = 0.25f * playerLight.currentLight / (float) playerLight.maximumLight;
        lightText.SetText("" + Mathf.FloorToInt(playerLight.currentLight));
    }

    void UpdateLevel()
    {
        levelText.SetText("" + playerLevel.currentLevel);
    }

    void UpdateUnitLight()
    {
        
        // Update unit lights to be where it should be
        float selectedCost = GetComponent<UnitHotbarUI>().selectedCost;
        GameObject unitLight = unitLightSlider.transform.GetChild(0).gameObject;

        // If blackout happens, deactivate light bars
        if (blackout)
        {
            unitLight.SetActive(false);
            return;
        }
        else
        {
            unitLight.SetActive(true);
        }

        // If the cost is more than the maximum obtainable light
        if (selectedCost / (float) playerLight.maximumLight > 1)
        {
            unitLightSlider.value = 0.25f;
            unitLight.GetComponent<Image>().color = unobtainableUnitLight;
            unitLightColor = unobtainableUnitLight;

        // If the cost is within the maximum obtainable light
        } else
        {
            unitLightSlider.value = 0.25f * selectedCost / (float) playerLight.maximumLight;

            // If cost is not acquirable via current light
            if (playerLight.currentLight < selectedCost)
            {
                unitLight.GetComponent<Image>().color = nonacquirableUnitLight;
                unitLightColor = nonacquirableUnitLight;

            // If cost is acquirable via current light
            } else
            {
                unitLight.GetComponent<Image>().color = acquirableUnitLight;
                unitLightColor = acquirableUnitLight;
            }
        }
        
    }

    void UpdateBlackout()
    {
        if (playerHealth.blackout && !blackout)
        {
            blackout = true;
            healthText.enabled = false;
            lightText.enabled = false;
            levelText.enabled = false;
        } else if (!playerHealth.blackout && blackout)
        {
            blackout = false;
            healthText.enabled = true;
            lightText.enabled = true;
            levelText.enabled = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        blackout = false;
        Color statusColor = statusSlider.transform.GetChild(0).GetComponent<Image>().color;
        statusColor.a = 0f;
        statusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
        Color lightStatusColor = lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color;
        lightStatusColor.a = 0f;
        lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color = lightStatusColor;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateLight();
        UpdateLevel();
        UpdateBlackout();
        UpdateUnitLight();
        UpdateStatus(Time.deltaTime);
        UpdateLightStatus(Time.deltaTime);
    }

    void UpdateStatus(float deltaTime)
    {
        Color statusColor = statusSlider.transform.GetChild(0).GetComponent<Image>().color;

        if (blackout)
        {
            statusColor.a = 0f;
            statusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
            return;
        }
        if (statusColor.a <= 0) return;

        statusColor.a -= deltaTime / statusFadeTime;
        statusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
    }

    public void SetDamagedStatus(float damage)
    {
        statusSlider.value = Mathf.Min((0.25f * ((playerHealth.currentHealth + damage) / (float) playerHealth.maximumHealth)), 0.25f);
        Color statusColor = damageStatusColor;
        statusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
    }

    public void SetHealedStatus(float healing)
    {
        statusSlider.value = Mathf.Min((0.25f * ((playerHealth.currentHealth + healing) / (float)playerHealth.maximumHealth)), 0.25f);
        Color statusColor = healingStatusColor;
        statusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
    }



    void UpdateLightStatus(float deltaTime)
    {
        Color statusColor = lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color;

        if (blackout)
        {
            statusColor.a = 0f;
            lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
            return;
        }
        if (statusColor.a <= 0) return;

        statusColor.a -= deltaTime / statusFadeTime;
        lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
    }

    public void SetLightConsumedStatus(float light)
    {
        lightStatusSlider.value = Mathf.Min((0.25f * ((playerLight.currentLight + light) / (float)playerLight.maximumLight)), 0.25f);
        Color statusColor = lightConsumedColor;
        lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
    }

    public void SetLightAddedStatus(float light)
    {
        lightStatusSlider.value = Mathf.Min((0.25f * ((playerLight.currentLight + light) / (float)playerLight.maximumLight)), 0.25f);
        Color statusColor = lightAddedColor;
        lightStatusSlider.transform.GetChild(0).GetComponent<Image>().color = statusColor;
    }

}

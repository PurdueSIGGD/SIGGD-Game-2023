using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiBarManager : MonoBehaviour
{
    public Color unitLightColor;

    [SerializeField] public PlayerHealthPoints playerHealth;
    [SerializeField] public LightResource playerLight;
    [SerializeField] public PlayerLevel playerLevel;

    private Color unobtainableUnitLight = new Color32(217, 128, 125, 255);
    private Color nonacquirableUnitLight = new Color32(16, 91, 163, 255);
    private Color acquirableUnitLight = new Color32(255, 232, 90, 255);

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider lightSlider;
    [SerializeField] private TMP_Text lightText;
    [SerializeField] private Image levelFrame;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider unitLightSlider;
    [SerializeField] private Slider psudoUnitLightSlider;
    [SerializeField] private Color blackoutColor;
    //[SerializeField] private Image blackoutMask;
    
    private bool blackout;

    void UpdateHealth() {
        healthSlider.value = playerHealth.currentHealth / (float) playerHealth.maximumHealth;
        healthText.SetText("" + Mathf.CeilToInt(playerHealth.currentHealth));
    }

    void UpdateLight() {
        lightSlider.value = playerLight.currentLight / (float) playerLight.maximumLight;
        lightText.SetText("" + Mathf.CeilToInt(playerLight.currentLight));
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
        GameObject psudoUnitLight = psudoUnitLightSlider.transform.GetChild(0).gameObject;

        // If the cost is more than the maximum obtainable light
        if (selectedCost / (float) playerLight.maximumLight > 1)
        {
            unitLightSlider.value = 1;
            unitLight.GetComponent<RawImage>().color = unobtainableUnitLight;

            psudoUnitLightSlider.value = 1;
            psudoUnitLight.GetComponent<RawImage>().color = unobtainableUnitLight;

            unitLightColor = unobtainableUnitLight;

        // If the cost is within the maximum obtainable light
        } else
        {
            unitLightSlider.value = selectedCost / (float) playerLight.maximumLight;
            psudoUnitLightSlider.value = selectedCost / (float) playerLight.maximumLight;

            // If cost is not acquirable via current light
            if (playerLight.currentLight < selectedCost)
            {
                unitLight.GetComponent<RawImage>().color = nonacquirableUnitLight;
                psudoUnitLight.GetComponent<RawImage>().color = nonacquirableUnitLight;

                unitLightColor = nonacquirableUnitLight;

            // If cost is acquirable via current light
            } else
            {
                unitLight.GetComponent<RawImage>().color = acquirableUnitLight;

                unitLightColor = acquirableUnitLight;
            }
        }
    }

    void UpdateBlackout()
    {
        if (playerHealth.blackout && !blackout)
        {
            blackout = true;
            //blackoutMask.enabled = true;
            //levelFrame.color = new Color(70f, 70f, 70f, 255f);
            levelFrame.color = blackoutColor;
            healthText.enabled = false;
            lightText.enabled = false;
            levelText.enabled = false;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        blackout = false;
        //blackoutMask.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateLight();
        UpdateLevel();
        UpdateBlackout();
        UpdateUnitLight();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiBarManager : MonoBehaviour
{

    [SerializeField] public PlayerHealthPoints playerHealth;
    [SerializeField] public LightResource playerLight;
    [SerializeField] public PlayerLevel playerLevel;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider lightSlider;
    [SerializeField] private TMP_Text lightText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image blackoutMask;

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

    void UpdateBlackout()
    {
        if (playerHealth.blackout && !blackout)
        {
            blackout = true;
            blackoutMask.enabled = true;
            levelText.enabled = false;
            healthText.enabled = false;
            lightText.enabled = false;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        blackout = false;
        blackoutMask.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateLight();
        UpdateLevel();
        UpdateBlackout();
    }
}

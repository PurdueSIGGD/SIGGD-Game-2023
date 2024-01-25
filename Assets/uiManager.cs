using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiManager : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider lightSlider;
    [SerializeField] private TMP_Text lightText;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxLight;

    private float currHealth;
    private float currLight;

    void UpdateHealth() {
        healthSlider.value = (float)(currHealth / maxHealth);
        healthText.SetText("" + (int)currHealth);
    }

    void UpdateLight() {
        lightSlider.value = (float)(currLight / maxLight);
        lightText.SetText("" + (int)currLight);
    }

    void SetHealth(float newH) {
        currHealth = newH;
        UpdateHealth();
    }

    void ChangeHealthBy(float deltaH) {
        currHealth += deltaH;
        UpdateHealth();
    }

    void SetFullHealth() {
        currHealth = maxHealth;
        UpdateHealth();
    }

    void SetEmptyHealth() {
        currHealth = 0;
        UpdateHealth();
    }

    void SetLight(float newL) {
        currLight = newL;
        UpdateLight();
    }

    void ChangeLightBy(float deltaL) {
        currLight += deltaL;
        UpdateLight();
    }

    void SetFullLight() {
        currLight = maxLight;
        UpdateLight();
    }

    void SetEmptyLight() {
        currLight = 0;
        UpdateLight();
    }



    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        currLight = maxLight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

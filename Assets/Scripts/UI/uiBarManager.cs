using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiBarManager : MonoBehaviour
{

    [SerializeField] public HealthPoints playerHealth;
    [SerializeField] public LightResource playerLight;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider lightSlider;
    [SerializeField] private TMP_Text lightText;
    //[SerializeField] private float maxHealth;
    //[SerializeField] private float maxLight;

    //private float currHealth;
    //private float currLight;

    void UpdateHealth() {
        /*if (currHealth < 0) {
            currHealth = 0;
        }
        else if (currHealth > maxHealth) {
            currHealth = maxHealth;
        }*/
        healthSlider.value = playerHealth.currentHealth / (float) playerHealth.maximumHealth;
        //healthText.SetText("" + (int) currHealth);
        healthText.SetText("" + Mathf.CeilToInt(playerHealth.currentHealth));
    }

    void UpdateLight() {
        /*if (currLight < 0) {
            currLight = 0;
        }
        else if (currLight > maxLight) {
            currLight = maxLight;
        }*/
        lightSlider.value = playerLight.currentLight / (float) playerLight.maximumLight;
        //lightText.SetText("" + (int)currLight);
        lightText.SetText("" + Mathf.CeilToInt(playerLight.currentLight));
    }

    /*public void SetHealth(float newH) {
        currHealth = newH;
        UpdateHealth();
    }

    public void ChangeHealthBy(float deltaH) {
        currHealth += deltaH;
        UpdateHealth();
    }

    public void SetFullHealth() {
        currHealth = maxHealth;
        UpdateHealth();
    }

    public void SetEmptyHealth() {
        currHealth = 0;
        UpdateHealth();
    }

    public void SetLight(float newL) {
        currLight = newL;
        UpdateLight();
    }

    public void ChangeLightBy(float deltaL) {
        currLight += deltaL;
        UpdateLight();
    }

    public void SetFullLight() {
        currLight = maxLight;
        UpdateLight();
    }

    public void SetEmptyLight() {
        currLight = 0;
        UpdateLight();
    } */



    // Start is called before the first frame update
    void Start()
    {
        //currHealth = maxHealth;
        //currLight = maxLight;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateLight();
    }
}

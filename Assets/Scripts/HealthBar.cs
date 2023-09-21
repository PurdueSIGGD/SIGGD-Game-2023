using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthBar;
    public Health enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = enemyHealth.maxHealth;
        healthBar.value = enemyHealth.maxHealth;
    }

    public void SetHealth(int currHealth)
    {
        healthBar.value = currHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


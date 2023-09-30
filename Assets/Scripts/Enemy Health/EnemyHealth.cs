using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public bool isInvulnerable;
    public int maxHealth;
    [SerializeField]
    private int currHealth;

    public delegate void OnDamage(float damageAmount); // Components dependent on enemy taking damage should subscribe to this delegate
    private event OnDamage RegisterDamageEvent;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    // Method to be called when an enemy is hit. Damage is processed, and will invoke damaged event if successful
    public void ProcessDamage(float damage)
    {
        if (isInvulnerable) return;
        // Do checks for damage
        RegisterDamageEvent(damage);
    }
}
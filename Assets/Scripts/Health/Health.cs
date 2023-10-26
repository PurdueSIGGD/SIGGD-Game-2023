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

    public delegate void OnDamage(float damageAmount); // Components dependent on entity taking damage should subscribe to this delegate. To tell an entity it has been damaged, call ProcessDamage().
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

    // TODO: Tell method which entity caused the damage, implement damage types/resistances(???)
    // Method to be called when an entity is hit. Damage is processed, and will invoke damaged event if damage is successful.
    public void ProcessDamage(float damage)
    {
        if (isInvulnerable) return;
        // Do checks for damage
        RegisterDamageEvent(damage);
    }
}
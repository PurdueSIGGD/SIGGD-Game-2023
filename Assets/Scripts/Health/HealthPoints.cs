using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{

    [SerializeField] public int maximumHealth;
    public float currentHealth;

    public bool invulnerable;
    [SerializeField] public int healDEV;
    [SerializeField] public int damageDEV;
    [SerializeField] private AudioSource hitSound;


    // Start is called before the first frame update
    public virtual void Start()
    {
        invulnerable = false;
        currentHealth = maximumHealth;
    }



    /// <summary>
    /// Deals the specified amount of damage to the entity owning this health script.
    /// This method enforces the minimum health value of zero.
    /// </summary>
    /// <param name="damage">
    /// The amount of damage to deal to this entity
    /// </param>
    /// <returns>
    /// The actual amount of damage dealt
    /// </returns>
    public virtual float damageEntity(float damage)
    {
        if (invulnerable == false) {
            if (hitSound != null) {
                hitSound.Play();
            }
            Debug.Log("take damage");
            float damageDealt = (currentHealth - damage <= 0f) ? currentHealth : damage;
            currentHealth -= damageDealt;
            if (currentHealth <= 0f)
            {

                kill();
            }
            return damageDealt;
        }
        return 0f;
    }



    /// <summary>
    /// Destroys the entity owning this health script.
    /// </summary>
    public virtual void kill()
    {
        Destroy(gameObject);
    }



    /// <summary>
    /// Provides the specified amount of healing to the entity owning this health script.
    /// This method enforces the maximum health value of the entity.
    /// </summary>
    /// <param name="healing">
    /// The amount of healing to provide to this entity
    /// </param>
    /// <returns>
    /// The actual amount of healing provided
    /// </returns>
    public virtual float healEntity(float healing)
    {
        float healingDealt = (currentHealth + healing >= maximumHealth) ? maximumHealth - currentHealth : healing;
        currentHealth += healingDealt;
        return healingDealt;
    }



    // Update is called once per frame
    void Update()
    {

        if (healDEV > 0)
        {
            healEntity(healDEV);
            healDEV = 0;
        }

        if (damageDEV > 0)
        {
            damageEntity(damageDEV);
            damageDEV = 0;
        }

    }
}

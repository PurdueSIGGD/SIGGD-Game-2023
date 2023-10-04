using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthBar healthBar;
     public int maxHealth;
    [SerializeField]
    private int damage;
    public int currHealth;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currHealth > 0)
            {
                currHealth -= damage;
                healthBar.SetHealth(currHealth);
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currHealth < maxHealth)
            {
                currHealth -= -1 * damage;
                healthBar.SetHealth(currHealth);
            }
        }
        if(currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

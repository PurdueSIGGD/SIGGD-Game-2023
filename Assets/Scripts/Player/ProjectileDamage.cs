using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    [SerializeField] int damage = 10;
    [SerializeField] bool piercing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag != "Enemy")
        {
            return;
        }

        HealthPoints health = other.gameObject.GetComponent<HealthPoints>();
        if (health != null)
        {
            health.damageEntity(damage);
        }
        if (!piercing)
        {
            Debug.Log("Projectile Hit: " + other.gameObject.tag);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

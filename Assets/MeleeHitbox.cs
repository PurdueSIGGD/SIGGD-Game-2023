using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{

    [SerializeField] int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, 1, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            return;
        }

        HealthPoints health = other.gameObject.GetComponent<HealthPoints>();
        if (health != null)
        {
            health.damageEntity(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

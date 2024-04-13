using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StupidEnemyAttack : MonoBehaviour
{

    [SerializeField] private float damage = 10f;
    [SerializeField] private float cooldownTime = 2f;

    private bool attackAvailable;

    // Start is called before the first frame update
    void Start()
    {
        attackAvailable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (attackAvailable && other.gameObject.tag == "Player") {

            HealthPoints health = other.gameObject.GetComponent<HealthPoints>();
            if (health != null)
            {
                health.damageEntity(damage);
                StartCoroutine(attackCooldownTimer());
            }
        }
    }

    public IEnumerator attackCooldownTimer()
    {
        attackAvailable = false;
        yield return new WaitForSeconds(cooldownTime);
        attackAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

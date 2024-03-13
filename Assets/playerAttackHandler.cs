using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackHandler : MonoBehaviour
{

    [SerializeField] LightResource playerLightPool;

    [SerializeField] GameObject rangedProjectile;
    [SerializeField] int rangedProjectileSpeed = 10;
    [SerializeField] float rangedRecoveryTime = 0.5f;
    [SerializeField] int rangedLightCost = 10;
    [SerializeField] bool fullAuto = false;

    private bool rangedAvailable;

    // Start is called before the first frame update
    void Start()
    {
        rangedAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rangedAvailable && ((Input.GetKeyDown(KeyCode.Mouse0) && !fullAuto) || (Input.GetKey(KeyCode.Mouse0) && fullAuto)))
        {
            if (playerLightPool.consumeLight(rangedLightCost) == 0)
            {
                return;
            }
            GameObject projectile = Instantiate(rangedProjectile, 
                                                (gameObject.transform.position + new Vector3(0, 1, 0)),
                                                gameObject.transform.rotation);
            Destroy(projectile, 3);
            projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, rangedProjectileSpeed));
            StartCoroutine(rangedRecoveryTimer());
        } 
    }



    public IEnumerator rangedRecoveryTimer()
    {
        rangedAvailable = false;
        yield return new WaitForSeconds(rangedRecoveryTime);
        rangedAvailable = true;
    }

    
}

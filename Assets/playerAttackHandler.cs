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
    [SerializeField] bool rangedFullAuto = false;

    [SerializeField] GameObject meleeHurtbox;
    [SerializeField] float meleeDuration = 0.35f;
    [SerializeField] float meleeRecoveryTime = 0.5f;
    [SerializeField] int meleeLightCost = 0;
    [SerializeField] bool meleeFullAuto = false;

    private bool rangedAvailable;
    private bool meleeAvailable;

    // Start is called before the first frame update
    void Start()
    {
        rangedAvailable = true;
        meleeAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rangedAvailable && ((Input.GetKeyDown(KeyCode.Mouse0) && !rangedFullAuto) || (Input.GetKey(KeyCode.Mouse0) && rangedFullAuto)))
        {
            if (rangedLightCost > 0 && playerLightPool.consumeLight(rangedLightCost) == 0)
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



        if (meleeAvailable && ((Input.GetKeyDown(KeyCode.Mouse1) && !meleeFullAuto) || (Input.GetKey(KeyCode.Mouse1) && meleeFullAuto)))
        {
            if (meleeLightCost > 0 && playerLightPool.consumeLight(meleeLightCost) == 0)
            {
                return;
            }
            GameObject hurtBox = Instantiate(meleeHurtbox,
                                             (gameObject.transform.localPosition + new Vector3(0, 1, 0)),
                                             gameObject.transform.rotation,
                                             gameObject.transform);
            Destroy(hurtBox, meleeDuration);
            StartCoroutine(meleeRecoveryTimer());
        }
    }



    public IEnumerator rangedRecoveryTimer()
    {
        rangedAvailable = false;
        yield return new WaitForSeconds(rangedRecoveryTime);
        rangedAvailable = true;
    }

    public IEnumerator meleeRecoveryTimer()
    {
        meleeAvailable = false;
        yield return new WaitForSeconds(meleeRecoveryTime);
        meleeAvailable = true;
    }

    
}

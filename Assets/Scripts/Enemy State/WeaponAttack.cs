using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Animator weaponAnimator;
    public bool isInRange;
    // Start is called before the first frame update
    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        isInRange = false;
        weaponAnimator.SetBool("WeaponAttackBool", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            weaponAnimator.SetBool("WeaponAttackBool", true);
        }
        else
        {
            weaponAnimator.SetBool("WeaponAttackBool", false);
        }
    }
}

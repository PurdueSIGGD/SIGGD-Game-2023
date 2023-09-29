using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    WeaponAttack weaponAttack;
    // Start is called before the first frame update
    void Start()
    {
        weaponAttack = this.transform.parent.gameObject.GetComponent<WeaponAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        weaponAttack.isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        weaponAttack.isInRange = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] weapons;
    private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weapons[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void ChangeWeapon(int index) {

    }

    public void ChangeWeapon(GameObject weapon) {

    }

    void OnAttack() {
        currentWeapon.GetComponent<WeaponScript>().PerformAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public GameObject[] weapons;
    private GameObject currentWeapon;
    private IWeapon currentWeaponScript;

    // Start is called before the first frame update
    void Start()
    {
        if (weapons.Length > 0) {
            currentWeapon = weapons[0];
            currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
        }
        Debug.Log("Weapon script is" + currentWeaponScript);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(int index) {
        if ((index >= 0) && (index < weapons.Length)) {
            currentWeapon = weapons[index];
            currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
        }
    }

    public void ChangeWeapon(GameObject weapon) {
        // Check if list contains the weapon object specified, and switch to it
        if (weapons.Contains<GameObject>(weapon)) {
            currentWeapon = weapon;
            currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
            Debug.Log("Weapon script is" + currentWeaponScript);
        }
    }

    void OnAttack1() {
        currentWeaponScript.PerformAttack(1);
        // Maybe combine attack2 into this method and check which button is pressed?
    }

    void OnChangeItem() {
        // Must pass in whether the scroll motion is up or down
    }
}

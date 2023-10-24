using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public GameObject[] weaponPrefabs; // Stores weapons inserted into the list in the editor
    private GameObject[] weaponObjects; // Stores the specific instances of all chosen weapons
    private GameObject currentWeapon;
    private IWeapon currentWeaponScript;
    private int currentWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Create weapon objects from prefabs
        weaponObjects = new GameObject[weaponPrefabs.Length];
        for (int i = 0; i < weaponPrefabs.Length; i++) {
            GameObject weapon = Instantiate(weaponPrefabs[i]);
            weapon.transform.parent = this.transform;
            weaponObjects[i] = weapon;
            weapon.GetComponent<IWeapon>().SetEnabled(false); // Disable all weapons at first
        }

        if (weaponObjects.Length > 0) {
            currentWeapon = weaponObjects[0];
            currentWeaponIndex = 0;
            currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
            weaponObjects[0].GetComponent<IWeapon>().SetEnabled(true);
        }
        // Debug.Log("Weapon script is" + currentWeaponScript);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableWeapon() {
        currentWeaponScript.SetEnabled(true);
    }

    public void DisableWeapon() {
        currentWeaponScript.SetEnabled(false);
    }

    public bool ChangeWeapon(int index) {
        if ((index >= 0) && (index < weaponPrefabs.Length)) {
            DisableWeapon();
            currentWeapon = weaponObjects[index];
            currentWeaponIndex = index;
            currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
            EnableWeapon();
            // Debug.Log("Weapon " + currentWeapon.name + " at index " + currentWeaponIndex + ".");
        }
        return false;
    }

    public bool ChangeWeapon(GameObject weapon) {
        // Check if list contains the weapon object specified, and switch to it
        for (int i = 0; i < weaponObjects.Length; i++) {
            if (weaponObjects[i] = weapon) {
                ChangeWeapon(i);
                return true;
            }
        }
        return false;
    }

    void OnAttack1() {
        currentWeaponScript.PerformAttack(1);
        // Maybe combine attack2 into this method and check which button is pressed?
    }

    void OnAttack2() {
        currentWeaponScript.PerformAttack(2);
    }

    void OnChangeItem(InputValue val) {
        // Debug.Log("value = " + val.Get<float>());
        if (val.Get<float>() > 0) {
            if (currentWeaponIndex == weaponObjects.Length - 1) {
                ChangeWeapon(0);
            } else {
                ChangeWeapon(currentWeaponIndex + 1);
            }
        } else if (val.Get<float>() < 0) {
            if (currentWeaponIndex == 0) {
                ChangeWeapon(weaponObjects.Length - 1);
            } else {
                ChangeWeapon(currentWeaponIndex - 1);
            }
        }
    }
}

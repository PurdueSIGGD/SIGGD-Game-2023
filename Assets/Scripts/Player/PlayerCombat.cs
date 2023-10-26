using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public GameObject[] weaponPrefabs; // Stores weapons inserted into the list in the editor
    private GameObject[] weaponObjects; // Stores the specific instances of all chosen weapons
    private GameObject currentWeapon;
    private IWeapon currentWeaponScript;
    private int currentWeaponIndex;

    // Initializes weaponObjects array and other variables.
    void Start()
    {
        // Create weapon objects from prefabs
        weaponObjects = new GameObject[weaponPrefabs.Length];
        for (int i = 0; i < weaponPrefabs.Length; i++) {
            if (weaponPrefabs[i] != null) {
                GameObject weapon = Instantiate(weaponPrefabs[i]);
                weapon.transform.parent = this.transform;
                weaponObjects[i] = weapon;
                weapon.GetComponent<IWeapon>().SetEnabled(false);
            }
        }

        // If no prefabs were provided, make a single null entry so the script works
        if (weaponObjects.Length == 0) {
            weaponObjects = new GameObject[1];
        }

        // Set the current weapon to be the first one in the list (even if null)
        currentWeapon = weaponObjects[0];
        currentWeaponIndex = 0;
        if (currentWeapon != null) {
            currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
        }
    }

    // Enables the current weapon.
    public void EnableWeapon() {
        currentWeaponScript?.SetEnabled(true); // ? is null check
    }

    // Disables the current weapon.
    public void DisableWeapon() {
        currentWeaponScript?.SetEnabled(false); // ? is null check
    }

    // Sets the current weapon to be weaponObjects[index], enables it, and disables the previous weapon.
    public bool ChangeWeapon(int index) {
        if (currentWeapon != null) {
            this.DisableWeapon();
        }
        if ((index >= 0) && (index < weaponPrefabs.Length)) {
            currentWeapon = weaponObjects[index];
            currentWeaponIndex = index;
            if (currentWeapon != null) {
                currentWeaponScript = currentWeapon.GetComponent<IWeapon>();
                this.EnableWeapon();
            }
            return true;
        }
        return false;
    }

    // Sets the current weapon to be the provided GameObject if it's in the weaponObjects array,
    // enables it, and disables the previous weapon.
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

    // Triggered by primary action button.
    void OnAttack1() {
        currentWeaponScript?.PerformAttack(1); // ? is null check
    }

    // Triggered by secondary action button.
    void OnAttack2() {
        currentWeaponScript?.PerformAttack(2); // ? is null check
    }

    // Triggered by the scroll wheel. This changes the weapon to the next or previous one in the
    // weaponObjects array, wrapping to the opposite end of the array if needed.
    void OnChangeItem(InputValue val) {
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

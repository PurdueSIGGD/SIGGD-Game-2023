using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    // Objects
    [SerializeField]
    private GameObject centerPoint; // Center point of player, for spawning hitboxes and projectiles
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject sword;

    // Gun attributes


    [SerializeField]
    private float bulletSpeed; // Projectile speed in meters/sec
    [SerializeField]
    private float fireRate; // Fire rate in seconds
    [SerializeField]
    private float fireCost; // Cost, in light, of each weapon fire

    // Sword attributes


    // Testing variables for now. These will eventually be pulled from other scripts.
    [SerializeField]
    private int mLight = 100;
    [SerializeField]
    private int lightRegen = 2; // Light regenerated per second

    // Initializes weaponObjects array and other variables.
    void Start()
    {
        // Ensure weapon objects are properly specified
        if ((gun == null) || (bullet == null) || (sword == null) || (centerPoint == null)) {
            Debug.Log("One or more required player combat objects are not assigned to serialized variables!");
        }

        // Initialize player light supply and make sure it's >= 0
        if (mLight < 0) {
            mLight = 0;
        }
    }

    // Triggered by right click. Shoots light gun.
    void OnShootGun() {

        Debug.Log("Shooting gun");

        // Check if light is zero
        if (mLight <= 0) { return; }

        // Check which direction the player is facing
        Vector3 playerLook = transform.forward;

        // Create projectile clone
        GameObject shotBullet = Instantiate<GameObject>(bullet);
        shotBullet.transform.SetLocalPositionAndRotation(centerPoint.transform.position, new Quaternion(0, 0, 0, 0));

        // Set bullet velocity
        shotBullet.GetComponent<Rigidbody>().velocity = playerLook * bulletSpeed;

        Debug.Log("Player look vector: " + playerLook);
        Debug.Log("Bullet position: " + shotBullet.transform.position);
        Debug.Log("Bullet velocity: " + shotBullet.GetComponent<Rigidbody>().velocity);

    }

    // Triggered by left click. Swings sword.
    void OnSwingSword() {
        // Trigger invisible hitbox in front of player. Also trigger animations.
    }

    void LightRegen() {

    }
}

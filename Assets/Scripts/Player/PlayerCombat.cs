using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject sword;
    private int mLight; // The amount of light the player has. May be from an external script in the future.

    [SerializeField]
    private float bulletSpeed; // Projectile speed in meters/sec
    [SerializeField]
    private float fireRate; // Fire rate in seconds
    [SerializeField]
    private float fireCost; // Cost, in light, of each weapon fire

    // Initializes weaponObjects array and other variables.
    void Start()
    {
        // Ensure weapon objects are properly specified
        if ((gun == null) || (bullet == null) || (sword == null)) {
            Debug.Log("One or more player weapon objects are not assigned!");
        }

        mLight = 100;
    }

    // Triggered by primary action button. Shoots light gun.
    void OnAttack1() {

        Debug.Log("Attack 1 triggered");

        // Check if light is zero
        if (mLight <= 0) { return; }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,
            float.PositiveInfinity)) {

            // If a collider was hit
            if (hit.collider != null) {
                Vector3 clickPoint = hit.point;
                // Draw a line hitbox from character to click point, purposefully aiming the
                // line a specified distance above the ground (if the click point is ground)
                Vector3 bulletDir = clickPoint - transform.position;
                Physics.Raycast(transform.position, bulletDir, out RaycastHit bulletHit);

                // Determine spot where bullet path ends
                Vector3 bulletEnd;
                if (bulletHit.collider != null) {
                    bulletEnd = bulletHit.point;
                } else {
                    bulletEnd = Vector3.zero;
                }

                // Create projectile clone
                GameObject shotBullet = Instantiate<GameObject>(bullet);
                shotBullet.transform.parent = transform;
                shotBullet.transform.localPosition = Vector3.zero;

                // Set bullet velocity
                Vector3 bulletVel = Vector3.Normalize(new Vector3(bulletEnd.x - transform.position.x,
                        bulletEnd.y - transform.position.y,
                        bulletEnd.z - transform.position.z)) * bulletSpeed;
                shotBullet.GetComponent<Rigidbody>().velocity = bulletVel;
            }
        }
    }

    // Triggered by secondary action button. Swings sword.
    void OnAttack2() {
        // Trigger invisible hitbox in front of player. Also trigger animations.
    }
}

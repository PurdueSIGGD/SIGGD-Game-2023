using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    const float SPEED = 0.1f;
    [SerializeField] TurretController tc;
    public bool airborne;

    private Vector3 moveVec;

    // Player movement function
    public void OnMove(InputValue action)
    {
        // Vector to move the player along
        moveVec = new Vector3(action.Get<Vector2>().x, 0, action.Get<Vector2>().y);
    }

    // Escape place mode
    public void OnEscapeTurret() => tc.OnEscape();

    // Place turret
    public void OnPlaceTurret() => tc.PlaceTurret();

    public void OnUpgradeturret() => tc.UpgradeTurret();

    private void FixedUpdate()
    {
        // Move the player to the appropriate position
        transform.position += SPEED * moveVec;
        // Update the turret controller player position
        tc.playerPosition = transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3) // Turret Placeable Layer (Ground)
        {
            airborne = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

/* 
 * DemoMovement
 * ------------
 * Controls Player Movement
 */
public class Movement : MonoBehaviour
{
    // Serialize Fields
    [SerializeField] Camera mainCamera;

    // Private Variables
    private Vector2 inputDirection;
    private Rigidbody RB;
    private PlayerData data;
    public bool sirend;
    public bool rooted;

    [SerializeField] SpriteRenderer crosshairSprite;
    private Vector3 crosshairPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        sirend = false;
        rooted = false;
        RB = GetComponent<Rigidbody>();
        data = GetComponent<PlayerData>();
        Cursor.visible = false;
    }

    // Regular Frame Update
    void Update()
    {
        Cursor.visible = false;
        /*
        // Poll - Update Player Direction Based on Mouse Position
        Ray camToWorld = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Plane xz = new Plane(Vector3.up, Vector3.zero);
        xz.Raycast(camToWorld, out float dist);
        Vector3 hit = camToWorld.GetPoint(dist) + Vector3.up;

        this.transform.LookAt(hit);

        crosshairPosition = hit + (Vector3.up * 0.9f);
        crosshairSprite.gameObject.transform.position = crosshairPosition;
        Vector3 direction = (gameObject.transform.position - mainCamera.gameObject.transform.position);
        direction.Normalize();
        crosshairSprite.gameObject.transform.LookAt(crosshairSprite.gameObject.transform.position + direction);
        //crosshairSprite.material.renderQueue = 4000;
        */
    }


    // Input Action (Move) - Update Move Direction
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    private void move()
    {

        if (rooted)
        {
            RB.velocity = Vector3.zero;
            return;
        }

        // Find Input Angle
        Vector3 inputVector = new Vector3(inputDirection.x, 0, inputDirection.y);
        float angle = Vector3.Angle(Vector3.forward, inputVector);
        angle *= (inputVector.x < 0) ? -1 : 1;

        // Apply Input Angle to Camera Face Direction
        Vector3 cameraForward = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
        Vector3 rotated = Quaternion.AngleAxis(angle, Vector3.up) * cameraForward;

        if (sirend == false) {
            // Calcualte Target Velocity
            Vector3 targetVelocity = (rotated * inputVector.magnitude).normalized * data.GetMaxSpeed();
            RB.velocity = targetVelocity;
        }
        else {
            //RB.velocity = Vector3.zero;
        }
    }

    // Physics Update
    void FixedUpdate()
    {
        move();

        // Poll - Update Player Direction Based on Mouse Position
        Ray camToWorld = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Plane xz = new Plane(Vector3.up, Vector3.zero);
        xz.Raycast(camToWorld, out float dist);
        Vector3 hit = camToWorld.GetPoint(dist) + Vector3.up;

        this.transform.LookAt(hit);

        crosshairPosition = hit + (Vector3.up * 1f);
        crosshairSprite.gameObject.transform.position = crosshairPosition;
        Vector3 direction = (gameObject.transform.position - mainCamera.gameObject.transform.position);
        direction.Normalize();
        crosshairSprite.gameObject.transform.LookAt(crosshairSprite.gameObject.transform.position + direction);
    }
}

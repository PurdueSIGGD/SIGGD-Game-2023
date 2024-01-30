using DefaultNamespace;
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
public class DemoMovement : MonoBehaviour
{
    // Serialize Fields
    [SerializeField] Camera mainCamera;

    // Private Variables
    private Vector2 inputDirection;
    private Rigidbody RB;
    private DemoPlayerData data;


    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        RB = GetComponent<Rigidbody>();
        data = GetComponent<DemoPlayerData>();
    }

    // Regular Frame Update
    void Update()
    {
        // Poll - Update Player Direction Based on Mouse Position
        Ray camToWorld = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
        Plane xz = new Plane(Vector3.up, Vector3.zero);
        xz.Raycast(camToWorld, out float dist);
        Vector3 hit = camToWorld.GetPoint(dist);

        this.transform.LookAt(hit);
    }

    // Input Action (Move) - Update Move Direction
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    private void move()
    {
        // Find Input Angle
        Vector3 inputVector = new Vector3(inputDirection.x, 0, inputDirection.y);
        float angle = Vector3.Angle(Vector3.forward, inputVector);
        angle *= (inputVector.x < 0) ? -1 : 1;

        // Apply Input Angle to Camera Face Direction
        Vector3 cameraForward = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
        Vector3 rotated = Quaternion.AngleAxis(angle, Vector3.up) * cameraForward;

        // Calcualte Target Velocity
        Vector3 targetVelocity = (rotated * inputVector.magnitude).normalized * data.GetMaxSpeed();
        RB.velocity = targetVelocity;
    }

    // Physics Update
    void FixedUpdate()
    {
        move();
    }
}

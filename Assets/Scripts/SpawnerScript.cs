using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject turretAccepted;
    [SerializeField] private GameObject turretDenied;
    [SerializeField] private MeshCollider ghostCollider;

    private GameObject turretGhost;
    private static bool wasSpawned;
    private static bool pressedE;
    private static bool hasSpace;
    private static bool noSpace;
    private static Ray ray;

    void Start()
    {
        pressedE = false;
        wasSpawned = false;
        hasSpace = true;
        noSpace = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasSpace = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        hasSpace = true;
    }
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Boolean raycast = Physics.Raycast(ray, out RaycastHit raycastHit);
        if (raycast)
        {
            //updates the hitbox of the "ghost" turret
            transform.position = raycastHit.point + new Vector3(0.3f, 1.2f, 0f);

            if (pressedE == true)
            {
                turretGhost.transform.position = raycastHit.point;
                
                //if statements test for collision and displays either green or red turret
                if (hasSpace == false && noSpace == false)
                {
                    Destroy(turretGhost);
                    turretGhost = Instantiate(turretDenied, raycastHit.point, Quaternion.identity);
                    noSpace = true;
                }
                else if (hasSpace == true && noSpace == true)
                {
                    Destroy(turretGhost);
                    turretGhost = Instantiate(turretAccepted, raycastHit.point, Quaternion.identity);
                    noSpace = false;
                }

                //spawns the turret on lmb
                if (wasSpawned == false && noSpace == false && Input.GetMouseButtonDown(0))
                {
                    Instantiate(turret, raycastHit.point, Quaternion.identity);
                    Debug.Log(Input.mousePosition);
                    wasSpawned = true;
                }
                //exit
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(turretGhost);
                    pressedE = false;
                }
            }
            else if (pressedE == false && Input.GetKeyDown(KeyCode.E))
            {
                turretGhost = Instantiate(turretAccepted, raycastHit.point, Quaternion.identity);
                pressedE = true;
            }
        }
        wasSpawned = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * DemoMovement
 * ------------
 * Controls Player Movement
 * TODO : Work on Unit Abstraction Hierarchy (Unit System -> Turrets || Single Use -> Specific Turret / SU classes)
 * All units have a cost and a gameobject associated with them. All units will have an Action function. Turrets may
 * repeat shooting actions where single use items may not.
 * TODO : Rework Turret Aim to work with this new script
 * TODO : Make sure the TurretController keeps track of turrets and does not exceed turret cap, where 0 = infinite turrets
 * TODO : Add in all desired subclasses of turrets
 * TODO : Turrets in wall
 */
public class DemoMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Poll - Update Player Direction Based on Mouse Position
        Ray camToWorld = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane xz = new Plane(Vector3.up, Vector3.zero);
        xz.Raycast(camToWorld, out float dist);
        Vector3 hit = camToWorld.GetPoint(dist);

        this.transform.LookAt(hit);
    }
}

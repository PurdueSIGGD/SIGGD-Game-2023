using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* 
 * TURRET CONTROLLER
 * Instantiates and destroys turrets
 * TODO : Work on Unit Abstraction Hierarchy (Unit System -> Turrets || Single Use -> Specific Turret / SU classes)
 * All units have a cost and a gameobject associated with them. All units will have an Action function. Turrets may
 * repeat shooting actions where single use items may not.
 * TODO : Rework Turret Aim to work with this new script
 * TODO : Make sure the TurretController keeps track of turrets and does not exceed turret cap, where 0 = infinite turrets
 * TODO : Add in all desired subclasses of turrets
 * TODO : Turrets in wall
 */
public class TurretController : MonoBehaviour
{
    // Blank turret model reference
    [SerializeField] GameObject blankModel;

    // Camera reference
    [SerializeField] Camera mainCamera;

    // Turret cap (0 = inf)
    [SerializeField] private int TURRET_CAP;

    // How far away and close to player turret can be placed (min distance prevents clipping)
    [SerializeField] private float PLACE_MIN;
    [SerializeField] private float PLACE_MAX;

    // Maximum placement angle for turrets
    [SerializeField] private float MAX_PLACE_ANGLE;

    // The position of the player
    [HideInInspector] public Vector3 playerPosition;

    // Bool for if in place mode
    private bool placeMode = false;

    // Bool for if the active position is placeable
    private bool canPlace = false;

    // Unit type
    private GameObject unitToSpawn;

    // Raycast hit
    private RaycastHit hit;

    // Position of raycast mouse in world space
    private Vector3 position = Vector3.zero;

    private void Awake()
    {
        blankModel.SetActive(false);
    }

    // Turret selected from button
    public void OnSelect(GameObject unit)
    {
        // Set the unit type
        unitToSpawn = unit;

        // Toggle turrets placeable boolean
        placeMode = true;

        // Enter place mode
        EnterPlaceMode();
    }

    // Escape key pressed to leave place mode
    public void OnEscape()
    {
        // Set place mode to false
        if (placeMode)
        {
            placeMode = false;
        }

        // Execute exit place mode
        ExitPlaceMode();
    }

    // Enter place mode
    void EnterPlaceMode()
    {
        // Check the model
        bool valid = CheckValid();

        // Get transform
        (Vector3 pos, Vector3 rot) = GetTransform();

        // If position is defined, place on surface
        if (!pos.Equals(Vector3.positiveInfinity))
        {
            blankModel.SetActive(true);
            blankModel.transform.position = pos;
        }

        // If the model is valid, set the color to green. If not, red
        if (valid)
        {
            SetColor(blankModel, Color.green);
        } else
        {
            SetColor(blankModel, Color.red);
        }
    }

    // Exit place mode
    void ExitPlaceMode()
    {
        // Set the blank model to be inactive
        blankModel.SetActive(false);
    }

    // Place the turret
    public void PlaceTurret()
    {
        // Get validity
        bool valid = CheckValid();

        // Get spawn location
        (Vector3 pos, Vector3 rot) = GetTransform();

        // Check validity
        if (valid )
        {
            // If the proposed gameobject has a unit behavior, instantiate at position. If not, log warning
            if (unitToSpawn.GetComponent<Unit>() != null)
            {
                GameObject newUnit = Instantiate(unitToSpawn, pos, Quaternion.identity);
                newUnit.transform.up = rot;
            }
            else
            {
                Debug.LogWarning("The object you are trying to instantiate is not a unit; please ensure you are using a unit!");
            }
        }
    }

    // Set the blank model color
    void SetColor(GameObject model, Color color)
    {
        if (model.GetComponent<Renderer>() != null)
        {
            model.GetComponent<Renderer>().material.color = color;
        } else
        {
            Renderer r = model.AddComponent<Renderer>();
            r.material.color = color;
        }
    }

    // Returns true if the proposed turret placement position is valid, false otherwise
    // TODO : Check distance to walls
    bool CheckValid()
    {
        // Ray from mouse position into screen
        Ray camToWorld = mainCamera.ScreenPointToRay(Input.mousePosition);

        // If no placeable surface, return not valid
        if (!Physics.Raycast(camToWorld, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Placeable")))
            return false;

        // If distance too far, return not valid
        if (!((Vector3.Distance(hit.point, playerPosition) >= PLACE_MIN) && (Vector3.Distance(hit.point, playerPosition) <= PLACE_MAX)))
        {
            return false;
        }

        // If hit normal y component is too low (slope too steep), return not valid
        if (Mathf.Abs(hit.normal.y) < Mathf.Cos(Mathf.Deg2Rad * MAX_PLACE_ANGLE))
        {
            return false;
        }

        // If point is on placeable surface close to player, return valid
        return true;
    }

    // Return the position and rotation of raycast hit of mouse
    (Vector3, Vector3) GetTransform()
    {
        // Ray from mouse position into screen
        Ray camToWorld = mainCamera.ScreenPointToRay(Input.mousePosition);

        // If no placeable surface, return + infinity for position and zero rotation
        if (!Physics.Raycast(camToWorld, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Placeable")))
            return (Vector3.positiveInfinity, Vector3.zero);

        // If point is on placeable surface return the hit point and normal
        return (hit.point, hit.normal);
    }

    // In update, perform place mode updates if canPlace is true
    private void FixedUpdate()
    {
        if (placeMode)
        {
            //Get valid
            bool valid = CheckValid();
            // Get transform
            (Vector3 pos, Vector3 rot) = GetTransform();

            // If position is infinity, hide GO
            if (pos.Equals(Vector3.positiveInfinity))
            {
                blankModel.SetActive(false);
            }
            else
            {
                // If now valid after not being valid, make valid and set color to green
                if (!canPlace && valid)
                {
                    canPlace = true;
                    SetColor(blankModel, Color.green);
                }
                // If now invalid after being valid, make invalid and set color to red
                if (canPlace && !valid)
                {
                    canPlace = false;
                    SetColor(blankModel, Color.red);
                }

                // Reset position, rotation
                blankModel.transform.position = pos;
                blankModel.transform.up = rot;
            }
        }
    }

}

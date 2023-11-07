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
 * TODO : Add instantiation ( referenced in comment below )
 * TODO : Add in all desired subclasses of turrets
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
    [SerializeField] private float PLACE_MIN = 3;
    [SerializeField] private float PLACE_MAX = 5;

    // The position of the player
    [HideInInspector] public Vector3 playerPosition;

    // Bool for if in place mode
    private bool placeMode = false;

    // Bool for if the active position is placeable
    private bool canPlace = false;

    // Unit type
    private string unitType;

    // Raycast hit
    private RaycastHit hit;

    // Position of raycast mouse in world space
    private Vector3 position = Vector3.zero;

    private void Awake()
    {
        blankModel.SetActive(false);
    }

    // Turret selected from button
    public void OnSelect(string typeName)
    {
        // Set the unit type
        unitType = typeName;

        // Toggle turrets placeable boolean
        placeMode = true;

        // Enter place mode
        EnterPlaceMode();
    }

    // Escape key pressed to leave place mode
    public void OnEscape()
    {
        Debug.Log("Escaping");

        // Set place mode to false
        if (placeMode)
        {
            placeMode = false;
        }

        // Execute exit place mode
        ExitPlaceMode();
    }

    // Set the type from a string. Undefined string = shooter turret
    public Unit CreateUnit(string uType)
    {
        GameObject newObj = new GameObject();
        Unit unit;
        switch (uType) {
            case "ShooterTurret":
                unit = newObj.AddComponent<ShooterTurret>();
                break;
            case "RocketTurret":
                unit = newObj.AddComponent<RocketTurret>();
                break;
            default:
                Debug.LogWarning("Button not tied to a specific turret. Using shooter turret by default.");
                unit = newObj.AddComponent<ShooterTurret>();
                break;
        }
        return unit;
    }

    // Enter place mode
    void EnterPlaceMode()
    {
        // Check the model
        (bool valid, Vector3 pos) = CheckValidAndGetPos();

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
        Unit placeUnit = CreateUnit(unitType);
        // TODO : Instantiate the object associated with the player
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
    // If the position on a placeable surface exists, it is returned, even if it is too far from the player
    // If the raycast returns no results, positive infinity is returned for the vector
    (bool, Vector3) CheckValidAndGetPos()
    {
        // Ray from mouse position into screen
        Ray camToWorld = mainCamera.ScreenPointToRay(Input.mousePosition);

        // If no placeable surface, return not valid
        if (!Physics.Raycast(camToWorld, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Placeable")))
            return (false, Vector3.positiveInfinity);

        // If distance too far, return not valid
        if (!(Vector3.Distance(hit.point, playerPosition) >= PLACE_MIN && Vector3.Distance(hit.point, playerPosition) <= PLACE_MAX))
        {
            return (false, hit.point);
        }

        // If point is on placeable surface close to player, return valid
        return (true, hit.point);
    }

    // In update, perform place mode updates if canPlace is true
    private void FixedUpdate()
    {
        if (placeMode)
        {
            //Get valid and pos
            (bool valid, Vector3 pos) = CheckValidAndGetPos();
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

                // Reset position
                blankModel.transform.position = pos;
            }
        }
    }

}

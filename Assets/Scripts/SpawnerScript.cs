using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject turretAccepted;
    [SerializeField] private GameObject turretDenied;
    private GameObject turretGhost;
    private GameObject turretGhostToPlace;

    [SerializeField] private GameObject owner;
    [SerializeField] private float PLACE_RANGE = 5;

    private RaycastHit hit;
    private static bool hoverTurret;
    private static bool hasSpace;
    private bool collided;

    private Vector3 toOwner 
    {
        get => owner.transform.position - transform.position;
    }

    void Start()
    {
        hoverTurret = false;
        hasSpace = true;
        turretGhostToPlace = turretAccepted;
    }

    private void CheckOutOfRange(Vector3 toOwner)
    {
        if (!collided) 
            setPlaceability(toOwner.magnitude < PLACE_RANGE);
    }

    public void CheckOutOfRange() => CheckOutOfRange(toOwner);

    // InputSystem Hook, set to moving your mouse.
    public void OnHoverPlaceTurret(Vector2 hover)
    {
        Ray camToWorld = mainCamera.ScreenPointToRay(hover);

        if (!Physics.Raycast(camToWorld, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Turret Placeable")))
        //if (!Physics.Raycast(camToWorld, out RaycastHit hit))
            return;

        this.hit = hit;
        // The offset from the hit point is arbitrarily chosen right now. Might be the cause of weird hitbox collisions.
        transform.position = hit.point + new Vector3(0.3f, 1.2f, 0f);
        if (hoverTurret)
        {
            CheckOutOfRange();
            UpdateGhost();
        }
    }

    public void UpdateGhost(Vector3 toOwner)
    {
        if (!hoverTurret) return;

        Destroy(turretGhost);
        turretGhost = Instantiate(turretGhostToPlace, hit.point, Quaternion.identity);
        turretGhost.transform.position = hit.point;
        // The ghost is only supposed to be placeable upright, so convert toOwner to that.
        Vector3 toOwnerFlattened = toOwner;
        toOwnerFlattened.y = 0;
        Quaternion look = Quaternion.LookRotation(toOwnerFlattened, Vector3.up);
        // Rotate the asset 90 degrees instead of use this code, this is dumb.
        look.eulerAngles += new Vector3(0, 90, 0);
        turretGhost.transform.rotation = look;
    }

    // Helper function to update the position and type (red or green) of the turretGhost.
    // OPTIMIZE THIS 
    public void UpdateGhost() => UpdateGhost(toOwner);

    // InputSystem Hook, set to LMB.
    public void OnPlaceTurret()
    {
        if (hoverTurret && hasSpace)
        {
            Quaternion rotation = turretGhost.transform.rotation;
            Instantiate(turret, turretGhost.transform.position, rotation);
        }
    }

    // InputSystem Hook, set to E or Space.
    public void OnTogglePlaceTurret()
    {
        if (hoverTurret)
        {
            Destroy(turretGhost);
            hoverTurret = !hoverTurret;
        }
        else
        {
            hoverTurret = !hoverTurret;
            UpdateGhost();
        }
    }

    // Collision Hook
    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
        setPlaceability(false);
    }

    // Collision Hook
    private void OnCollisionExit(Collision collision)
    {
        collided = false;
        setPlaceability(true);
    }

    private void setPlaceability(bool canPlace) 
    {
        hasSpace = canPlace;
        turretGhostToPlace = canPlace ? turretAccepted : turretDenied;
    }

}

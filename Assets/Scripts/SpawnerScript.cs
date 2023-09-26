using UnityEngine;
using UnityEngine.InputSystem;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject turretAccepted;
    [SerializeField] private GameObject turretDenied;
    [SerializeField] private MeshCollider ghostCollider;
    [SerializeField] private TurretController controller;

    private RaycastHit hit;
    private GameObject turretGhost;
    private GameObject turretGhostToPlace;
    private static bool hoverTurret;
    private static bool hasSpace;
    private static Ray ray;

    void Start()
    {
        hoverTurret = false;
        hasSpace = true;
        turretGhostToPlace = turretAccepted;
    }

    // InputSystem Hook, set to moving your mouse.
    public void OnHoverPlaceTurret(InputValue action)
    {
        Vector2 hover = action.Get<Vector2>();
        Ray camToWorld = mainCamera.ScreenPointToRay(hover);

        if (Physics.Raycast(camToWorld, out RaycastHit hit))
        {
            this.hit = hit;
            // The offset from the hit point is arbitrarily chosen right now. Might be the cause of weird hitbox collisions.
            transform.position = hit.point + new Vector3(0.3f, 1.2f, 0f);
            if (hoverTurret)
                calcPlaceablity();
        }
    }

    // Helper function to update the position and type (red or green) of the turretGhost.
    // OPTIMIZE THIS 
    private void calcPlaceablity()
    {
        Destroy(turretGhost);
        turretGhost = Instantiate(turretGhostToPlace, hit.point, Quaternion.identity);
        turretGhost.transform.position = hit.point;
    }

    // InputSystem Hook, set to LMB.
    public void OnPlaceTurret()
    {
        if (hoverTurret && hasSpace)
            Instantiate(turret, hit.point, Quaternion.identity);
    }

    // InputSystem Hook, set to E or Space.
    public void OnTogglePlaceTurret()
    {                
        if (hoverTurret)
            Destroy(turretGhost);
        else
            calcPlaceablity();

        hoverTurret = !hoverTurret;
    }

    // Collision Hook
    private void OnCollisionEnter(Collision collision)
    {
        hasSpace = false;
        turretGhostToPlace = turretDenied;
    }

    // Collision Hook
    private void OnCollisionExit(Collision collision)
    {
        hasSpace = true;
        turretGhostToPlace = turretAccepted;
    }

}

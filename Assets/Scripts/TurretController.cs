using UnityEngine.InputSystem;

public interface TurretController 
{
    void HoverPlaceTurret(Spawn _, InputValue action);
    void TogglePlaceTurret(Spawn _);

    void PlaceTurret(Spawn _);
}
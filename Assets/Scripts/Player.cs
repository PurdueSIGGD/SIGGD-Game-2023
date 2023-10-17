using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, TurretController
{
    const float SPEED = 2f;
    private Vector2 move;
    private bool placingTurret = true;
    [SerializeField] Spawn spawn;
    public bool airborne;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnMove(InputValue action) 
    {
        move = action.Get<Vector2>();
        spawn.CheckOutOfRange();
        spawn.UpdateGhost();
    }

    public void HoverPlaceTurret(Spawn s, InputValue action) => s.OnHoverPlaceTurret(action.Get<Vector2>());
    public void TogglePlaceTurret(Spawn s) => s.OnTogglePlaceTurret();
    public void PlaceTurret(Spawn s) => s.OnPlaceTurret();

    // InputSystem Hook
    public void OnHoverPlaceTurret(InputValue action) => HoverPlaceTurret(spawn, action);

    // InputSystem Hook
    public void OnInteractTurret()
    {
        IdleTurret[] turrets = FindObjectsOfType<IdleTurret>();
        List<IdleTurret> turretsList = new List<IdleTurret>(turrets).Where(t => t.IsInteractable).ToList();
        if (turretsList.Count == 0) 
            return;

        turretsList.Sort((a, b) => 
        {
            var distA = (a.transform.position - transform.position).magnitude;
            var distB = (b.transform.position - transform.position).magnitude;
            return distA.CompareTo(distB);
        });
        turretsList.First().Interact(gameObject);
    }

    // InputSystem Hook
    public void OnTogglePlaceTurret()
    {
        TogglePlaceTurret(spawn);
        if (placingTurret)
        {
            print("Now, please place the turret!");
        }
        placingTurret = !placingTurret;
    }

    // InputSystem Hook
    public void OnPlaceTurret()
    {
        PlaceTurret(spawn);
        if (placingTurret) 
            print("TODO: placing turret");
        else 
            print("Wait, not placing turret! Toggle with spacebar.");
    }

    public void OnRotateTurret(InputValue action)
    {
        Vector2 scroll = action.Get<Vector2>();
        spawn.OnRotateTurrent(scroll);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMove = new Vector3(move.x, 0, move.y) * SPEED;
        transform.position += Time.deltaTime * deltaMove; 
        if (move != Vector2.zero)
        {
            spawn.CheckOutOfRange();
            spawn.UpdateGhost();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3) // Turret Placeable Layer (Ground)
        {
            airborne = false;
        }
        
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, TurretController
{
    const float SPEED = 2f;
    private Vector2 move;
    private bool placingTurret = true;
    [SerializeField] Spawn spawn;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnMove(InputValue action) 
    {
        move = action.Get<Vector2>();
    }

    public void HoverPlaceTurret(Spawn s, InputValue action) => s.OnHoverPlaceTurret(action);
    public void TogglePlaceTurret(Spawn s) => s.OnTogglePlaceTurret();
    public void PlaceTurret(Spawn s) => s.OnPlaceTurret();

    // InputSystem Hook
    public void OnHoverPlaceTurret(InputValue action) => HoverPlaceTurret(spawn, action);

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

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMove = new Vector3(move.x, 0, move.y) * SPEED;
        transform.position += Time.deltaTime * deltaMove; 
    }
}

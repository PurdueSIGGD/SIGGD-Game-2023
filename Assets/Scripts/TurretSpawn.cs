using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSpawn : MonoBehaviour
{
    const float SPEED = 2f;
    private Vector2 move;
    private bool placingTurret = true;
    private GameObject turret;
    
    // Start is called before the first frame update
    void Start()
    {
        // turret = new Turret();
    }

    public void OnMove(InputValue action) 
    {
        move = action.Get<Vector2>();
    }

    // InputSystem Hook
    public void OnTogglePlaceTurret()
    {
        if (placingTurret)
        {
            print("Now, please place the turret!");
        }
        placingTurret = !placingTurret;
    }

    // InputSystem Hook
    public void OnPlaceTurret()
    {
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

        // Preview the turret to place
        /*
        if (placingTurret)
        {
            turret.SetActive(true);
        }
        */
    }
}

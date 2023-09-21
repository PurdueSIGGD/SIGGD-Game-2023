using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject owner;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnHoverPlaceTurret(InputValue action)
    {
        Vector2 hover = action.Get<Vector2>();
        Ray camToWorld = mainCam.ScreenPointToRay(hover);
        // y location is hover height.
        // pos.y = owner.transform.position.y;

        if (Physics.Raycast(camToWorld, out RaycastHit hit))
        {
            gameObject.transform.position = hit.point;
        }
        print(hover);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

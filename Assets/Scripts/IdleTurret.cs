using UnityEngine;

public class IdleTurret : MonoBehaviour
{
    public bool IsInteractable;

    // This variable should be set when this GameObject is Instantiated.

    // For now, let's just say we face the interacter. A lerp can be added.
    public void Interact(GameObject interacter) 
    {
        Vector3 toOwner = transform.position - interacter.transform.position;
        Vector3 toOwnerFlattened = toOwner;
        toOwnerFlattened.y = 0;
        Quaternion look = Quaternion.LookRotation(toOwnerFlattened, Vector3.up);
        // Rotate the asset 90 degrees instead of use this code, this is dumb.
        look.eulerAngles += new Vector3(0, 90, 0);
        transform.rotation = look;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}

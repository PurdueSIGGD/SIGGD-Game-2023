using UnityEngine;

// NOTE: This is designed to be places on an abject which is at the floor of were the enemy will be standing
// There should be a child object which holds the sprite with its bottom on the floor. This object will be
// rotated to look at the cam, which the child will be rotated and have its sprite changed to represent
// were it is "looking". This script should also be a child of an object which sets its lookDirection each frame.

[RequireComponent(typeof(Animator))]
public class DirectionalSprite : MonoBehaviour
{
    private Animator animator;
    private Transform cameraTransform;
    [SerializeField] private Transform spritePlane;
    [SerializeField] private int directionNumber = 2;
    private static readonly int Direction = Animator.StringToHash("Direction");
    [SerializeField] private float rotationOffset;
    public Vector3 lookDirection;

    // To make things easier, dupe sprites over the y axis so the number is ~/2, probably just do this in the
    // animator
    
    // Default direction assumed to be left
    
    // Choose sprite based on closest sprite which matches its local angle compared to the camera's up axis
    // (Do this by changing a value in the animator)
    
    // Have the plane look towards the camera
    
    // Then rotate the plane an the axis facing the camera by its offset direction to its sprite's "true" angle to
    // correct for the offset.
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var cameraForward = cameraTransform.forward;
        
        // Create camera plane representation
        var cameraPlane = new Plane(-cameraForward, cameraTransform.position);
        var dirToPlane = position - cameraPlane.ClosestPointOnPlane(position);
        
        // Look at camera
        transform.rotation = Quaternion.LookRotation(dirToPlane, Vector3.up);

        // Get rotation around world up between cam forward projected to plane and direction
        var projectedCamForward = ClearVectorY(cameraForward);
        var projectedLookDirection = ClearVectorY(lookDirection);
        var rot = Vector3.SignedAngle(projectedCamForward, projectedLookDirection, Vector3.up);
        if (rot < 0)
        {
            rot += 360;
        }

        // Turn rotation into an integer direction
        var dir = (int)(rot * directionNumber / 360);

        // Set plane rotation
        spritePlane.localRotation = Quaternion.Euler(0, 0, rotationOffset - rot);

        // Switch sprite based on direction
        animator.SetInteger(Direction, dir);
        
    }

    private Vector3 ClearVectorY(Vector3 vector3)
    {
        vector3.y = 0;
        return vector3;
    }
}

using UnityEngine;
using UnityEngine.AI;

// NOTE: This is designed to be places on an abject which is at the floor of were the enemy will be standing
// There should be a child object which holds the sprite with its bottom on the floor. This object will be
// rotated to look at the cam, which the child will be rotated and have its sprite changed to represent
// were it is "looking". This script should also be a child of an object which sets its lookDirection each frame.

[RequireComponent(typeof(Animator))]
public class DirectionalSprite : MonoBehaviour
{
    [Header("These should autofill so it should be okay for them to be blank")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform spritePlane;
    [Header("Set this to manually override the direction, set to Vector3.zero to stop overriding")]
    public Vector3 lookDirectionOverride = Vector3.zero;
    [Header("How many degrees off from \"up\" the image is (likely 90 or -90)")]
    [SerializeField] private float rotationOffset;
    private bool breakIt;
    [SerializeField] public bool calculateOnce;
    // Saved values for later
    private float initialYScale;
    private MobNav mobNav;
    private Rigidbody rigid;
    
    // Start is called before the first frame update
    private void Start()
    {
        // If it was not set in the UI, assume the sprite is the first child
        if (spritePlane == null)
        {
            spritePlane = transform.GetChild(0);
        }
        
        // If it was not set in the UI, assume the player's camera is either the main on or the first in the scene
        if (cameraTransform == null)
        {
            var mainCam = Camera.main;
            if (mainCam != null)
            {
                cameraTransform = mainCam.transform;
            }
            else
            {
                cameraTransform = FindObjectOfType<Camera>().transform;
            }
        }

        initialYScale = spritePlane.localScale.y;
        var parent = transform.parent;
        mobNav = parent.GetComponent<MobNav>();
        rigid = parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (breakIt == true) {
            return;
        }
        var transform1 = transform;
        var position = transform1.position;
        var cameraForward = cameraTransform.forward;
        
        // Create camera plane representation
        var cameraPlane = new Plane(-cameraForward, cameraTransform.position);
        var dirToPlane = position - cameraPlane.ClosestPointOnPlane(position);
        
        // Look at camera
        transform1.rotation = Quaternion.LookRotation(dirToPlane, Vector3.up);

        // Get rotation around world up between cam forward projected to plane and direction
        var projectedCamForward = ClearVectorY(cameraForward);
        var lookDirection = GetNewDirection().normalized;
        //Debug.Log(lookDirection);
        var projectedLookDirection = ClearVectorY(lookDirection);
        var rot = Vector3.SignedAngle(projectedCamForward, projectedLookDirection, Vector3.up);
        
        // Correct for negative rotations
        if (rot < 0)
        {
            rot += 360;
        }

        var correctedRot = rotationOffset + rot;

        var scaleY = initialYScale;
        // See if rotation is above halfway and modify values if so
        if (rot > 180)
        {
            rot = -correctedRot;
            scaleY = -scaleY;
            //Debug.Log("Over");
        }
        else
        {
            rot = -correctedRot;
            //Debug.Log("Under");
        }

        // Set plane rotation
        spritePlane.localRotation = Quaternion.Euler(0, 0, rot);
        
        var localScale = spritePlane.localScale;
        spritePlane.localScale = new Vector3(localScale.x, scaleY, localScale.z);
        if (calculateOnce) {
            breakIt = true;
        }
    }

    // Returns the direction this object should be facing
    private Vector3 GetNewDirection()
    {
        if (lookDirectionOverride != Vector3.zero)
        {
            //Debug.Log("First");
            return lookDirectionOverride;
        }
        
        // Temporary
        return FindObjectOfType<Movement>().transform.position - transform.position;
        
        if (mobNav != null)
        {
            Debug.Log("Mob Nav");
            return mobNav.move_offset;
        }

        if (rigid != null)
        {
            Debug.Log("Rigid");
            return rigid.velocity;
        }
        
        return Vector3.zero;
    }

    private static Vector3 ClearVectorY(Vector3 vector3)
    {
        vector3.y = 0;
        return vector3;
    }
}

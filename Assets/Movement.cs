using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movedirection;
    private Rigidbody objRb;
    public float speed;

    void Start()
    {
        objRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movedirection = new Vector3(horizontalInput, 0, verticalInput);
        /* if (Input.w = 1)
        {
            horizontalInput -= 1;
        } */
    }
    void FixedUpdate()
    {
        objRb.velocity = movedirection * speed;
    }
}
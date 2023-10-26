using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketJet : MonoBehaviour
{

    [SerializeField] int verticalFactor;
    [SerializeField] int horizontalFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Works!");
        // Get Triggering Object's RigidBody
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 force = horizontalFactor * this.transform.forward + verticalFactor * (new Vector3(0, 1, 0));
        rb.AddForce(force, ForceMode.Impulse);
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicReset : MonoBehaviour
{

    // -- Private Fields --
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RegionCollider"))
        {
            rb.isKinematic = true;
        }
    }
}

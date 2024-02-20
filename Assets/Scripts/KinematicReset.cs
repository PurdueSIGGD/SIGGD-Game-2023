using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KinematicReset : MonoBehaviour
{

    // -- Private Fields --
    private Rigidbody rb;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Placeable"))
        {
            rb.isKinematic = true;
            agent.enabled = true;
        }
    }

    public void Knockback()
    {
        rb.isKinematic = false;
        agent.enabled = false;
    }

}

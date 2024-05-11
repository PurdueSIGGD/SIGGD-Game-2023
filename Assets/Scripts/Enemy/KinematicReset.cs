using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
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

    public void Stun(float duration)
    {
        rb.isKinematic = false;
        agent.enabled = false;
        StartCoroutine(UnStun(duration));
    }

    IEnumerator UnStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        rb.isKinematic = true;
        agent.enabled = true;
    }
}

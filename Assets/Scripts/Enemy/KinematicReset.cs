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
    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        check = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("YOOOO");
        if (check && collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            rb.isKinematic = true;
            agent.enabled = true;
        }
    }

    public void Knockback()
    {
        rb.isKinematic = false;
        agent.enabled = false;
        check = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        check = true;
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

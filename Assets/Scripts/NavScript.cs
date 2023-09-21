using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public float maxDistance;
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(GetComponent<Transform>().position, player.transform.position - transform.position, out hit, Mathf.Infinity)) {
            if (hit.collider.gameObject == player) {
                agent.SetDestination(player.transform.position);
            }
        }
    }
}

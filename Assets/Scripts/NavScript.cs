using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    public float maxDistance;
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(GetComponent<Transform>().position, player.position - transform.position, out hit, Mathf.Infinity)) {
            if (hit.collider.gameObject.transform == player) {
                agent.SetDestination(player.position);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject otherObject;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(otherObject.transform.position);
    }
}

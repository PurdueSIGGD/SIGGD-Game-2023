using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Transform thisTrans;
    [SerializeField] private float detectRad;
    [SerializeField] private float loseRad;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool aiEnable;
    // Start is called before the first frame update
    void Start()
    {
        aiEnable = false;
    }

    // Update is called once per frame
    void Update()
    {   
        LayerMask mask = 1 << LayerMask.NameToLayer("Player");
        bool inDetRange = false;
        bool outOfRange = true;
        Collider[] detColArr = Physics.OverlapSphere(thisTrans.position, detectRad, mask);
        if (detColArr.Length != 0) {
            inDetRange = true;
        }
        Collider[] loseColArr = Physics.OverlapSphere(thisTrans.position, loseRad, mask);
        if (loseColArr.Length != 0) {
            outOfRange = false;
        }
        if (outOfRange) {
            aiEnable = false;
        }
        else if (inDetRange) {
            aiEnable = true;
        }
        if (aiEnable) {
            agent.enabled = true;
            agent.SetDestination(target.position);
        }
        else {
            agent.enabled = false;
        }
    }
}

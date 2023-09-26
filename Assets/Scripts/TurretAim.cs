using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretAim : MonoBehaviour
{
    private List<Collider> triggerList = new List<Collider>();
    Vector3 initialPos;
    [SerializeField] float lerpSpeed = 5.0f;

    void Start() {
        initialPos = transform.position;
    }
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Enemy" && !triggerList.Contains(collider)) {
            //Debug.Log(collider.gameObject.tag);
            triggerList.Add(collider);
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (triggerList.Contains(collider)) {
            triggerList.Remove(collider);
            //Debug.Log("se fue xd");
        }
    }

    void Update() {
        Collider target = null;
        float smallerDist = float.PositiveInfinity;
        if(triggerList.Count > 0) {
            foreach(Collider collider in triggerList) {
                float dist = (transform.position - collider.transform.position).sqrMagnitude;
                if(dist < smallerDist) {
                    target = collider;
                    smallerDist = dist;
                }
            }
        }
        
        Vector3 lookPos = initialPos;
        if(target != null) {
            lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
        }

        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lerpSpeed);
    }
}

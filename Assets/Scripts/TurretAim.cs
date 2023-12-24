using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretAim : MonoBehaviour
{
    private List<Collider> triggerList = new List<Collider>();
    Quaternion initialRot;
    [SerializeField] float lerpSpeed = 5.0f;
    [SerializeField] private GameObject bullet;

    void Start() {
        initialRot = transform.rotation;
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

    private void shootBullet() {
        GameObject curBullet = Instantiate(bullet, transform.position, transform.rotation);
        //curBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 100.0f);
        Physics.IgnoreCollision(curBullet.GetComponent<Collider>(), GetComponent<Collider>());
        Destroy(curBullet, 5.0f);
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
        
        Quaternion rotation = initialRot;
        
        if(target != null) {
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lerpSpeed);

        if(Input.GetButtonDown("Enable Debug Button 2")) {
            shootBullet();
        }
    }
}

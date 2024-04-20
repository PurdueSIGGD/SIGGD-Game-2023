using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMechanics : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float DAMAGE;
    [SerializeField] 
    private float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        birthTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (birthTime + lifeTime < Time.time) {
            Destroy(this.gameObject);
        }


    }

    private void OnCollisionEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            birthTime = Time.time;
        }

        Destroy(this.gameObject);
    }


}

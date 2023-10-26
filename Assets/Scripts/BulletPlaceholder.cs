using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlaceholder : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("called");
        if (collider.gameObject.tag == "RegionColliders") {
            gameObject.GetComponent<MeshRenderer>().material = collider.gameObject.GetComponent<MeshRenderer>().material;
        }
    }
}

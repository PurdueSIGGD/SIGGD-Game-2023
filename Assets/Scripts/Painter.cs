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

    private void OnTriggerColliderEnter(Collider collider) {
        if (collider.gameObject.tag == "RegionColliders") {
            gameObject.AddComponent<collider.gameObject.GetComponent<Renderer>().material>();
        }
    }
}

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    int capactity = 10000;
    int charge = 0;
    int increment_val = 1;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (charge < capactity) {
            charge += increment_val;
            if (charge > capactity) {
                charge = capactity;
            }
        }
        // if (charge % 100 == 0) {
        //     UnityEngine.Debug.Log("Charge: " + charge);
        // }
    }
}

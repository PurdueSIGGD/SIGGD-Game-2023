using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour, IWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PerformAttack(int attack) {
        // Call animation stuff, manipulate hitboxes, manipulate health values, etc.

        if (attack == 1) {
            Debug.Log("Primary attack triggered on knife");
        } else if (attack == 2) {
            Debug.Log("Secondary attack triggered on knife");
        }

        return true;
    }
}

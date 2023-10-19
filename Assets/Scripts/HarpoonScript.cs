using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour, IWeapon
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
        
        if (attack == 1) {
            Debug.Log("Primary attack triggered on harpoon");
        } else if (attack == 2) {
            Debug.Log("Secondary attack triggered on harpoon");
        }

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour, IWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetPositionAndRotation(new Vector3(-1.14f, .88f, .62f), Quaternion.Euler(-5.7f, -2.5f, 79));
        this.SetEnabled(false);
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

    public void SetEnabled(bool enabled) {
        Component[] comps = GetComponents<Component>();
        foreach (Component c in comps) {
            Debug.Log("Behavior for " + this.gameObject.name + ": " + c);
            if (c is not IWeapon) {
                // ((Behaviour) c).enabled = enabled;
            }
        }
    }
}

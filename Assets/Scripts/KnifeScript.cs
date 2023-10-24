using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour, IWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetPositionAndRotation(new Vector3(-.58f, .51f, .61f), Quaternion.Euler(0, 0, -73));
        this.SetEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PerformAttack(int attack) {

        if (attack == 1) {
            Debug.Log("Primary attack triggered on knife");
        } else if (attack == 2) {
            Debug.Log("Secondary attack triggered on knife");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonScript : MonoBehaviour, IWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetLocalPositionAndRotation(new Vector3(-.68f, .37f, -.39f),
                                                    Quaternion.Euler(-91, -177.3f, 171.4f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool PerformAttack(int attack) {
        return true;
    }

    public void SetEnabled(bool enabled) {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null) {
            mr.enabled = enabled;
        }
        Collider[] cList = GetComponents<Collider>();
        foreach (Collider c in cList) {
            c.enabled = enabled;
        }
    }
}

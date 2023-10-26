using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour, IWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetLocalPositionAndRotation(new Vector3(-.59f, -.01f, -.43f),
                                                    Quaternion.Euler(-70.5f, -8.1f, -16.9f));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    // -- Serialize Fields --

    [SerializeField]
    Landmine mine;

    // -- Behavior --
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            mine.Detonate();
        }
    }
}

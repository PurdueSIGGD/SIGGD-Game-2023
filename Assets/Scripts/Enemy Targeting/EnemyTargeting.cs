using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTargeting : MonoBehaviour
{
    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

    IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
               
        }
    }
}
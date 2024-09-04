using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileKillCheck : MonoBehaviour
{

    [SerializeField] float projectileLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

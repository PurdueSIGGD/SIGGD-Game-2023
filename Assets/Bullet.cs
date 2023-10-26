using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int life = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, life);
    }
}

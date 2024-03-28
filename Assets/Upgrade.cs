using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradedTurret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This doesn't do the job of checking if the player can afford the turret, that should be done elsewhere
    public void ReplaceTurret() {
        // Destroy game object the script is attached to and replace with upgraded turret prefab
        Vector3 pos = gameObject.transform.position;
        Destroy(gameObject);
        Instantiate(upgradedTurret, pos, Quaternion.identity);
    }
}

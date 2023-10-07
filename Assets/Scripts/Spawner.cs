using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy; 

    [SerializeField]
    public float minDistance = 5.0f;
    [SerializeField]
    public float maxDistance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 50; i++) {
            // Spawn object
            UnityEngine.Vector2 randomPoint = generatePointInRing();
            UnityEngine.Vector3 randomPoint3D = new UnityEngine.Vector3(randomPoint.x, player.transform.position.y, randomPoint.y);
            GameObject newEnemy = Instantiate(enemy, randomPoint3D, UnityEngine.Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private UnityEngine.Vector2 generatePointInRing() {
        UnityEngine.Vector2 dir = Random.insideUnitCircle.normalized;
        float dist = Random.Range(minDistance, maxDistance);
        return dir * dist;
    }
}

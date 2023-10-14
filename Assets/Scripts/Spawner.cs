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

    [SerializeField]
    LayerMask regionLayer;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 50; i++) {
            // Spawn object
            Vector2 randomPoint = generatePointInRing();
            Vector3 randomPoint3D = new Vector3(randomPoint.x, player.transform.position.y, randomPoint.y);
            GameObject newEnemy = Instantiate(enemy, randomPoint3D, Quaternion.identity);
            // Set the color of the enemy to the color of the region that we spawned in
            Collider[] collArray = Physics.OverlapSphere(randomPoint3D, 1.0f, regionLayer, QueryTriggerInteraction.Collide);
            for (int j = 0; j < collArray.Length; j++) {
                 if (collArray[i].gameObject.tag == "RegionColliders") {
                     gameObject.GetComponent<MeshRenderer>().material = collArray[i].gameObject.GetComponent<MeshRenderer>().material;
                 }
            }
            randomPoint = generatePointInRing();
            randomPoint3D = new Vector3(randomPoint.x, player.transform.position.y, randomPoint.y);
            newEnemy.transform.position = randomPoint3D;
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

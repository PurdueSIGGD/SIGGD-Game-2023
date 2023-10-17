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
            Vector2 randomPoint = generatePointInRing();
            Vector3 randomPoint3D = new Vector3(randomPoint.x, 0.0f, randomPoint.y) +  player.transform.position;

            // Get the color of the region our random point is in
            Material regionMaterial = null;
            Collider[] collArray = Physics.OverlapSphere(randomPoint3D, 1.0f);
            Debug.Log(collArray.Length);
            for (int j = 0; j < collArray.Length; j++) {
                if (collArray[j].gameObject.tag == "RegionColliders") {
                    regionMaterial = collArray[j].gameObject.GetComponent<MeshRenderer>().material;
                }
            }

            // Generate a new point to spawn the enemy in with the color we found
            randomPoint = generatePointInRing();
            randomPoint3D = new Vector3(randomPoint.x, 0.0f, randomPoint.y) +  player.transform.position;
            GameObject newEnemy = Instantiate(enemy, randomPoint3D, Quaternion.identity);
            newEnemy.GetComponent<MeshRenderer>().material = regionMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 generatePointInRing() {
        Vector2 dir = Random.insideUnitCircle.normalized;
        float dist = Random.Range(minDistance, maxDistance);
        return dir * dist;
    }
}

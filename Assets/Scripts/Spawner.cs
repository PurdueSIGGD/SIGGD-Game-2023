using System.Collections;
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
        InvokeRepeating("SpawnEnemy", 0, 0.1f);
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Optional spawning in a cone for waves
    private Vector2 generatePointInRing(int degrees = -1, int spread = -1) {
        Vector2 dir = Random.insideUnitCircle.normalized;
        if (degrees != -1) {
            float randomRads= Mathf.Deg2Rad * Random.Range(degrees - spread, degrees + spread);
            dir = new Vector2(1, Mathf.Tan(randomRads)).normalized;
        }
        float dist = Random.Range(minDistance, maxDistance);
        return dir * dist;
    }

    // Optional spawning in a cone for waves
    private void SpawnEnemy(int degrees = -1, int spread = -1) {
        // Spawn object
        Vector2 randomPoint = generatePointInRing();
        Vector3 randomPoint3D = new Vector3(randomPoint.x, 0.0f, randomPoint.y) +  player.transform.position;

        // Get the color of the region our random point is in
        Material regionMaterial = null;
        Collider[] collArray = Physics.OverlapSphere(randomPoint3D, 1.0f);
        for (int j = 0; j < collArray.Length; j++) {
            if (collArray[j].gameObject.tag == "RegionColliders") {
                regionMaterial = collArray[j].gameObject.GetComponent<MeshRenderer>().material;
            }
        }

        // Generate a new point to spawn the enemy in with the color we found
        randomPoint = generatePointInRing(degrees, spread);
        randomPoint3D = new Vector3(randomPoint.x, 0.0f, randomPoint.y) +  player.transform.position;
        GameObject newEnemy = Instantiate(enemy, randomPoint3D, Quaternion.identity);
        newEnemy.GetComponent<MeshRenderer>().material = regionMaterial;
    }

    IEnumerator SpawnWave() {
        // Randomize spread and direction for wave
        int degrees = Random.Range(0, 360);
        // Deviation from degrees in one direction (2 * spread is the whole arc)
        int spread = Random.Range(45, 90);
        Debug.Log("degrees: " + degrees);
        Debug.Log("spread: " + spread);
        int totalSpawns = 0;
        // Larger numbers make the wave have more enemy volume
        int waveVolume = 15;
        while (totalSpawns / waveVolume < Mathf.PI) {
            SpawnEnemy(degrees, spread);
            totalSpawns++;
            yield return new WaitForSeconds(1.25f - Mathf.Sin((float)totalSpawns / (float)waveVolume));
        }
    }
}

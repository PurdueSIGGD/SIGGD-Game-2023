using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy; 

    [SerializeField]
    public float minDistance = 5.0f;
    [SerializeField]
    public float maxDistance = 10.0f;

    float constantSpawnTimer = 0.0f;
    float constantSpawnInterval = 3.0f; //seconds
    float waveSpawnTimer = 0.0f;
    float waveSpawnInterval = 45.0f; //seconds

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        // Theres definitely a better way to constant spawn
        // I tried invoke repeating but that breaks with parameters
        constantSpawnTimer += Time.deltaTime;
        waveSpawnTimer += Time.deltaTime;
        if (constantSpawnTimer > constantSpawnInterval) {
            constantSpawnTimer -= constantSpawnInterval;
            SpawnEnemy();
        }
        if (waveSpawnTimer > waveSpawnInterval) {
            waveSpawnTimer -= waveSpawnInterval;
            StartCoroutine(SpawnWave());
        }
    }

    // Optional spawning in a cone for waves
    private Vector2 generatePointInRing(int degrees = -1, int spread = -1) {
        Vector2 dir = Random.insideUnitCircle.normalized;
        if (degrees != -1) {
            int randomDegrees = degrees + Random.Range(-spread, spread);
            dir = new Vector2(Mathf.Sin(Mathf.Deg2Rad * randomDegrees), Mathf.Cos(Mathf.Deg2Rad* randomDegrees));
        }
        float dist = Random.Range(minDistance, maxDistance);
        return dir * dist;
    }

    // Optional spawning in a cone for waves
    void SpawnEnemy(int degrees = -1, int spread = -1) {
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
        Debug.Log("Degrees: " + degrees);
        Debug.Log("Spread: " + spread);
        int currentTotalSpawns = 0;
        // Larger numbers make the wave have more enemy volume
        int waveVolume = 10;
        // As this approaches 1, the wave spawns much faster
        // Don't make it less than 1 or it'll probably break since we wait for speed - sin(x) seconds
        float speed = 3;
        while (currentTotalSpawns / waveVolume < Mathf.PI) {
            SpawnEnemy(degrees, spread);
            currentTotalSpawns++;
            yield return new WaitForSeconds(speed - Mathf.Sin((float)currentTotalSpawns / (float)waveVolume));
        }
    }
}

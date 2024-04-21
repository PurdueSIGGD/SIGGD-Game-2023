using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CustomEditor(typeof(BoxPrefabFill))]
internal class BoxPrefabFillEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
            
        var boxPrefabFill = (BoxPrefabFill)target;
            
        if (GUILayout.Button("Place Prefabs"))
        {
            //boxPrefabFill.ClearChildren();
            boxPrefabFill.PlacePrefabs();
            boxPrefabFill.GetComponent<MeshRenderer>().enabled = false;
            boxPrefabFill.GetComponent<Collider>().enabled = false;
        }
        GUILayout.Label("To clear prefabs delete them from the scene");
        
        if (GUILayout.Button("Un-hide this object"))
        {
            //boxPrefabFill.ClearChildren();
            boxPrefabFill.GetComponent<MeshRenderer>().enabled = true;
            boxPrefabFill.GetComponent<Collider>().enabled = true;
        }
        
    }
}

// The goal of this script is to fill a box with prefabs to make designing levels easier
public class BoxPrefabFill : MonoBehaviour
{
    [Header("This should be an empty with default values")] [SerializeField]
    private Transform prefabHolder;
    [Header("Put prefabs from largest to smallest")] [SerializeField]
    private GameObject[] bigPrefabsToUse;
    [SerializeField]
    private GameObject[] smallPrefabsToUse;
    [SerializeField] private float unitsPerPrefab = 4;
    [SerializeField] private Vector3 prefabScale = Vector3.one;
    [Header("Which ratio of the area should be the large")] [SerializeField]
    private float bigRatio = 1;
    [Header("Which ratio of the area should be the small")] [SerializeField]
    private float smallRatio = 0.5f;
    [Header("This increases prefab density on edge of big and small")]
    [Header("~0.9 recommended, avoid 0 and 1")][SerializeField]
    private float falloff = 0.9f;
    [FormerlySerializedAs("smallRockDensityIncrease")] [SerializeField]
    private float smallDensityIncrease = 4f;
    [SerializeField] private int seed = 0;
    
    // Start is called before the first frame update
    public void PlacePrefabs()
    {
        // Set the seed so that that the randomness is controllable
        Random.InitState(seed);

        var bigDistance = bigRatio / 2;
        var smallDistance = smallRatio / 2;
        
        // Decide how many prefabs to place
        var gridSize = Vector2.Scale(new Vector2(transform.localScale.x, transform.localScale.z), new Vector2(1/unitsPerPrefab, 1/unitsPerPrefab));

        var combinedDistance = bigDistance + smallDistance;
        
        // Loop over local space and place prefabs based on how close they are to the center
        for (var i = -combinedDistance; i < combinedDistance; i += GetDelta(i, falloff, bigDistance, combinedDistance, smallDensityIncrease) / gridSize.x)
        {
            for (var j = -combinedDistance; j < combinedDistance; j += GetDelta(j, falloff, bigDistance, combinedDistance, smallDensityIncrease) / gridSize.y)
            {
                // Randomize the position
                var x = i + Random.Range(-combinedDistance, combinedDistance) / gridSize.x;
                var y = j + Random.Range(-combinedDistance, combinedDistance) / gridSize.y;
                
                // 0 to 1 representation of how where the position is (max Manhattan distance)
                var maxSize = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                var bigDist = maxSize / bigDistance;
                var smallDist = (maxSize - bigDistance) / smallDistance;
                var totalDist = maxSize / combinedDistance;
                
                // Get the position
                var worldPos = transform.TransformPoint(new Vector3(x, 0, y));
                
                // Spawn the prefab
                GameObject toSpawn = null;
                if (bigDist <= 1)
                {
                    // randomize the distance as well for more variation
                    bigDist += Random.Range(-bigDistance, bigDistance) / 2f;
                    // Spawn big rocks
                    toSpawn = bigPrefabsToUse[(int)Mathf.Clamp(bigDist * bigPrefabsToUse.Length, 0, bigPrefabsToUse.Length - 1)];
                }
                else
                {
                    // randomize the distance as well for more variation
                    smallDist += Random.Range(-smallDist, smallDist) / 2f;
                    // Spawn little rocks
                    toSpawn = smallPrefabsToUse[(int)Mathf.Clamp(smallDist * smallPrefabsToUse.Length, 0, smallPrefabsToUse.Length - 1)];
                }
                var g = Instantiate(toSpawn, worldPos, Quaternion.Euler(0, Random.value * 360, 0), prefabHolder);
                var distScaler = (1 - totalDist / 2);
                g.transform.localScale = Vector3.Scale(prefabScale, new Vector3(1,distScaler,1))*distScaler;

            }
        }
    }

    // https://www.desmos.com/calculator/m2tuhl7nqt
    private static float GetDelta(float x, float b, float bigDistance, float combinedDistance, float smallDensityIncrease)
    {
        x = Mathf.Abs(x);
        var modifiedX = x / combinedDistance;
        var inverseB = 1 / b;
        var fx = Mathf.Pow(2 * modifiedX - 1, 2);
        var gx = (fx + inverseB - 1) / inverseB;
        return x > bigDistance ? gx / smallDensityIncrease : gx;
    }
}

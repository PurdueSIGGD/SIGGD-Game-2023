using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CustomEditor(typeof(BoxPrefabFill))]
internal class BoxPrefabFillEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
            
        var boxPrefabFill = (BoxPrefabFill)target;
            
        if (GUILayout.Button("Place Prefabs"))
        {
            boxPrefabFill.ClearChildren();
            boxPrefabFill.PlacePrefabs();
            boxPrefabFill.GetComponent<MeshRenderer>().enabled = false;
            boxPrefabFill.GetComponent<Collider>().enabled = false;
        }
        
        if (GUILayout.Button("Clear Prefabs"))
        {
            boxPrefabFill.ClearChildren();
            boxPrefabFill.GetComponent<MeshRenderer>().enabled = true;
            boxPrefabFill.GetComponent<Collider>().enabled = true;
        }
    }
}

// The goal of this script is to fill a box with prefabs to make designing levels easier
public class BoxPrefabFill : MonoBehaviour
{
    [Header("Put prefabs from smallest to largest")] [SerializeField]
    private GameObject[] prefabsToUse;
    [SerializeField] private float unitsPerPrefab = 4;
    [SerializeField] private Vector3 prefabScale = Vector3.one;
    [SerializeField] private int seed = 0;

    public void ClearChildren()
    {
        // Clear children
        for(var i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    
    // Start is called before the first frame update
    public void PlacePrefabs()
    {
        // Set the seed so that that the randomness is controllable
        Random.InitState(seed);
        
        // Decide how many prefabs to place
        var gridSize = Vector2.Scale(new Vector2(transform.localScale.x, transform.localScale.z), new Vector2(1/unitsPerPrefab, 1/unitsPerPrefab));

        // Loop over local space and place prefabs based on how close they are to the center
        for (var i = -0.5f; i < 0.5f; i += (1 - 1.5f*Mathf.Abs(i)) / gridSize.x)
        {
            for (var j = -0.5f; j < 0.5f; j += (1 - 1.5f*Mathf.Abs(j)) / gridSize.y)
            {
                // Randomize the position
                var x = i + Random.Range(-0.5f, 0.5f) / gridSize.x;
                var y = j + Random.Range(-0.5f, 0.5f) / gridSize.y;
                
                // 0 to 1 representation of how far from the edge the position is (max Manhattan distance)
                var dist = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)) * 2;
                
                // randomize the distance as well for more variation
                dist -= Random.value * 0.25f;
                
                // Get the position
                var worldPos = transform.TransformPoint(new Vector3(x, 0, y));
                
                // Spawn the prefab
                var toSpawn = prefabsToUse[(int)Mathf.Clamp(dist * prefabsToUse.Length, 0, prefabsToUse.Length - 1)];
                var g = Instantiate(toSpawn, worldPos, Quaternion.Euler(0, Random.value * 360, 0), transform);
                var distScaler = (1 - dist / 2);
                g.transform.localScale = Vector3.Scale(prefabScale, new Vector3(1/transform.localScale.x,distScaler/transform.localScale.y,1/transform.localScale.z))*distScaler;
            }
        }
    }
}

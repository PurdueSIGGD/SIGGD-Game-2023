using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CustomEditor(typeof(RockReplacer)), CanEditMultipleObjects]
internal class RockReplacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var replacer = (RockReplacer)target;

        if (GUILayout.Button("Replace rocks"))
        {
            replacer.replaceRocks("HugeRock(Clone)");
        }

    }
}

public class RockReplacer : MonoBehaviour
{
    [SerializeField] private GameObject rockToUse;

    public void replaceRocks(string n)
    {
        var objs = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == n);
        foreach (var obj in objs)
        {
            var g = Instantiate(rockToUse, obj.transform.position, obj.transform.rotation);
            g.transform.localScale = new Vector3(3, 2, 3);
            DestroyImmediate(obj);
        }
    }
}

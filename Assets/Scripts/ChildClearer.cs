using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChildClearer))]
internal class ChildClearerEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
            
        var childClearer = (ChildClearer)target;
            
        if (GUILayout.Button("Clear Children"))
        {
            childClearer.ClearChildren();
        }
    }
}

public class ChildClearer : MonoBehaviour
{
    public void ClearChildren()
    {
        // Clear children
        for(var i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}

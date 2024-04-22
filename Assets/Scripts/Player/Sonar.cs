using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private float totalPingTime = 2f;
    private bool pinging;
    private float pingTimer = 0;
    [SerializeField] private float pingFadeInSpeed;
    [SerializeField] private float pingFadeOutSpeed;
    private float pingFade = 0;
    private List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private Transform pylonRingParent;
    [SerializeField] private Transform artifactRingParent;

    public void StartPing()
    {
        pinging = true;
        var pylons = FindObjectsOfType<ChargePylon>();
        pylons = GetInRange(pylons);
        
        var artifacts = FindObjectsOfType<Artifact>();
        artifacts = GetInRange(artifacts);

        ChildClearer.ClearTransformChildren(pylonRingParent);

        foreach (var pylon in pylons)
        {
            var dist = Vector3.Distance(transform.position, pylon.transform.position);
            var g = Instantiate(ringPrefab, pylonRingParent);
            g.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(1000 * (1 - dist / range), 0), 1000);
        }
        
        ChildClearer.ClearTransformChildren(artifactRingParent);
        
        foreach (var artifact in artifacts)
        {
            var dist = Vector3.Distance(transform.position, artifact.transform.position);
            var g = Instantiate(ringPrefab, artifactRingParent);
            g.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(1000 * (1 - dist / range), 0), 1000);
        }
    }

    private T[] GetInRange<T>(T[] array)
    {
        var list = new List<T>(array);
        foreach (var val in array)
        {
            if (Vector3.Distance(((val as Component)!).transform.position, transform.position) > range)
            {
                list.Remove(val);
            }
        }

        return list.ToArray();
    }

    private void Update()
    {
        // Change the ping fade
        pingFade = Mathf.Clamp(pingFade + Time.deltaTime * (pinging ? pingFadeInSpeed : -pingFadeOutSpeed), 0, 1);
        
        // Calculate the ping values
        if (pinging)
        {
            pingTimer += Time.deltaTime;

            if (pingTimer > totalPingTime)
            {
                pinging = false;
                pingTimer = 0;
            }
        }
        
        // Show the ping data in the UI
    }
}

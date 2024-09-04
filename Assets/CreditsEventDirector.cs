using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEventDirector : MonoBehaviour
{

    private string creditsSender = "Purdue SIGGD 2023-2024";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(creditSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator creditSequence()
    {
        yield return null;
    }
}

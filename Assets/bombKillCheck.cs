using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombKillCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void killBomb(float delay)
    {

    }

    private IEnumerator killBombCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Object.Destroy(gameObject);
    }
}

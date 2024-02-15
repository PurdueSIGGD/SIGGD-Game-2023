using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Torpedo : MonoBehaviour
{
    // -- Private Fields --
    float duration;
    float height;
    GameObject target;
    Vector3 startPos;
    Vector3 endPos;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize fields
        endPos = (target.transform.position - this.transform.position) / 2 + this.transform.position;
        endPos.y = height;
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.Pow(time / duration, 2);

        Vector3 pos = Vector3.Lerp(startPos, endPos, t);
        pos.y = Func(t, height);

        this.transform.rotation = Quaternion.LookRotation(pos - this.transform.position);

        this.transform.position = pos;
    }

    // -- Helper Functions --

    float Func(float x, float scale)
    {
        float y = -4 * Mathf.Pow((x - 0.5f), 2) + 1;
        return scale * y;
    }

    // -- Instance Methods --
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public void SetHeight(float height)
    {
        this.height = height;
    }
}

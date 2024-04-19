using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Torpedo : MonoBehaviour
{
    // -- Serialize Fields --

    [SerializeField]
    float radius;

    [SerializeField]
    float damage;

    [SerializeField]
    float knockback;

    [SerializeField]
    LayerMask enemyMask;

    // -- Private Fields --
    float duration;
    float height;
    GameObject target;
    Vector3 startPos;
    Vector3 endPos;
    float time;
    bool canBoom;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize fields
        endPos = target.transform.position;
        startPos = this.transform.position;
        canBoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.Pow(time / duration, 2);

        if (target != null)
        {
            endPos = target.transform.position;
        }
        Vector3 pos = Vector3.Lerp(startPos, endPos, t);
        pos.y = Func(t, height) + startPos.y;

        this.transform.rotation = Quaternion.LookRotation(pos - this.transform.position);

        this.transform.position = pos;

        // Set Boom On
        if (t > 0.5f)
        {
            canBoom = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBoom)
        {
            Debug.Log("BOOM!");
            Debug.Log(other.gameObject.name);
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius, enemyMask);
            Debug.Log(colliders.Length);
            foreach (Collider enemy in colliders)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 explosionPoint = this.transform.position - Vector3.up;
                explosionPoint.y = 0;
                enemy.GetComponent<KinematicReset>().Knockback();
                enemy.GetComponent<Rigidbody>().AddExplosionForce(knockback, explosionPoint, radius, knockback / 3);
                enemy.GetComponent<HealthPoints>().damageEntity(damage);
            }
            Destroy(this.gameObject);
        }
    }

    // -- Helper Functions --

    float Func(float time, float scale)
    {
        float y = -4 * Mathf.Pow((time - 0.5f), 2) + 1;
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
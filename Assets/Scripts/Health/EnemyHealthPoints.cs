using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthPoints : HealthPoints
{
    // Start is called before the first frame update


    public override void kill() {
        Debug.Log("die");
        Destroy(this.transform.parent.gameObject);
    }
}

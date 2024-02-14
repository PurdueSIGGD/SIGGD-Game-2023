using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    private GameObject gameObject;

    public abstract int GetPriority();

    public abstract IEnumerator EnterState();
    public abstract void ExitState();    
}
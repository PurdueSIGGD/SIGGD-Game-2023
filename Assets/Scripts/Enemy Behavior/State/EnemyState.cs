using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyState
{
    public abstract void StartState();
    public abstract void StopState();
    public abstract void UpdateState();

    private GameObject gameObject;
}
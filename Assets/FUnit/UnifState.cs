using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnifState
{
    public abstract void EnterState(Unif context);
    public abstract void UpdateState(Unif context);

}

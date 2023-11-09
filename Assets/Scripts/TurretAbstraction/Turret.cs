using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : Unit
{
    protected abstract void Create();

    protected abstract void Destroy();
}

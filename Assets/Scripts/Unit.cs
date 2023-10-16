using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //What will the unit do on spawn?
    protected abstract void Create();

    //What will the unit do on end
    protected abstract void End();
}

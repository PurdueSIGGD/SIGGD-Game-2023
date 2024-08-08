using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerPulse : UnitState
{
    public override void EnterState(Unit context)
    {
        HealerFSM healerContext = (HealerFSM)context;
        healerContext.player.GetComponent<HealthPoints>().healEntity(healerContext.healAmount);
        healerContext.player.GetComponent<LightResource>().addLight(healerContext.lightAmount);
    }

    public override void UpdateState(Unit context)
    {
        return;
    }
}

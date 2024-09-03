using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RocketeerIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;
        rocketeerContext.fireTime = rocketeerContext.fireCooldown;
        rocketeerContext.animator.ResetTrigger("Fire");
    }

    public override void UpdateState(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;
        GameObject unit = context.gameObject;

        // look at target
        if (rocketeerContext.target != null)
        {
            var dir = rocketeerContext.target.transform.position - unit.transform.position;
            rocketeerContext.gunObj.GetComponent<DirectionalSprite>().lookDirectionOverride = dir;
        }

        if (FindTargets(context) && rocketeerContext.fireTime == 0)
        {
            // switch to fire state
            rocketeerContext.SwitchState(rocketeerContext.fireState);
        }
    }
    
    // Private Methods
    private bool FindTargets(Unit context)
    {
        RocketeerFSM rocketeerContext = (RocketeerFSM)context;
        GameObject unit = context.gameObject;
        float range = rocketeerContext.range;
        LayerMask projMask = rocketeerContext.projMask;

        // get objects in range
        Collider[] hits = Physics.OverlapSphere(unit.transform.position, range, projMask);

        // if no hits, return
        if (hits.Length == 0)
        {
            rocketeerContext.target = null;
            return false;
        }

        // order targets by distance
        GameObject[] ordered = new GameObject[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            float minDist = float.PositiveInfinity;
            GameObject closest = null;
            int minIdx = -1;

            for (int c = 0; c < hits.Length; c++)
            {
                Collider hit = hits[c];
                if (hit == null) continue;
                float dist = Mathf.Abs((hit.gameObject.transform.position - unit.transform.position).magnitude);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = hit.gameObject;
                    minIdx = c;
                }
            }

            hits[minIdx] = null;
            ordered[i] = closest;
        }

        rocketeerContext.target = ordered[0];
        return true;
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }
}

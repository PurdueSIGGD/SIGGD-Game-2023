using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GunnerIdle : UnitState
{
    public override void EnterState(Unit context)
    {
        GunnerFSM gunner = (GunnerFSM)context;
        gunner.config.animator.ResetTrigger("Fire");
        gunner.personal.reloaded = false;
        context.StartCoroutine(Cooldown(context, gunner.config.fireCooldown));
    }

    public override void OnTriggerEnter(Unit context, Collider other)
    {
        return;
    }

    public override void UpdateState(Unit context)
    {
        GunnerFSM gunner = (GunnerFSM)context;
        GameObject unit = context.gameObject;

        // look at target
        if (gunner.personal.target != null)
        {
            var dir = gunner.personal.target.transform.position - unit.transform.position;
            gunner.config.gunObj.GetComponent<DirectionalSprite>().lookDirectionOverride = dir;
        }

        if (FindTargets(context) && gunner.personal.reloaded)
        {
            // switch to fire state
            gunner.SwitchState(GunnerFSM.fireState);
        }

    }

    // Private Methods
    private bool FindTargets(Unit context)
    {
        GunnerFSM gunner = (GunnerFSM)context;
        GameObject unit = context.gameObject;
        float range = gunner.config.range;
        LayerMask projMask = gunner.config.projMask;

        // get objects in range
        Collider[] hits = Physics.OverlapSphere(unit.transform.position, range, projMask);

        // if no hits, return
        if (hits.Length == 0)
        {
            gunner.personal.target = null;
            return false;
        }

        float minDist = float.PositiveInfinity;
        GameObject closest = null;

        for (int c = 0; c < hits.Length; c++)
        {
            Collider hit = hits[c];
            float dist = Mathf.Abs((hit.gameObject.transform.position - unit.transform.position).magnitude);
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit.gameObject;
            }
        }

        gunner.personal.target = closest;
        return true;
    }

    IEnumerator Cooldown(Unit context, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        GunnerFSM gunner = (GunnerFSM)context;
        gunner.personal.reloaded = true;
    }
}

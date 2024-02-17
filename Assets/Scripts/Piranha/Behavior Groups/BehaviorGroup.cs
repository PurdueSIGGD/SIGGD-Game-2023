using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorGroup
{
    List<BehaviorGroup> subGroups;
    List<Behavior> subBehaviors;

    private abstract bool evaluatePreConditions();

    public virtual void getAvailableBehaviors(ref List<Behavior> behaviorList)
    {
        if (!evaluatePreConditions())
        {
            return;
        }

        // Append child behaviors
        foreach (Behavior subBehavior in subBehaviors)
        {
            if (!subBehavior.evaluatePreConditions())
            {
                continue;
            }

            behaviorList.Add(subBehavior);
        }

        // Append child groups
        foreach (BehaviorGroup subGroup in subGroups)
        {
            subGroup.getAvailableBehaviors(ref behaviorList);
        }

    }
}
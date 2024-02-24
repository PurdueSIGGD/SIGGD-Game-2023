using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorGroup
{
    List<BehaviorGroup> subGroups;
    List<Behavior> subBehaviors;

    protected abstract bool EvaluatePreConditions();

    public virtual void GetAvailableBehaviors(List<Behavior> behaviorList)
    {
        if (!EvaluatePreConditions())
        {
            return;
        }

        // Append child behaviors
        foreach (Behavior subBehavior in subBehaviors)
        {
            if (subBehavior.InitializeAndGetPriority() == -1)
            {
                continue;
            }

            behaviorList.Add(subBehavior);
        }

        // Append child behaviors from child groups
        foreach (BehaviorGroup subGroup in subGroups)
        {
            subGroup.GetAvailableBehaviors(behaviorList);
        }

    }
}
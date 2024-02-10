using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInvert : BTDecoratorNode
{
    public BTInvert(BTCompositeNode parent) : base(parent) { }
    public BTInvert(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        (BTResult result, string action) = child.Evaluate(gameObject, blackboard);

        if (result == BTResult.Running) return (result, action);

        switch (result)
        {
            case BTResult.Success:
                result = BTResult.Failure;
                break;
            case BTResult.Failure:
                result = BTResult.Success;
                break;
        }

        return (result, null);
    }
}
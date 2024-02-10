using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTCompositeNode
{
    public BTSelector(BTCompositeNode parent) : base(parent) { }
    public BTSelector(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        for (int i = 0; i < children.Count; i++)
        {
            BTNode child = children[i];
            (BTResult result, string action) = child.Evaluate(gameObject, blackboard);

            switch (result)
            {
                case BTResult.Success:
                    return (result, null);
                    break;
                case BTResult.Running:
                    return (result, action);
                    break;
            }
        }
        return (BTResult.Failure, null);
    }
}
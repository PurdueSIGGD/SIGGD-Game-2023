using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTCompositeNode
{
    public BTSequence(BTCompositeNode parent) : base(parent) { }
    public BTSequence(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        for (int i = 0; i < children.Count; i++)
        {
            BTNode child = children[i];
            (BTResult result, string action) = child.Evaluate(gameObject, blackboard);

            switch (result)
            {
                case BTResult.Failure:
                    return (result, null);
                    break;
                case BTResult.Running:
                    return (result, action);
                    break;
            }
        }
        return (BTResult.Success, null);
    }
}
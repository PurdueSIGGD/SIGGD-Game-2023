using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTCompositeNode
{
    public BTSequence(BTCompositeNode parent) : base(parent) { }
    public BTSequence(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        for (int i = 0; i < children.Count; i++)
        {
            BTNode child = children[i];
            (BTResult result, BTLeafNode activeNode) = child.Evaluate();
            this.activeNode = activeNode;

            switch (result)
            {
                case BTResult.Failure:
                    return (result, activeNode);
                    break;
                case BTResult.Running:
                    return (result, activeNode);
                    break;
            }
        }
        return (BTResult.Success, null);
    }
}
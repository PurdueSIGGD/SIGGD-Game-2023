using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInvert : BTDecoratorNode
{
    public BTInvert(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.child = null;
    }

    public BTInvert(BTNode parent, BTNode child)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.child = child;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        (BTResult result, BTLeafNode activeNode) = child.Evaluate();
        this.activeNode = activeNode;

        switch (result)
        {
            case BTResult.Success:
                result = BTResult.Failure;
                break;
            case BTResult.Failure:
                result = BTResult.Success;
                break;
        }

        return (result, activeNode);
    }
}
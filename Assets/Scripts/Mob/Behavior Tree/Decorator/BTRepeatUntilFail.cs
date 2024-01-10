using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeatUntilFail : BTDecoratorNode
{
    public BTRepeatUntilFail(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.child = null;
    }

    public BTRepeatUntilFail(BTNode parent, BTNode child)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.child = child;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        (BTResult result, BTLeafNode activeNode) = child.Evaluate();
        this.activeNode = activeNode;

        if (result == BTResult.Failure) {
            return (BTResult.Success, activeNode);
        }
        return (BTResult.Running, activeNode);
    }
}
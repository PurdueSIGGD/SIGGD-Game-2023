using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeat : BTDecoratorNode
{
    public BTRepeat(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.child = null;
    }

    public BTRepeat(BTNode parent, BTNode child)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.child = child;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        (BTResult _, BTLeafNode activeNode) = child.Evaluate();
        this.activeNode = activeNode;
        return (BTResult.Running, activeNode);
    }
}
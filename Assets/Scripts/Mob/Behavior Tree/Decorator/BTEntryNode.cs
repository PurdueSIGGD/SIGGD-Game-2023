using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEntryNode : BTDecoratorNode
{
    public BTEntryNode(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.parent = null;
        this.activeNode = null;
        this.child = null;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        (BTResult _, BTLeafNode activeNode) = child.Evaluate();
        this.activeNode = activeNode;
        return (BTResult.Running, activeNode);
    }
}
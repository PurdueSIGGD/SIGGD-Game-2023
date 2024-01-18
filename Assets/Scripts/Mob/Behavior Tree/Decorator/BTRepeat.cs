using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeat : BTDecoratorNode
{
    public override (BTResult, BTLeafNode) Evaluate()
    {
        (BTResult _, BTLeafNode activeNode) = child.Evaluate();
        this.activeNode = activeNode;
        return (BTResult.Running, activeNode);
    }
}
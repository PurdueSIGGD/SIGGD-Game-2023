using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeat : BTDecoratorNode
{
    public BTRepeat(BTCompositeNode parent) : base(parent) { }
    public BTRepeat(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        (BTResult _, BTLeafNode activeNode) = child.Evaluate();
        this.activeNode = activeNode;
        return (BTResult.Running, activeNode);
    }
}
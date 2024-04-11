using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeatUntilFail : BTDecoratorNode
{
    public BTRepeatUntilFail(BTCompositeNode parent) : base(parent) { }
    public BTRepeatUntilFail(BTDecoratorNode parent) : base(parent) { }

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
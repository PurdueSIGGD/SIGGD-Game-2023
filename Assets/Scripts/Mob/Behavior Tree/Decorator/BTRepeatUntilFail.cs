using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRepeatUntilFail : BTDecoratorNode
{
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
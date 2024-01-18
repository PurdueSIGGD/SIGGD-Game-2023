using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTCompositeNode
{
    private int currentIndex;

    public override (BTResult, BTLeafNode) Evaluate()
    {
        for (int i = currentIndex; i < children.Count; i++)
        {
            BTNode child = children[i];
            (BTResult result, BTLeafNode activeNode) = child.Evaluate();
            this.activeNode = activeNode;

            switch (result)
            {
                case BTResult.Success:
                    currentIndex = 0;
                    return (result, activeNode);
                    break;
                case BTResult.Running:
                    return (result, activeNode);
                    break;
            }
            currentIndex++;
        }

        currentIndex = 0;
        return (BTResult.Failure, null);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTCompositeNode
{
    private int currentIndex;

    public BTSelector(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.children = null;
        this.currentIndex = 0;
    }

    public BTSelector(BTNode parent, BTNode[] children)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.children = children;
        this.currentIndex = 0;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        for (int i = currentIndex; i < children.Length; i++)
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
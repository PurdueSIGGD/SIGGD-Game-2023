using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTCompositeNode
{
    private int currentIndex;

    public BTSequence(BTNode parent)
    {
        this.parent = parent;
        this.gameObject = parent?.gameObject;
        this.children = null;
        this.currentIndex = 0;
    }

    public BTSequence(BTNode parent, BTNode[] children)
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
                case BTResult.Failure:
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
        return (BTResult.Success, null);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTDecoratorNode : BTNode
{
    public BTLeafNode activeNode;
    public BTNode child;

    public BTDecoratorNode() : base()
    {
        this.activeNode = null;
        this.child = null;
    }

    public BTDecoratorNode(BTCompositeNode parent) : base(parent)
    {
        this.activeNode = null;
        this.child = null;
    }

    public BTDecoratorNode(BTDecoratorNode parent) : base(parent)
    {
        this.activeNode = null;
        this.child = null;
    }
}
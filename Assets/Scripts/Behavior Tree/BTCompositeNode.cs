using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTCompositeNode : BTNode
{
    public BTLeafNode activeNode;
    public List<BTNode> children;

    public BTCompositeNode() : base()
    {
        this.activeNode = null;
        this.children = new List<BTNode>();
    }

    public BTCompositeNode(BTCompositeNode parent) : base(parent)
    {
        this.activeNode = null;
        this.children = new List<BTNode>();
    }

    public BTCompositeNode(BTDecoratorNode parent) : base(parent)
    {
        this.activeNode = null;
        this.children = new List<BTNode>();
    }
}
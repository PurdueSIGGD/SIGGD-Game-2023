using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    public enum BTResult
    {
        Failure,
        Success,
        Running,
    }

    public abstract (BTResult, BTLeafNode) Evaluate();

    public BTNode parent;
    public GameObject gameObject;

    public BTNode()
    {
        this.parent = null;
        this.gameObject = null;
    }

    public BTNode(BTCompositeNode parent)
    {
        this.parent = parent;
        this.gameObject = parent.gameObject;
        parent.children.Add(this);
    }

    public BTNode(BTDecoratorNode parent)
    {
        this.parent = parent;
        this.gameObject = parent.gameObject;
        parent.child = this;
    }
}
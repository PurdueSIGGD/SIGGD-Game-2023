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

    public BTNode parent;

    public BTNode()
    {
        this.parent = null;
    }

    public BTNode(BTCompositeNode parent)
    {
        this.parent = parent;
    }

    public BTNode(BTDecoratorNode parent)
    {
        this.parent = parent;
    }

    public abstract (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard);
}




public abstract class BTCompositeNode : BTNode
{
    public List<BTNode> children;

    public BTCompositeNode() : base()
    {
        this.children = new List<BTNode>();
    }

    public BTCompositeNode(BTCompositeNode parent) : base(parent)
    {
        this.children = new List<BTNode>();
    }

    public BTCompositeNode(BTDecoratorNode parent) : base(parent)
    {
        this.children = new List<BTNode>();
    }
}




public abstract class BTDecoratorNode : BTNode
{
    public BTNode child;

    public BTDecoratorNode() : base()
    {
        this.child = null;
    }

    public BTDecoratorNode(BTCompositeNode parent) : base(parent)
    {
        this.child = null;
    }

    public BTDecoratorNode(BTDecoratorNode parent) : base(parent)
    {
        this.child = null;
    }
}




public abstract class BTLeafNode : BTNode
{
    public BTLeafNode() : base() { }
    public BTLeafNode(BTCompositeNode parent) : base(parent) { }
    public BTLeafNode(BTDecoratorNode parent) : base(parent) { }
}
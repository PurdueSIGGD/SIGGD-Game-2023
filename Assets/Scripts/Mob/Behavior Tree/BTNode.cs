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

public abstract class BTLeafNode : BTNode
{
    public abstract void StartRunning();
    public abstract void StopRunning();

    public abstract void NodeUpdate();

    public BTLeafNode(BTCompositeNode parent) : base(parent) { }
    public BTLeafNode(BTDecoratorNode parent) : base(parent) { }
}

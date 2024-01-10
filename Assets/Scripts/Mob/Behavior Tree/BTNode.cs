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
}

public abstract class BTCompositeNode : BTNode
{
    public BTLeafNode activeNode;
    public BTNode[] children;
}

public abstract class BTDecoratorNode : BTNode
{
    public BTLeafNode activeNode;
    public BTNode child;
}

public abstract class BTLeafNode : BTNode
{
    public abstract void NodeUpdate();
}
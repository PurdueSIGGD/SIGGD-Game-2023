using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTLeafNode : BTNode
{
    public BTLeafNode(BTCompositeNode parent) : base(parent) { }
    public BTLeafNode(BTDecoratorNode parent) : base(parent) { }
}
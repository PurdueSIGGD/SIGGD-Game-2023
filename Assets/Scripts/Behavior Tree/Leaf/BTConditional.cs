using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTConditionalNode : BTLeafNode
{
    private Dictionary<string, bool> bitmap;
    private string key;

    public BTConditionalNode(BTCompositeNode parent, Dictionary<string, bool> bitmap, string key) : base(parent)
    {
        this.bitmap = bitmap;
        this.key = key;
    }

    public BTConditionalNode(BTDecoratorNode parent, Dictionary<string, bool> bitmap, string key) : base(parent) 
    {
        this.bitmap = bitmap;
        this.key = key;
    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        if (bitmap[key] == true) return (BTResult.Success, null);
        else return (BTResult.Failure, null);
    }
}
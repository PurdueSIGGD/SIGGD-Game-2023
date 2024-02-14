using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction : BTLeafNode
{
    private string actionName;

    public BTAction(string actionName) : base() 
    {
        this.actionName = actionName;
    }

    public BTAction(BTCompositeNode parent, string actionName) : base(parent) 
    {
        this.actionName = actionName;
    }

    public BTAction(BTDecoratorNode parent, string actionName) : base(parent)
    {
        this.actionName = actionName;
    }

    public override (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        bool result = blackboard[actionName];

        if (result)
        {
            return (BTResult.Success, null);
        }
        else return (BTResult.Failure, null);
    }
}
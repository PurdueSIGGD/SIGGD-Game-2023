using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTConditionalNode : BTLeafNode
{
    private string conditionName;

    public BTConditionalNode(string conditionName) : base()
    {
        this.conditionName = conditionName;
    }

    public BTConditionalNode(BTCompositeNode parent, string conditionName) : base(parent)
    {
        this.conditionName = conditionName;
    }

    public BTConditionalNode(BTDecoratorNode parent, string conditionName) : base(parent)
    {
        this.conditionName = conditionName;
    }

    public override (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        bool result = blackboard[conditionName];

        if (result) return (BTResult.Success, null);
        else return (BTResult.Failure, null);
    }
}
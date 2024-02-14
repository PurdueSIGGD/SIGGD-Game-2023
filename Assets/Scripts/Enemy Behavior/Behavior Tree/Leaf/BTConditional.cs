using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTConditional : BTLeafNode
{
    private string conditionName;

    public BTConditional(string conditionName) : base()
    {
        this.conditionName = conditionName;
    }

    public BTConditional(BTCompositeNode parent, string conditionName) : base(parent)
    {
        this.conditionName = conditionName;
    }

    public BTConditional(BTDecoratorNode parent, string conditionName) : base(parent)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCondition : BTLeafNode
{
    public BTCondition(BTCompositeNode parent, System.Delegate condition) : base(parent)
    {

    }

    public BTCondition(BTDecoratorNode parent, System.Delegate condition) : base(parent)
    {

    }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        return (BTResult.Success, null);
    }

    public override void NodeUpdate()
    {

    }

    public override void StartRunning()
    {


    }

    public override void StopRunning()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTActionNode : BTLeafNode
{
    public override (BTResult, BTLeafNode) Evaluate()
    {
        return (BTResult.Running, null);
    }
}
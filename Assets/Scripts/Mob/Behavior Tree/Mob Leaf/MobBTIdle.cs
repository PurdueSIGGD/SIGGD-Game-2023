using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTIdle: BTLeafNode
{
    public MobBTIdle(BTCompositeNode parent) : base(parent) { }
    public MobBTIdle(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, BTLeafNode) Evaluate()
    {
        return (BTResult.Running, this);
    }

    public override void NodeUpdate()
    {

    }

    public override IEnumerator StartRunning()
    {
        yield return null;
    }

    public override IEnumerator StopRunning()
    {
        yield return null;
    }
}
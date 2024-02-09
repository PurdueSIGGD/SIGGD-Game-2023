using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTLeafNode
{
    private double waitTime;
    private bool forceTick;
    private bool isCompleted;

    public BTWait(BTCompositeNode parent, double waitTime, bool forceTick) : base(parent)
    {
        this.waitTime = waitTime;
        this.forceTick = forceTick;
    }

    public BTWait(BTDecoratorNode parent, double waitTime, bool forceTick) : base(parent)
    {
        this.waitTime = waitTime;
        this.forceTick = forceTick;
    }

    private double timer;
    public override (BTResult, BTLeafNode) Evaluate()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0;
            return (BTResult.Success, null);
        }
        else return (BTResult.Running, this);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTAction
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

    public override (BTResult, BTLeafNode) Evaluate()
    {
        if (isCompleted) return (BTResult.Success, null);
        else return (BTResult.Running, this);
    }

    private double timer;
    public override void NodeUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            if (forceTick) { }; // TODO force a tick when complete
            isCompleted = true;
        }
    }

    public override void StartRunning()
    {


    }

    public override void StopRunning()
    {
        timer = 0;
        isCompleted = false;
    }
}
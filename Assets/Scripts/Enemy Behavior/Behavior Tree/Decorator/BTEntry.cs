using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEntry : BTDecoratorNode {

    public BTEntry() : base() { }
    public BTEntry(BTCompositeNode parent) : base(parent) { }
    public BTEntry(BTDecoratorNode parent) : base(parent) { }

    public override (BTResult, string) Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        (BTResult result, string action) = child.Evaluate(gameObject, blackboard);

        return (result, action);
    }
}
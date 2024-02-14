using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBTTree : ScriptableObject
{
    private BTEntry root;

    void Awake()
    {
        BTEntry root = new BTEntry();
        
        BTSelector or = new BTSelector(root);

            BTSequence and = new BTSequence(or);
                BTConditional inRange = new BTConditional(and, "InRange");
                BTAction dash = new BTAction(and, "Dash");

            BTAction follow = new BTAction(or, "Follow");
    }

    public string Evaluate(GameObject gameObject, Dictionary<string, bool> blackboard)
    {
        (BTNode.BTResult _, string result) = root.Evaluate(gameObject, blackboard);
        return result;
    }
}
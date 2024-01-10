using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MobBTree : MonoBehaviour
{
    public BTDecoratorNode rootNode;
    public BTLeafNode activeNode;

    void Awake()
    {
        rootNode = new BTRepeat(null);
        rootNode.gameObject = gameObject;

        BTSelector selector = new BTSelector(rootNode);
        rootNode.child = selector;

        BTSequence sequence = new BTSequence(selector);
        MobBTIdle idle = new MobBTIdle(selector);
        selector.children = new BTNode[] {sequence, idle};

        MobBTIsVisible isVisible = new MobBTIsVisible(sequence);
        MobBTFollow follow = new MobBTFollow(sequence);

        sequence.children = new BTNode[] {isVisible, follow};
    }

    private const double TICK_LENGTH = 1.0;
    private double tickTimer;
    void Update()
    {
        tickTimer += Time.deltaTime;
        while (tickTimer >= TICK_LENGTH) 
        {
            tickTimer -= TICK_LENGTH;
            (BTNode.BTResult _, BTLeafNode active) = rootNode.Evaluate();
            activeNode = active;
        }
        activeNode?.NodeUpdate();
    }
}
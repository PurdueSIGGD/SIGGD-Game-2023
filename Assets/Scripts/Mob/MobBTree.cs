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
        Debug.Log(typeof(BTNode).AssemblyQualifiedName);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MobBTree : MonoBehaviour
{
    public BTDecoratorNode rootNode;
    public BTLeafNode activeNode;

    private MobNavigationController mobNavigationController;
    private MobTargetingController mobTargetingController;

    void Awake()
    {
        rootNode = new BTEntryNode(gameObject);
        BTSelector selector = new BTSelector(rootNode);
        BTSequence sequence = new BTSequence(selector);
        MobBTIdle idle = new MobBTIdle(selector);

        MobBTTargetInRange hasTarget = new MobBTTargetInRange(sequence, 0, 100);
        MobBTFollow follow = new MobBTFollow(sequence);

        mobNavigationController = GetComponent<MobNavigationController>();
        mobTargetingController = GetComponent<MobTargetingController>();
    }

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        mobTargetingController.AddTarget(player);
    }

    private const double TICK_LENGTH = 1.0;
    private double tickTimer;
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_LENGTH) 
        {
            tickTimer -= TICK_LENGTH;
            (BTNode.BTResult _, BTLeafNode newActive) = rootNode.Evaluate();
            
            if (activeNode != newActive)
            {
                activeNode?.StopRunning();
                newActive.StartRunning();
            }

            activeNode = newActive;

        }
        activeNode?.NodeUpdate();
    }
}
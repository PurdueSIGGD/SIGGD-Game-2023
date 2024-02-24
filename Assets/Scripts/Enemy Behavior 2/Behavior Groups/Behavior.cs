public abstract class Behavior
{
    public enum BehaviorResult
    {
        Success,
        Failure,
        Running,
    };

    public abstract bool IsActive();

    protected abstract bool EvaluatePreConditions();
    public abstract float InitializeAndGetPriority();

    public abstract void StartBehavior();
    public abstract void StopBehavior();
 
    public abstract BehaviorResult BehaviorFixedUpdate();
}
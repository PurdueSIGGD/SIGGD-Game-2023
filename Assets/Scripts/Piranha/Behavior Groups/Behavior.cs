public abstract class Behavior
{
    public abstract bool evaluatePreConditions();
    public abstract float getPriority();

    public abstract void startBehavior();
    public abstract void stopBehavior();

    public abstract void behaviorUpdate();
}
public class Transition
{
    public IState From { get; }
    public IState To { get; }
    public ITransitionCondition Condition { get; }

    public Transition(IState from, IState to, ITransitionCondition condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }

    public bool CanTransition(IState currentState)
    {
        return From == currentState && Condition.IsMet();
    }
}
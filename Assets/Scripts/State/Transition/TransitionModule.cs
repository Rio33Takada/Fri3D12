using System.Collections.Generic;

public class TransitionModule : ITransitionModule
{
    private readonly List<Transition> transitions = new();

    private ModularStateMachine stateMachine;

    public void Initialize(ModularStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }

    public void Tick()
    {
        foreach (var transition in transitions)
        {
            if (transition.CanTransition(stateMachine.CurrentState))
            {
                stateMachine.ChangeState(transition.To);
                return;
            }
        }
    }

    public void OnBeforeChangeState(IState currentState, IState nextState)
    {
    }

    public void OnAfterChangeState(IState previousState, IState currentState)
    {
    }
}
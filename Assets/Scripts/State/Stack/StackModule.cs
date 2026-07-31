using System.Collections.Generic;

public class StackModule : IStackModule
{
    private readonly Stack<IState> stateStack = new();

    private ModularStateMachine stateMachine;

    public bool CanPop => stateStack.Count > 0;

    public void Initialize(ModularStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Push(IState nextState)
    {
        if (stateMachine.CurrentState != null)
        {
            stateStack.Push(stateMachine.CurrentState);
        }

        stateMachine.ChangeState(nextState);
    }

    public void Pop()
    {
        if (!CanPop)
            return;

        var previousState = stateStack.Pop();
        stateMachine.ChangeState(previousState);
    }

    public void OnBeforeChangeState(IState currentState, IState nextState)
    {
    }

    public void OnAfterChangeState(IState previousState, IState currentState)
    {
    }

    public void Tick()
    {
    }
}
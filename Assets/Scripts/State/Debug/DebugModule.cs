using UnityEngine;

public class DebugModule : IDebugModule
{
    private ModularStateMachine stateMachine;

    public void Initialize(ModularStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public string GetCurrentStateName()
    {
        return stateMachine.CurrentState?.GetType().Name ?? "None";
    }

    public void OnBeforeChangeState(IState currentState, IState nextState)
    {
        Debug.Log($"Exit: {currentState?.GetType().Name ?? "None"}");
        Debug.Log($"Enter: {nextState?.GetType().Name ?? "None"}");
    }

    public void OnAfterChangeState(IState previousState, IState currentState)
    {
    }

    public void Tick()
    {
    }
}
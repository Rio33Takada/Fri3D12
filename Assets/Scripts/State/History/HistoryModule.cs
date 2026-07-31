using System.Collections.Generic;

public class HistoryModule : IHistoryModule
{
    private readonly List<IState> history = new();

    public IReadOnlyList<IState> History => history;

    public void Initialize(ModularStateMachine stateMachine)
    {
    }

    public void OnBeforeChangeState(IState currentState, IState nextState)
    {
    }

    public void OnAfterChangeState(IState previousState, IState currentState)
    {
        if (currentState != null)
        {
            history.Add(currentState);
        }
    }

    public void Tick()
    {
    }
}
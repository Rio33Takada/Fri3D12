public class HierarchyModule : IHierarchyModule
{
    private ModularStateMachine stateMachine;

    public void Initialize(ModularStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
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
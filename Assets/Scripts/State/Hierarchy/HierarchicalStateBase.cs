public abstract class HierarchicalStateBase : StateBase, IHierarchicalState
{
    public ModularStateMachine ChildStateMachine { get; } = new();

    public abstract IState InitialChildState { get; }

    public override void Enter()
    {
        if (InitialChildState != null)
        {
            ChildStateMachine.ChangeState(InitialChildState);
        }
    }

    public override void Tick()
    {
        ChildStateMachine.Tick();
    }
}
public interface IHierarchicalState : IState
{
    ModularStateMachine ChildStateMachine { get; }
    IState InitialChildState { get; }
}
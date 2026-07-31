public interface IStackModule : IStateMachineModule
{
    void Push(IState nextState);
    void Pop();
    bool CanPop { get; }
}
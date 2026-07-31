public interface IStateMachineModule
{
    void Initialize(ModularStateMachine stateMachine);

    void OnBeforeChangeState(IState currentState, IState nextState);
    void OnAfterChangeState(IState previousState, IState currentState);

    void Tick();
}
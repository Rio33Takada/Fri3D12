public interface IStateMachine
{
    IState CurrentState { get; }

    void ChangeState(IState nextState);
    void Tick();
}
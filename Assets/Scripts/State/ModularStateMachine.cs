using System.Collections.Generic;

public class ModularStateMachine : IStateMachine
{
    private readonly List<IStateMachineModule> modules = new();

    public IState CurrentState { get; private set; }

    public void AddModule(IStateMachineModule module)
    {
        modules.Add(module);
        module.Initialize(this);
    }

    public void ChangeState(IState nextState)
    {
        if (nextState == null)
            return;

        var previousState = CurrentState;

        foreach (var module in modules)
            module.OnBeforeChangeState(CurrentState, nextState);

        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState.Enter();

        foreach (var module in modules)
            module.OnAfterChangeState(previousState, CurrentState);
    }

    public void Tick()
    {
        CurrentState?.Tick();

        foreach (var module in modules)
            module.Tick();
    }
}
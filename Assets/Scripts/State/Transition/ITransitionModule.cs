using System.Collections.Generic;

public interface ITransitionModule : IStateMachineModule
{
    void AddTransition(Transition transition);
}
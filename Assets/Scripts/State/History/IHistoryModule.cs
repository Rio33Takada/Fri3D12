using System.Collections.Generic;

public interface IHistoryModule : IStateMachineModule
{
    IReadOnlyList<IState> History { get; }
}
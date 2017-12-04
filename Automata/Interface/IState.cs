using System.Collections.Generic;

namespace Automata.Interface
{
    public interface IState
    {
        string Id { get; }
        IAutomata Automata { get; }
        bool IsStartState { get; set; }
        bool IsAcceptState { get; set; }

        IEnumerable<IStateTransition> SelfTransitions { get; }
        IEnumerable<IStateTransition> InTransitions { get; }
        IEnumerable<IStateTransition> OutTransitions { get; }
    }
}

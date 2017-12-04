using System.Collections.Generic;

namespace Automata.Interface
{
    using Event;
    using Transition;

    public interface IAutomata
    {
        IAlphabet Alphabet { get; }
        ISet<IState> States { get; }
        ISet<IStateTransition> Transitions { get; }

        event StateDelegate OnStateAdd;
        event StateDelegate OnStateDelete;
        event TransitionDelegate OnTransitionAdd;
        event TransitionDelegate OnTransitionDelete;

        IState GetOrCreateState(string stateId, bool isStartState = false, bool isAcceptState = false);
        IStateTransition CreateTransition(string sourceId, string targetId, string label = null);
        IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type);
    }
}

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
        event StateDelegate OnStateRemove;
        event TransitionDelegate OnTransitionAdd;
        event TransitionDelegate OnTransitionRemove;

        IState GetState(string stateId);
        IState GetOrCreateState(string stateId, bool isStartState = false, bool isAcceptState = false);
        void RemoveState(string stateId);

        IStateTransition CreateTransition(string sourceId, string targetId, params object[] symbols);
        IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type);
        void RemoveTrasition(IStateTransition transition);
    }
}

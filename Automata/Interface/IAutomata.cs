using System.Collections.Generic;

namespace Automata.Interface
{
    using Enum;
    using Event;
    using Transition;

    public interface IAutomata
    {
        #region Properties
        IAlphabet Alphabet { get; }
        ISet<IState> States { get; }
        ISet<IStateTransition> Transitions { get; }
        bool IsDeterministic { get; }
        #endregion

        #region Event
        event StateDelegate OnStateAdd;
        event StateDelegate OnStateUpdate;
        event StateDelegate OnStateRemove;
        event TransitionDelegate OnTransitionAdd;
        event TransitionDelegate OnTransitionUpdate;
        event TransitionDelegate OnTransitionRemove;
        #endregion

        #region State
        IState GetStartState();
        IState GetState(string stateId);
        IState GetOrCreateState(string stateId, bool isStartState = false, bool isAcceptState = false);
        IState CreateState(string stateId, bool isStartState = false, bool isAcceptState = false);
        void UpdateState(IState state);
        void RemoveState(string stateId);
        void RemoveState(IState state);
        #endregion

        #region Transition
        IStateTransition CreateTransition(string sourceId, string targetId, params object[] symbols);
        IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type);
        void UpdateTransition(IStateTransition transition);
        void RemoveTrasition(IStateTransition transition);
        void RemoveTransitionsForState(IState state);
        #endregion

        #region Virtual
        IState InstantiateState(string stateId, bool isStartState = false, bool isAcceptState = false);
        IStateTransition InstantiateTransition(string sourceId, string targetId, object[] symbols);
        #endregion
    }
}

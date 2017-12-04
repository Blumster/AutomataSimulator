using System;
using System.Collections.Generic;

namespace Automata.Finite
{
    using Event;
    using Interface;
    using State;
    using Transition;

    public abstract class FiniteAutomata : IAutomata
    {
        public IAlphabet Alphabet { get; }
        public ISet<IState> States { get; } = new HashSet<IState>();
        public ISet<IStateTransition> Transitions { get; } = new HashSet<IStateTransition>();

        public event StateDelegate OnStateAdd;
        public event StateDelegate OnStateDelete;
        public event TransitionDelegate OnTransitionAdd;
        public event TransitionDelegate OnTransitionDelete;

        public abstract bool CanAddTransition(IStateTransition transition);

        public FiniteAutomata(IAlphabet alphabet)
        {
            Alphabet = alphabet;
        }

        public FiniteAutomata(IAlphabet alphabet, IEnumerable<IState> states, IState startState, IEnumerable<IStateTransition> transitions, IEnumerable<IState> acceptingStates)
        {
            Alphabet = alphabet;

            startState.IsStartState = true;

            States.UnionWith(states);

            foreach (var state in acceptingStates)
                state.IsAcceptState = true;

            Transitions.UnionWith(transitions);
        }

        public IState GetOrCreateState(string stateId, bool isStartState = false, bool isAcceptState = false)
        {
            foreach (var state in States)
            {
                if (state.Id.Equals(stateId))
                    return state;
            }

            var newState = new State(stateId)
            {
                IsStartState = isStartState,
                IsAcceptState = isAcceptState
            };

            States.Add(newState);

            OnStateAdd?.Invoke(this, new StateEventArgs(newState));

            return newState;
        }

        public IStateTransition CreateTransition(string sourceId, string targetId, string label = null)
        {
            if (sourceId == null)
                throw new ArgumentNullException(nameof(sourceId), "The transition source id can not be null!");

            if (targetId == null)
                throw new ArgumentNullException(nameof(targetId), "The transition target id can not be null!");

            var sourceState = GetOrCreateState(sourceId);
            var targetState = GetOrCreateState(targetId);

            var newTransition = new SimpleTransition(sourceState, targetState)
            {
                Label = label
            };

            Transitions.Add(newTransition);

            OnTransitionAdd?.Invoke(this, new TransitionEventArgs(newTransition));

            return newTransition;
        }

        public IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type)
        {
            throw new NotImplementedException();
        }
    }
}

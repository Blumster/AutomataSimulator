using System;
using System.Collections.Generic;
using System.Xml;

namespace Automata.Finite
{
    using Enum;
    using Event;
    using Interface;
    using State;
    using Transition;

    public class FiniteAutomata : IAutomata
    {
        #region Properties
        public IAlphabet Alphabet { get; set; }
        public ISet<IState> States { get; } = new HashSet<IState>();
        public ISet<IStateTransition> Transitions { get; } = new HashSet<IStateTransition>();

        public AutomataType Type
        {
            get
            {
                return CalculateType();
            }
        }
        #endregion

        #region Events
        public event StateDelegate OnStateAdd;
        public event StateDelegate OnStateUpdate;
        public event StateDelegate OnStateRemove;
        public event TransitionDelegate OnTransitionAdd;
        public event TransitionDelegate OnTransitionUpdate;
        public event TransitionDelegate OnTransitionRemove;
        #endregion


        #region Constructors
        public FiniteAutomata()
        {
        }

        public FiniteAutomata(IAlphabet alphabet)
        {
            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet), "The state id can not be null!");
        }

        public FiniteAutomata(IAlphabet alphabet, IEnumerable<IState> states, IState startState, IEnumerable<IStateTransition> transitions, IEnumerable<IState> acceptingStates)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states), "The state list can not be null!");

            if (startState == null)
                throw new ArgumentNullException(nameof(startState), "The start state can not be null!");

            if (transitions == null)
                throw new ArgumentNullException(nameof(transitions), "The transition list can not be null!");

            if (acceptingStates == null)
                throw new ArgumentNullException(nameof(acceptingStates), "The accepting state list can not be null!");

            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet), "The alphabet can not be null!");

            startState.IsStartState = true;

            States.UnionWith(states);
            States.Add(startState);

            foreach (var state in acceptingStates)
                state.IsAcceptState = true;

            Transitions.UnionWith(transitions);
        }
        #endregion

        #region Interface
        #region State
        public IState GetStartState()
        {
            foreach (var state in States)
                if (state.IsStartState)
                    return state;

            return null;
        }

        public IState GetState(string stateId)
        {
            if (stateId == null)
                throw new ArgumentNullException(nameof(stateId), "The state id can not be null!");

            foreach (var state in States)
                if (state.Id.Equals(stateId))
                    return state;

            return null;
        }

        public IState GetOrCreateState(string stateId, bool isStartState = false, bool isAcceptState = false)
        {
            if (stateId == null)
                throw new ArgumentNullException(nameof(stateId), "The state id can not be null!");

            var state = GetState(stateId);
            if (state == null)
            {
                state = InstantiateState(stateId, isStartState, isAcceptState);

                if (States.Add(state))
                    OnStateAdd?.Invoke(this, new StateEventArgs(state));
            }

            return state;
        }

        public IState CreateState(string stateId, bool isStartState = false, bool isAcceptState = false)
        {
            if (stateId == null)
                throw new ArgumentNullException(nameof(stateId), "The state id can not be null!");

            var state = GetState(stateId);
            if (state != null)
                throw new Exception("A state with this id already exists!");

            state = InstantiateState(stateId, isStartState, isAcceptState);

            if (States.Add(state))
                OnStateAdd?.Invoke(this, new StateEventArgs(state));

            return state;
        }

        public void UpdateState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The state can not be null!");

            if (GetState(state.Id) == state)
                OnStateUpdate?.Invoke(this, new StateEventArgs(state));
        }

        public void RemoveState(string stateId)
        {
            if (stateId == null)
                throw new ArgumentNullException(nameof(stateId), "The state id can not be null!");

            var state = GetState(stateId);
            if (state != null)
                RemoveState(state);
        }

        public void RemoveState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The state can not be null!");

            RemoveTransitionsForState(state);

            if (States.Remove(state))
                OnStateRemove?.Invoke(this, new StateEventArgs(state));
        }
        #endregion

        #region Transition
        public IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The state can not be null!");

            var ret = new List<IStateTransition>();

            foreach (var transition in Transitions)
            {
                if ((type == TransitionType.Out || type == TransitionType.Self) && transition.SourceState == state)
                    ret.Add(transition);
                else if (type == TransitionType.In && transition.TargetState == state)
                    ret.Add(transition);
            }

            return ret;
        }

        public IStateTransition CreateTransition(string sourceId, string targetId, params object[] symbols)
        {
            if (sourceId == null)
                throw new ArgumentNullException(nameof(sourceId), "The transition source id can not be null!");

            if (targetId == null)
                throw new ArgumentNullException(nameof(targetId), "The transition target id can not be null!");

            var transition = InstantiateTransition(sourceId, targetId, symbols);

            Transitions.Add(transition);

            OnTransitionAdd?.Invoke(this, new TransitionEventArgs(transition));

            return transition;
        }

        public void UpdateTransition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The transition can not be null!");

            foreach (var existing in Transitions)
                if (existing == transition)
                    OnTransitionUpdate?.Invoke(this, new TransitionEventArgs(transition));
        }

        public void RemoveTrasition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The transition can not be null!");

            if (Transitions.Remove(transition))
                OnTransitionRemove?.Invoke(this, new TransitionEventArgs(transition));
        }

        public void RemoveTransitionsForState(IState state)
        {
            var toRemove = new List<IStateTransition>();

            foreach (var transition in Transitions)
                if (transition.SourceState == state || transition.TargetState == state)
                    toRemove.Add(transition);

            foreach (var transition in toRemove)
                RemoveTrasition(transition);
        }
        #endregion

        #region IO
        public void WriteToXmlWriter(XmlWriter writer)
        {

        }

        public void ReadFromXmlReader(XmlReader reader)
        {

        }
        #endregion
        #endregion

        #region Virtual
        public virtual IState InstantiateState(string stateId, bool isStartState = false, bool isAcceptState = false)
        {
            return new State(stateId)
            {
                IsStartState = isStartState,
                IsAcceptState = isAcceptState,
                Automata = this
            };
        }

        public virtual IStateTransition InstantiateTransition(string sourceId, string targetId, object[] symbols)
        {
            var sourceState = GetOrCreateState(sourceId);
            var targetState = GetOrCreateState(targetId);

            return new SimpleTransition(sourceState, targetState, symbols)
            {
                Automata = this
            };
        }
        #endregion

        #region Methods
        private AutomataType CalculateType()
        {
            var type = AutomataType.Deterministic;

            foreach (var state in States)
            {
                foreach (var symbol in Alphabet.GetSymbols())
                {
                    var applicableTransitions = new List<IStateTransition>();

                    foreach (var transition in GetTransitions(state, TransitionType.Out))
                    {
                        if (transition.HandlesSymbol(symbol))
                            applicableTransitions.Add(transition);
                    }

                    if (applicableTransitions.Count == 0 && type == AutomataType.Deterministic)
                        type = AutomataType.PartiallyDeterminsitic;
                    else if (applicableTransitions.Count > 1)
                        type = AutomataType.Nondeterministic;
                }
            }

            return type;
        }
        #endregion
    }
}

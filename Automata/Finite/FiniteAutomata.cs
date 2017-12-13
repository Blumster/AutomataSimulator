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

    /// <summary>
    /// Defines a finite implementation for an automata.
    /// </summary>
    public class FiniteAutomata : IAutomata
    {
        #region Properties
        /// <summary>
        /// Returns the alphabet of this automata.
        /// </summary>
        public IAlphabet Alphabet { get; set; }

        /// <summary>
        /// Returns the iterateable set of states in this automata.
        /// </summary>
        public ISet<IState> States { get; } = new HashSet<IState>();

        /// <summary>
        /// Returns the iteratable set of transitions in this automata.
        /// </summary>
        public ISet<IStateTransition> Transitions { get; } = new HashSet<IStateTransition>();

        /// <summary>
        /// Returns the current type of this automata.
        /// </summary>
        public AutomataType Type
        {
            get
            {
                return CalculateType();
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Defines an event handler that is called when a state is added to the automata.
        /// </summary>
        public event StateDelegate OnStateAdd;

        /// <summary>
        /// Defines an event handler that is called when a state is updated in the automata.
        /// </summary>
        public event StateDelegate OnStateUpdate;

        /// <summary>
        /// Defines an event handler that is called when a state is removed from the automata.
        /// </summary>
        public event StateDelegate OnStateRemove;

        /// <summary>
        /// Defines an event handler that is called when a transition is added to the automata.
        /// </summary>
        public event TransitionDelegate OnTransitionAdd;

        /// <summary>
        /// Defines an event handler that is called when a transition is updated in the automata.
        /// </summary>
        public event TransitionDelegate OnTransitionUpdate;

        /// <summary>
        /// Defines an event handler that is called when a transition is removed from the automata.
        /// </summary>
        public event TransitionDelegate OnTransitionRemove;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty finite automata.
        /// </summary>
        public FiniteAutomata()
        {
        }

        /// <summary>
        /// Creates an empty finite automata with the given alphabet.
        /// </summary>
        /// <param name="alphabet">The alphabet of the new automata</param>
        public FiniteAutomata(IAlphabet alphabet)
        {
            Alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet), "The state id can not be null!");
        }

        /// <summary>
        /// Creates a new finite automata with the given parameters.
        /// </summary>
        /// <param name="alphabet">The alphabet of the new automata.</param>
        /// <param name="states">The list of states of the new automata.</param>
        /// <param name="startState">The start state of the new automata.</param>
        /// <param name="transitions">The list of transitions of the new automata.</param>
        /// <param name="acceptingStates">The list of accepting transitions of the new automata.</param>
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
        /// <summary>
        /// Returns the start state of the automata.
        /// </summary>
        /// <returns>The start state instance or null.</returns>
        public IState GetStartState()
        {
            foreach (var state in States)
                if (state.IsStartState)
                    return state;

            return null;
        }

        /// <summary>
        /// Returns a state by a given unique id.
        /// </summary>
        /// <param name="stateId">The unique state id.</param>
        /// <returns>The state instance for the unique id or null.</returns>
        public IState GetState(string stateId)
        {
            if (stateId == null)
                throw new ArgumentNullException(nameof(stateId), "The state id can not be null!");

            foreach (var state in States)
                if (state.Id.Equals(stateId))
                    return state;

            return null;
        }

        /// <summary>
        /// Returns a state by a given unique id. If the state doesn't exist, it is created.
        /// </summary>
        /// <param name="stateId">The unique state id.</param>
        /// <param name="isStartState">True, if the new state is the starting state.</param>
        /// <param name="isAcceptState">True, if the new state is an accepting state.</param>
        /// <returns>The existing or newly created state.</returns>
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

        /// <summary>
        /// Creates a state by a given unique id.
        /// </summary>
        /// <param name="stateId">The unique state id.</param>
        /// <param name="isStartState">True, if the new state is the starting state.</param>
        /// <param name="isAcceptState">True, if the new state is an accepting state.</param>
        /// <returns>The newly created state.</returns>
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

        /// <summary>
        /// Handles the states's update after a property of it was modified.
        /// </summary>
        /// <param name="state">The state that was updated.</param>
        public void UpdateState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The state can not be null!");

            if (GetState(state.Id) == state)
                OnStateUpdate?.Invoke(this, new StateEventArgs(state));
        }

        /// <summary>
        /// Removes a state from the automata by it's unique id.
        /// </summary>
        /// <param name="stateId">The unique id of the state.</param>
        public void RemoveState(string stateId)
        {
            if (stateId == null)
                throw new ArgumentNullException(nameof(stateId), "The state id can not be null!");

            var state = GetState(stateId);
            if (state != null)
                RemoveState(state);
        }

        /// <summary>
        /// Removes a state from the automata.
        /// </summary>
        /// <param name="state">The state instance.</param>
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
        /// <summary>
        /// Creates and adds a transition to the automata.
        /// </summary>
        /// <param name="sourceId">The unique id of the source state.</param>
        /// <param name="targetId">The unique id of the target state.</param>
        /// <param name="symbols">The array of symbols this transitions transitions on.</param>
        /// <returns>The newly created transition.</returns>
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

        /// <summary>
        /// Returns the list of transitions that are related to the given state based on the given type.
        /// </summary>
        /// <param name="state">The source state.</param>
        /// <param name="type">The type of the transitions from the source state.</param>
        /// <returns>The list of transitions.</returns>
        public IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The state can not be null!");

            var ret = new List<IStateTransition>();

            foreach (var transition in Transitions)
            {
                if ((type == TransitionType.Out || type == TransitionType.Self || type == TransitionType.All) && transition.SourceState == state)
                    ret.Add(transition);
                else if ((type == TransitionType.In || type == TransitionType.All) && transition.TargetState == state)
                    ret.Add(transition);
            }

            return ret;
        }

        /// <summary>
        /// Handles the transition's update after a property of it was modified.
        /// </summary>
        /// <param name="transition">The trainsition that was updated.</param>
        public void UpdateTransition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The transition can not be null!");

            foreach (var existing in Transitions)
                if (existing == transition)
                    OnTransitionUpdate?.Invoke(this, new TransitionEventArgs(transition));
        }

        /// <summary>
        /// Removes a transition from the automata.
        /// </summary>
        /// <param name="transition">The transition that is removed.</param>
        public void RemoveTrasition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The transition can not be null!");

            if (Transitions.Remove(transition))
                OnTransitionRemove?.Invoke(this, new TransitionEventArgs(transition));
        }

        /// <summary>
        /// Removes all transitions that are related to a given state.
        /// </summary>
        /// <param name="state">The state instance.</param>
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
        /// <summary>
        /// This method is called to attach extra data to the XML element of this entity.
        /// </summary>
        /// <param name="writer">The current XML writer instance.</param>
        public void WriteToXmlWriter(XmlWriter writer)
        {
        }

        /// <summary>
        /// This method is called to read extra data from the XML element of this entity.
        /// </summary>
        /// <param name="reader">The current XML reader instance.</param>
        public void ReadFromXmlReader(XmlReader reader)
        {
        }
        #endregion
        
        #region Virtual
        /// <summary>
        /// Instantiates a new state for the automata.
        /// 
        /// Can be overridden to change the type of the state.
        /// </summary>
        /// <param name="stateId">The unique id and name of the new state.</param>
        /// <param name="isStartState">If true, the new state will be the starting state.</param>
        /// <param name="isAcceptState">If true, the new state will be an accepting state.</param>
        /// <returns>The new state.</returns>
        public virtual IState InstantiateState(string stateId, bool isStartState = false, bool isAcceptState = false)
        {
            return new State(stateId)
            {
                IsStartState = isStartState,
                IsAcceptState = isAcceptState,
                Automata = this
            };
        }

        /// <summary>
        /// Instantiates a new transition for the automata.
        /// 
        /// Can be overridden to change the type of the transition.
        /// </summary>
        /// <param name="sourceId">The unique id of the source state.</param>
        /// <param name="targetId">The unique id of the target state.</param>
        /// <param name="symbols">The array of symbols this transitions transitions on.</param>
        /// <returns>The new transition,</returns>
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
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the type of the automata based on it's current state.
        /// </summary>
        /// <returns>The current type of the automata.</returns>
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

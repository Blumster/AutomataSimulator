using System.Collections.Generic;

namespace Automata.Interface
{
    using Enum;
    using Event;

    /// <summary>
    /// Defines an interface for an automata implementation.
    /// </summary>
    public interface IAutomata : IXmlObject
    {
        #region Properties
        /// <summary>
        /// Returns the alphabet of this automata.
        /// </summary>
        IAlphabet Alphabet { get; set; }

        /// <summary>
        /// Returns the iterateable set of states in this automata.
        /// </summary>
        ISet<IState> States { get; }

        /// <summary>
        /// Returns the iteratable set of transitions in this automata.
        /// </summary>
        ISet<IStateTransition> Transitions { get; }

        /// <summary>
        /// Returns the current type of this automata.
        /// </summary>
        AutomataType Type { get; }
        #endregion

        #region Event
        /// <summary>
        /// Defines an event handler that is called when a state is added to the automata.
        /// </summary>
        event StateDelegate OnStateAdd;

        /// <summary>
        /// Defines an event handler that is called when a state is updated in the automata.
        /// </summary>
        event StateDelegate OnStateUpdate;

        /// <summary>
        /// Defines an event handler that is called when a state is removed from the automata.
        /// </summary>
        event StateDelegate OnStateRemove;

        /// <summary>
        /// Defines an event handler that is called when a transition is added to the automata.
        /// </summary>
        event TransitionDelegate OnTransitionAdd;

        /// <summary>
        /// Defines an event handler that is called when a transition is updated in the automata.
        /// </summary>
        event TransitionDelegate OnTransitionUpdate;

        /// <summary>
        /// Defines an event handler that is called when a transition is removed from the automata.
        /// </summary>
        event TransitionDelegate OnTransitionRemove;
        #endregion

        #region State
        /// <summary>
        /// Returns the start state of the automata.
        /// </summary>
        /// <returns>The start state instance or null.</returns>
        IState GetStartState();

        /// <summary>
        /// Returns a state by a given unique id.
        /// </summary>
        /// <param name="stateId">The unique state id.</param>
        /// <returns>The state instance for the unique id or null.</returns>
        IState GetState(string stateId);

        /// <summary>
        /// Returns a state by a given unique id. If the state doesn't exist, it is created.
        /// </summary>
        /// <param name="stateId">The unique state id.</param>
        /// <param name="isStartState">True, if the new state is the starting state.</param>
        /// <param name="isAcceptState">True, if the new state is an accepting state.</param>
        /// <returns>The existing or newly created state.</returns>
        IState GetOrCreateState(string stateId, bool isStartState = false, bool isAcceptState = false);

        /// <summary>
        /// Creates a state by a given unique id.
        /// </summary>
        /// <param name="stateId">The unique state id.</param>
        /// <param name="isStartState">True, if the new state is the starting state.</param>
        /// <param name="isAcceptState">True, if the new state is an accepting state.</param>
        /// <returns>The newly created state.</returns>
        IState CreateState(string stateId, bool isStartState = false, bool isAcceptState = false);

        /// <summary>
        /// Handles the states's update after a property of it was modified.
        /// </summary>
        /// <param name="state">The state that was updated.</param>
        void UpdateState(IState state);

        /// <summary>
        /// Removes a state from the automata by it's unique id.
        /// </summary>
        /// <param name="stateId">The unique id of the state.</param>
        void RemoveState(string stateId);

        /// <summary>
        /// Removes a state from the automata.
        /// </summary>
        /// <param name="state">The state instance.</param>
        void RemoveState(IState state);
        #endregion

        #region Transition
        /// <summary>
        /// Creates and adds a transition to the automata.
        /// </summary>
        /// <param name="sourceId">The unique id of the source state.</param>
        /// <param name="targetId">The unique id of the target state.</param>
        /// <param name="symbols">The array of symbols this transitions transitions on.</param>
        /// <returns>The newly created transition.</returns>
        IStateTransition CreateTransition(string sourceId, string targetId, params object[] symbols);

        /// <summary>
        /// Returns the list of transitions that are related to the given state based on the given type.
        /// </summary>
        /// <param name="state">The source state.</param>
        /// <param name="type">The type of the transitions from the source state.</param>
        /// <returns>The list of transitions.</returns>
        IEnumerable<IStateTransition> GetTransitions(IState state, TransitionType type);

        /// <summary>
        /// Handles the transition's update after a property of it was modified.
        /// </summary>
        /// <param name="transition">The trainsition that was updated.</param>
        void UpdateTransition(IStateTransition transition);

        /// <summary>
        /// Removes a transition from the automata.
        /// </summary>
        /// <param name="transition">The transition that is removed.</param>
        void RemoveTrasition(IStateTransition transition);

        /// <summary>
        /// Removes all transitions that are related to a given state.
        /// </summary>
        /// <param name="state">The state instance.</param>
        void RemoveTransitionsForState(IState state);
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
        IState InstantiateState(string stateId, bool isStartState = false, bool isAcceptState = false);

        /// <summary>
        /// Instantiates a new transition for the automata.
        /// 
        /// Can be overridden to change the type of the transition.
        /// </summary>
        /// <param name="sourceId">The unique id of the source state.</param>
        /// <param name="targetId">The unique id of the target state.</param>
        /// <param name="symbols">The array of symbols this transitions transitions on.</param>
        /// <returns>The new transition.</returns>
        IStateTransition InstantiateTransition(string sourceId, string targetId, object[] symbols);
        #endregion
    }
}

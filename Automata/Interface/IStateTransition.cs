﻿namespace Automata.Interface
{
    /// <summary>
    /// Defines an interface for a transition implementation.
    /// </summary>
    public interface IStateTransition : IXmlObject
    {
        #region Properties
        /// <summary>
        /// The automata this transition is in.
        /// </summary>
        IAutomata Automata { get; set; }

        /// <summary>
        /// This transitions source state it transitions from.
        /// </summary>
        IState SourceState { get; }

        /// <summary>
        /// This transitions target state it transitions to.
        /// </summary>
        IState TargetState { get; }

        /// <summary>
        /// The symbols on which this transition transitions to the target state.
        /// </summary>
        object[] Symbols { get; }

        /// <summary>
        /// The string of the label of this transition.
        /// </summary>
        string Label { get; }
        #endregion

        #region Transition
        /// <summary>
        /// Checks if the transitions handles the given symbol.
        /// </summary>
        /// <param name="symbol">The symbolt to check.</param>
        /// <returns>True, if the transition handles the symbol.</returns>
        bool HandlesSymbol(object symbol);
        #endregion
    }
}

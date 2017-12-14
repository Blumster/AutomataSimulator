using System;

using Microsoft.Msagl.Drawing;

namespace Automata.Simulator.Drawing
{
    using Interface;

    /// <summary>
    /// Defines a state that can be drawn, based on a background logic state.
    /// </summary>
    public class State : Node
    {
        #region Fields
        /// <summary>
        /// Boolean field for temporary start state option override.
        /// </summary>
        public bool? OverrideIsStartState = null;

        /// <summary>
        /// Boolean field for temporary accept state option override.
        /// </summary>
        public bool? OverrideIsAcceptState = null;
        #endregion

        #region Properties
        /// <summary>
        /// The background logic state.
        /// </summary>
        public IState LogicState { get; }

        /// <summary>
        /// Returms the background logic's start state value, if it's not overridden. If it is, it returns the overridden value.
        /// </summary>
        public bool IsStartState
        {
            get
            {
                return OverrideIsStartState ?? LogicState.IsStartState;
            }
        }

        /// <summary>
        /// Returms the background logic's accept state value, if it's not overridden. If it is, it returns the overridden value.
        /// </summary>
        public bool IsAcceptState
        {
            get
            {
                return OverrideIsAcceptState ?? LogicState.IsAcceptState;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new state based on the background logic state.
        /// </summary>
        /// <param name="logicState">The background logic state.</param>
        public State(IState logicState)
            : base(logicState.Id)
        {
            LogicState = logicState ?? throw new ArgumentNullException(nameof(logicState), "The logic state can not be null!");
        }
        #endregion
    }
}

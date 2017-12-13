using System;

namespace Automata.Event
{
    using Interface;

    /// <summary>
    /// Stores a state for event handling.
    /// </summary>
    public class StateEventArgs : EventArgs
    {
        /// <summary>
        /// The stored state.
        /// </summary>
        public IState State { get; }

        /// <summary>
        /// Creates a new instance with the given state.
        /// </summary>
        /// <param name="state"></param>
        public StateEventArgs(IState state)
        {
            State = state;
        }
    }
}

using System;

namespace Automata.Event
{
    using Interface;

    /// <summary>
    /// Stores a transition for event handling.
    /// </summary>
    public class TransitionEventArgs : EventArgs
    {
        /// <summary>
        /// The stored transition.
        /// </summary>
        public IStateTransition Transition { get; }

        /// <summary>
        /// Creates a new instance with the given transition.
        /// </summary>
        /// <param name="state"></param>
        public TransitionEventArgs(IStateTransition transition)
        {
            Transition = transition;
        }
    }
}

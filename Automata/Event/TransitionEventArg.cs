using System;

namespace Automata.Event
{
    using Interface;

    public class TransitionEventArgs : EventArgs
    {
        public IStateTransition Transition { get; }

        public TransitionEventArgs(IStateTransition transition)
        {
            Transition = transition;
        }
    }
}

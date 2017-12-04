using System;

namespace Automata.Event
{
    using Interface;

    public class StateEventArgs : EventArgs
    {
        public IState State { get; }

        public StateEventArgs(IState state)
        {
            State = state;
        }
    }
}

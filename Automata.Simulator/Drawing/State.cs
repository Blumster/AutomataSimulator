using System;

using Microsoft.Msagl.Drawing;

namespace Automata.Simulator.Drawing
{
    using Interface;

    public class State : Node
    {
        public IState LogicState { get; }

        public bool IsStartState
        {
            get
            {
                return LogicState.IsStartState;
            }
        }
        public bool IsAcceptState
        {
            get
            {
                return LogicState.IsAcceptState;
            }
        }

        public State(IState logicState)
            : base(logicState.Id)
        {
            LogicState = logicState;
        }
    }
}

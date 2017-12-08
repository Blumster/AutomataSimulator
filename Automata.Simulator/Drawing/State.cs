using Microsoft.Msagl.Drawing;

namespace Automata.Simulator.Drawing
{
    using Interface;

    public class State : Node
    {
        #region Fields
        public bool? OverrideIsStartState = null;
        public bool? OverrideIsAcceptState = null;
        #endregion

        public IState LogicState { get; }

        public bool IsStartState
        {
            get
            {
                if (OverrideIsStartState.HasValue)
                    return OverrideIsStartState.Value;

                return LogicState.IsStartState;
            }
        }
        public bool IsAcceptState
        {
            get
            {
                if (OverrideIsAcceptState.HasValue)
                    return OverrideIsAcceptState.Value;

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

using System.Collections.Generic;
using System.Diagnostics;

namespace Automata.State
{
    using Interface;
    using Transition;

    [DebuggerDisplay("Állapot: {Id}")]
    public class State : IState
    {
        public string Id { get; set; }
        public IAutomata Automata { get; set; }
        public bool IsStartState { get; set; }
        public bool IsAcceptState { get; set; }

        IEnumerable<IStateTransition> IState.SelfTransitions
        {
            get
            {
                return Automata.GetTransitions(this, TransitionType.Self);
            }
        }

        IEnumerable<IStateTransition> IState.InTransitions
        {
            get
            {
                return Automata.GetTransitions(this, TransitionType.In);
            }
        }

        IEnumerable<IStateTransition> IState.OutTransitions
        {
            get
            {
                return Automata.GetTransitions(this, TransitionType.Out);
            }
        }

        public State(string id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is State)
                return Equals(obj as State);

            return false;
        }

        public bool Equals(State obj)
        {
            if (obj == null)
                return false;

            return Id.Equals(obj.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }
    }
}

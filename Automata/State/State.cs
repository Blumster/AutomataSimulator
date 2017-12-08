using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automata.State
{
    using Interface;
    using Transition;

    [DebuggerDisplay("Állapot: {Id}")]
    public class State : IState
    {
        #region Fields
        private string _id;
        #endregion

        #region Properties
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

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value ?? throw new ArgumentNullException("Id", "The state's id can not be null!");
            }
        }
        #endregion

        #region Constructor
        public State(string id)
        {
            if (id.Length > 3)
                throw new ArgumentOutOfRangeException(nameof(id), "The state id can only be 3 characters long!");

            Id = id;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj is State state)
                return Equals(state);

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
        #endregion
    }
}

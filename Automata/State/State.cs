using System;
using System.Collections.Generic;
using System.Xml;

namespace Automata.State
{
    using Enum;
    using Interface;

    /// <summary>
    /// The state implementation based on the IState interface.
    /// </summary>
    public class State : IState
    {
        #region Properties
        /// <summary>
        /// The unique id of the state.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The automata this state is in.
        /// </summary>
        public IAutomata Automata { get; set; }

        /// <summary>
        /// Determines if this state is the start state of the automata.
        /// </summary>
        public bool IsStartState { get; set; }

        /// <summary>
        /// Determines if this state is an accept state.
        /// </summary>
        public bool IsAcceptState { get; set; }

        /// <summary>
        /// The list of loop transitions.
        /// </summary>
        IEnumerable<IStateTransition> IState.SelfTransitions
        {
            get
            {
                return Automata.GetTransitions(this, TransitionType.Self);
            }
        }

        /// <summary>
        /// The list of incoming transitions.
        /// </summary>
        IEnumerable<IStateTransition> IState.InTransitions
        {
            get
            {
                return Automata.GetTransitions(this, TransitionType.In);
            }
        }

        /// <summary>
        /// The list of outgoing transitions
        /// </summary>
        IEnumerable<IStateTransition> IState.OutTransitions
        {
            get
            {
                return Automata.GetTransitions(this, TransitionType.Out);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new state with the given unique id.
        /// </summary>
        /// <param name="id">The unique state id.</param>
        public State(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "The state's id can not be null!");

            if (id.Length > 3)
                throw new ArgumentOutOfRangeException(nameof(id), "The state id can only be 3 characters long!");

            Id = id;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is called to attach extra data to the XML element of this entity.
        /// </summary>
        /// <param name="writer">The current XML writer instance.</param>
        public void WriteToXmlWriter(XmlWriter writer)
        {
            
        }

        /// <summary>
        /// This method is called to read extra data from the XML element of this entity.
        /// </summary>
        /// <param name="reader">The current XML reader instance.</param>
        public void ReadFromXmlReader(XmlReader reader)
        {
            
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Checks if the other object equals with this.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>True, if they are equal.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as State);
        }

        /// <summary>
        /// Checks if the other state equals with this.
        /// </summary>
        /// <param name="obj">The other state.</param>
        /// <returns>True, if they are equal.</returns>
        public bool Equals(State obj)
        {
            if (obj == null)
                return false;

            return Id.Equals(obj.Id);
        }

        /// <summary>
        /// Generates the hash code for this state.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Returns the textual representation of this state.
        /// </summary>
        /// <returns>The textual representation.</returns>
        public override string ToString()
        {
            return Id;
        }
        #endregion
    }
}

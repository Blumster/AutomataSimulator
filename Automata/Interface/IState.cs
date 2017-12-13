using System.Collections.Generic;
using System.Xml;

namespace Automata.Interface
{
    /// <summary>
    /// Defines an interface for a state implementation.
    /// </summary>
    public interface IState
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The automata this state is in.
        /// </summary>
        IAutomata Automata { get; set; }

        /// <summary>
        /// Determines if this state is the start state of the automata.
        /// </summary>
        bool IsStartState { get; set; }


        /// <summary>
        /// Determines if this state is an accept state.
        /// </summary>
        bool IsAcceptState { get; set; }
        #endregion

        #region Transitions
        IEnumerable<IStateTransition> SelfTransitions { get; }
        IEnumerable<IStateTransition> InTransitions { get; }
        IEnumerable<IStateTransition> OutTransitions { get; }
        #endregion

        #region IO
        /// <summary>
        /// This method is called to attach extra data to the XML element of this entity.
        /// </summary>
        /// <param name="writer">The current XML writer instance.</param>
        void WriteToXmlWriter(XmlWriter writer);

        /// <summary>
        /// This method is called to read extra data from the XML element of this entity.
        /// </summary>
        /// <param name="reader">The current XML reader instance.</param>
        void ReadFromXmlReader(XmlReader reader);
        #endregion
    }
}

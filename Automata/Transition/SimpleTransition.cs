using System;
using System.Xml;

namespace Automata.Transition
{
    using Interface;

    /// <summary>
    /// The simple transition implementation based on the IStateTransition interface.
    /// </summary>
    public class SimpleTransition : IStateTransition
    {
        #region Properties
        /// <summary>
        /// The automata this transition is in.
        /// </summary>
        public IAutomata Automata { get; set; }

        /// <summary>
        /// This transitions source state it transitions from.
        /// </summary>
        public IState SourceState { get; }

        /// <summary>
        /// This transitions target state it transitions to.
        /// </summary>
        public IState TargetState { get; }

        /// <summary>
        /// The symbols on which this transition transitions to the target state.
        /// </summary>
        public object[] Symbols { get; }

        /// <summary>
        /// The string of the label of this transition.
        /// </summary>
        public virtual string Label
        {
            get
            {
                return Automata.Alphabet.ConstructSymbolText(Symbols);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new transition with the given parameters.
        /// </summary>
        /// <param name="source">The source state.</param>
        /// <param name="target">The target state.</param>
        /// <param name="symbols">The handled symbols array.</param>
        public SimpleTransition(IState source, IState target, object[] symbols)
        {
            SourceState = source;
            TargetState = target;
            
            Symbols = new object[symbols.Length];

            Array.Copy(symbols, Symbols, symbols.Length);
        }
        #endregion

        #region Interface
        /// <summary>
        /// Checks if the transitions handles the given symbol.
        /// </summary>
        /// <param name="symbol">The symbolt to check.</param>
        /// <returns>True, if the transition handles the symbol.</returns>
        public bool HandlesSymbol(object symbol)
        {
            if (!Automata.Alphabet.ContainsSymbol(symbol))
                return false;

            foreach (var handledSymbols in Symbols)
                if (symbol.Equals(handledSymbols))
                    return true;

            return false;
        }
        #endregion

        #region IO
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
    }
}

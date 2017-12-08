using System;
using System.Diagnostics;
using System.Xml;

namespace Automata.Transition
{
    using Interface;

    [DebuggerDisplay("{SourceState.Id} -> {TargetState.Id} ({Label})")]
    public class SimpleTransition : IStateTransition
    {
        public IAutomata Automata { get; set; }
        public IState SourceState { get; }
        public IState TargetState { get; }
        public object[] Symbols { get; }

        public virtual string Label
        {
            get
            {
                return Automata.Alphabet.ConstructSymbolText(Symbols);
            }
        }

        public SimpleTransition(IState source, IState target, object[] symbols)
        {
            SourceState = source;
            TargetState = target;
            
            Symbols = new object[symbols.Length];

            Array.Copy(symbols, Symbols, symbols.Length);
        }

        public bool HandlesSymbol(object symbol)
        {
            if (!Automata.Alphabet.ContainsSymbol(symbol))
                return false;

            foreach (var handledSymbols in Symbols)
                if (symbol.Equals(handledSymbols))
                    return true;

            return false;
        }

        public void WriteToXmlWriter(XmlWriter writer)
        {

        }

        public void ReadFromXmlReader(XmlReader reader)
        {

        }
    }
}

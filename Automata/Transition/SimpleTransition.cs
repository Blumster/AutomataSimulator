using System;
using System.Diagnostics;

namespace Automata.Transition
{
    using Interface;

    [DebuggerDisplay("{Source.Id} -> {Target.Id}")]
    public class SimpleTransition : IStateTransition
    {
        public IAutomata Automata { get; set; }
        public IState SourceState { get; }
        public IState TargetState { get; }
        public object[] Symbols { get; }

        public string Label
        {
            get
            {
                return Automata.Alphabet.ConstructLabel(Symbols);
            }
        }

        public SimpleTransition(IState source, IState target, object[] symbols)
        {
            SourceState = source;
            TargetState = target;
            
            Symbols = new object[symbols.Length];

            Array.Copy(symbols, Symbols, symbols.Length);
        }
    }
}

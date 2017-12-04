using System.Diagnostics;

namespace Automata.Transition
{
    using Interface;

    [DebuggerDisplay("{Source.Id} -> {Target.Id}")]
    public class SimpleTransition : IStateTransition
    {
        public IState SourceState { get; }

        public IState TargetState { get; }
        public string Label { get; set; }

        public SimpleTransition(IState source, IState target)
        {
            SourceState = source;
            TargetState = target;
        }
    }
}

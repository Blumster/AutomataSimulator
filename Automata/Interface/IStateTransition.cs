namespace Automata.Interface
{
    public interface IStateTransition
    {
        IState SourceState { get; }
        IState TargetState { get; }
        string Label { get; }

        bool HandlesSymbol(object symbol);
    }
}

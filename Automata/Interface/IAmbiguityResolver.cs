namespace Automata.Interface
{
    public interface IAmbiguityResolver
    {
        IStateTransition Resolve(ISimulation simulation);
    }
}

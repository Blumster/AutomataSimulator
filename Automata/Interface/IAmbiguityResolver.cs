namespace Automata.Interface
{
    /// <summary>
    /// Defines an interface for an ambiguity resolver implementation.
    /// </summary>
    public interface IAmbiguityResolver
    {
        /// <summary>
        /// Resolves an ambigiuous simulation.
        /// </summary>
        /// <param name="simulation">The current simulation instance.</param>
        /// <returns>The resolved transition to apply.</returns>
        IStateTransition Resolve(ISimulation simulation);
    }
}

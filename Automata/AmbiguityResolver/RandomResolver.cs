using System;
using System.Linq;

namespace Automata.AmbiguityResolver
{
    using Interface;

    public class RandomResolver : IAmbiguityResolver
    {
        /// <summary>
        /// Resolves an ambigiuous simulation.
        /// </summary>
        /// <param name="simulation">The current simulation instance.</param>
        /// <returns>The resolved transition to apply.</returns>
        public IStateTransition Resolve(ISimulation simulation)
        {
            var trans = simulation.GetApplicableTransitions();

            return trans.ElementAt(new Random().Next(0, trans.Count()));
        }
    }
}

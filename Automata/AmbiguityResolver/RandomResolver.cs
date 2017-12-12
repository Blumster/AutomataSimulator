using System;
using System.Linq;

namespace Automata.AmbiguityResolver
{
    using Interface;

    public class RandomResolver : IAmbiguityResolver
    {
        public IStateTransition Resolve(ISimulation simulation)
        {
            var trans = simulation.GetApplicableTransitions();

            return trans.ElementAt(new Random().Next(0, trans.Count()));
        }
    }
}

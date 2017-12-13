using System.Windows.Forms;

namespace Automata.Simulator.Resolver
{
    using Form;
    using Interface;

    /// <summary>
    /// Defines a manual, UI based, ambiguity resolver implementation.
    /// </summary>
    public class ManualResolver : IAmbiguityResolver
    {
        /// <summary>
        /// Resolves an ambigiuous simulation.
        /// </summary>
        /// <param name="simulation">The current simulation instance.</param>
        /// <returns>The resolved transition to apply.</returns>
        public IStateTransition Resolve(ISimulation simulation)
        {
            using (var transitionResolverForm = new TransitionResolverForm(simulation))
            {
                if (transitionResolverForm.ShowDialog() == DialogResult.Cancel)
                    return null;

                return transitionResolverForm.SelectedTransition;
            }
        }
    }
}

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
        private bool _isWindowOpen = false;

        /// <summary>
        /// Resolves an ambigiuous simulation.
        /// </summary>
        /// <param name="simulation">The current simulation instance.</param>
        /// <returns>The resolved transition to apply.</returns>
        public IStateTransition Resolve(ISimulation simulation)
        {
            if (_isWindowOpen)
                return null;

            _isWindowOpen = true;

            using (var transitionResolverForm = new TransitionResolverForm(simulation))
            {
                _isWindowOpen = true;

                if (transitionResolverForm.ShowDialog() == DialogResult.Cancel)
                    return null;

                _isWindowOpen = false;

                return transitionResolverForm.SelectedTransition;
            }
        }
    }
}

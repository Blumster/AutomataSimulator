using System.Windows.Forms;

namespace Automata.Simulator.Resolver
{
    using Form;
    using Interface;

    public class ManualResolver : IAmbiguityResolver
    {
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

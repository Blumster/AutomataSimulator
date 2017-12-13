using Microsoft.Msagl.Drawing;

using MsaglEdge = Microsoft.Msagl.Drawing.Edge;

namespace Automata.Simulator.Drawing
{
    using Interface;

    /// <summary>
    /// Defines an edge that can be drawn, based on a transition.
    /// </summary>
    public class Edge : MsaglEdge
    {
        /// <summary>
        /// The logic holder transition.
        /// </summary>
        public IStateTransition LogicTransition { get; }

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="sourceState">The source state.</param>
        /// <param name="targetState">The target state.</param>
        /// <param name="transition">The logic transition.</param>
        public Edge(State sourceState, State targetState, IStateTransition transition)
            : base(sourceState, targetState, ConnectionToGraph.Connected)
        {
            LogicTransition = transition;

            LabelText = LogicTransition.Label;
        }
    }
}

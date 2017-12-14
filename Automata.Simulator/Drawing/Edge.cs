using System;

using Microsoft.Msagl.Drawing;

using MsaglEdge = Microsoft.Msagl.Drawing.Edge;

namespace Automata.Simulator.Drawing
{
    using Interface;

    /// <summary>
    /// Defines an edge that can be drawn, based on a background logic transition.
    /// </summary>
    public class Edge : MsaglEdge
    {
        #region Properties
        /// <summary>
        /// The background logic transition.
        /// </summary>
        public IStateTransition LogicTransition { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new edge based on the background logic transition.
        /// </summary>
        /// <param name="sourceState">The source state.</param>
        /// <param name="targetState">The target state.</param>
        /// <param name="transition">The background logic transition.</param>
        public Edge(State sourceState, State targetState, IStateTransition transition)
            : base(sourceState, targetState, ConnectionToGraph.Connected)
        {
            LogicTransition = transition ?? throw new ArgumentNullException(nameof(transition), "The logic transition can not be null!");

            LabelText = LogicTransition.Label;
        }
        #endregion
    }
}

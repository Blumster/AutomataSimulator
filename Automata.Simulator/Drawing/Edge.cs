using System;

using Microsoft.Msagl.Drawing;

using MsaglEdge = Microsoft.Msagl.Drawing.Edge;

namespace Automata.Simulator.Drawing
{
    using Interface;
    

    public class Edge : MsaglEdge
    {
        public IStateTransition LogicTransition { get; }

        public Edge(State sourceState, State targetState, IStateTransition transition)
            : base(sourceState, targetState, ConnectionToGraph.Connected)
        {
            LogicTransition = transition;

            LabelText = LogicTransition.Label;
        }
    }
}

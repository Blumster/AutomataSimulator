using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;

using MsaglPoint = Microsoft.Msagl.Core.Geometry.Point;

namespace Automata.Simulator.Drawing
{
    using Interface;

    public class AutomataGraph : Graph
    {
        private static FieldInfo SegmentsFieldInfo = null;

        public IAutomata Automata { get; }
        public bool IsSaved { get; set; }
        public double OriginalScale { get; private set; } = double.NaN;

        public event Action OnRedraw;

        public AutomataGraph(IAutomata automata)
        {
            Automata = automata;

            SetupEvents();
            BuildGraph();
        }

        private void SetupEvents()
        {
            Automata.OnStateAdd += (o, args) => AddState(args.State);
            Automata.OnStateDelete += (o, args) => RemoveState(args.State);
            Automata.OnTransitionAdd += (o, args) => AddTransition(args.Transition);
            Automata.OnTransitionDelete += (o, args) => RemoveTransition(args.Transition);
        }

        private void BuildGraph()
        {
            foreach (var logicState in Automata.States)
                AddState(logicState);

            foreach (var logicTransition in Automata.Transitions)
                AddTransition(logicTransition);
        }

        private void AddState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The logic state can not be null!");

            var drawingState = NodeMap[state.Id] as State;
            if (drawingState == null)
            {
                drawingState = new State(state)
                {
                    DrawNodeDelegate = DrawNode
                };

                NodeMap[state.Id] = drawingState;

                IsSaved = false;

                Redraw();
            }
        }

        private void RemoveState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The logic state can not be null!");

            var drawingState = NodeMap[state.Id] as State;
            if (drawingState != null)
            {
                RemoveNode(drawingState);

                IsSaved = false;

                Redraw();
            }
        }

        private void AddTransition(IStateTransition transition)
        {
            var sourceState = NodeMap[transition.SourceState.Id] as State;
            var targetState = NodeMap[transition.TargetState.Id] as State;

            if (sourceState == null || targetState == null)
                throw new Exception("You can not add a transition to a non-existant state!");

            AddPrecalculatedEdge(new Edge(sourceState, targetState, transition));

            Redraw();
        }

        private void RemoveTransition(IStateTransition transition)
        {
            Edge edgeToRemove = null;

            foreach (var edge in Edges.Select(e => e as Edge))
                if (edge != null && edge.LogicTransition == transition)
                    edgeToRemove = edge as Edge;

            if (edgeToRemove != null)
                RemoveEdge(edgeToRemove);
        }

        public void Redraw()
        {
            OnRedraw?.Invoke();
        }

        private bool DrawNode(Node node, object graphics)
        {
            var drawGraphics = graphics as Graphics;
            var state = node as State;

            if (drawGraphics == null || state == null)
                return false;

            state.Attr.Shape = Shape.DrawFromGeometry;
            state.Attr.LineWidth = 0.7;

            Curve curve = new Curve();
            
            var center = state.GeometryNode.BoundaryCurve.BoundingBox.Center;

            if (state.IsAcceptState)
                AddDiscontinousSegment(curve, new Ellipse(9, 9, center));

            AddDiscontinousSegment(curve, new Ellipse(10, 10, center));

            if (state.IsStartState)
            {
                var triangleCenter = new MsaglPoint(center.X - 15, center.Y);

                AddDiscontinousSegment(curve, CurveFactory.RotateCurveAroundCenterByDegree(CurveFactory.CreateInteriorTriangle(10, 10, triangleCenter), triangleCenter, -90));

                state.Label.Text = $" {state.Id}";
            }

            state.GeometryNode.BoundaryCurve = curve;

            return false;
        }

        private static void AddDiscontinousSegment(Curve container, ICurve curve)
        {
            if (SegmentsFieldInfo == null)
                SegmentsFieldInfo = typeof(Curve).GetField("segs", BindingFlags.NonPublic | BindingFlags.Instance);

            if (SegmentsFieldInfo == null)
                throw new Exception("The program was unable to mine out the segs field!");

            container.ParStart = 0;

            var containerSegments = GetSegmentsField(container);

            if (curve is Curve)
            {
                var curveSegments = GetSegmentsField(curve);

                container.IncreaseSegmentCapacity(curveSegments.Count);

                foreach (var c in curveSegments)
                {
                    containerSegments.Add(c);

                    container.ParEnd += c.ParEnd - c.ParStart;
                }
            }
            else
            {
                containerSegments.Add(curve);

                container.ParEnd += curve.ParEnd - curve.ParStart;
            }
        }

        private static IList<ICurve> GetSegmentsField(ICurve curve)
        {
            if (SegmentsFieldInfo == null)
                throw new Exception("The program was unable to mine out the segs field!");

            var segments = SegmentsFieldInfo.GetValue(curve) as IList<ICurve>;
            if (segments == null)
                throw new Exception("The segs field's value isn't an IList<ICurve>!");

            return segments;
        }
    }
}

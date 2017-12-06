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
    using Event;
    using Interface;

    public class AutomataGraph : Graph
    {
        #region Static fields
        private static FieldInfo SegmentsFieldInfo = null;
        #endregion

        #region Fields
        private IAutomata _automata;
        #endregion

        #region Events
        public event Action OnRedraw;
        #endregion

        #region Properties
        public bool IsSaved { get; set; }

        public IAutomata Automata
        {
            get
            {
                return _automata;
            }
            set
            {
                if (_automata != null)
                {
                    TeardownGraph();
                    TeardownEvents();
                }

                _automata = value;

                SetupEvents();
                SetupGraph();
            }
        }
        #endregion


        #region Constructor
        public AutomataGraph(IAutomata automata)
        {
            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");
        }
        #endregion

        #region Event handling
        private void SetupEvents()
        {
            Automata.OnStateAdd += OnStateAdd;
            Automata.OnStateRemove += OnStateRemove;
            Automata.OnTransitionAdd += OnTransitionAdd;
            Automata.OnTransitionRemove += OnTransitionRemove;
        }

        private void TeardownEvents()
        {
            Automata.OnStateAdd -= OnStateAdd;
            Automata.OnStateRemove -= OnStateRemove;
            Automata.OnTransitionAdd -= OnTransitionAdd;
            Automata.OnTransitionRemove -= OnTransitionRemove;
        }

        private void OnStateAdd(object sender, StateEventArgs args)
        {
            AddState(args.State);
        }

        private void OnStateRemove(object sender, StateEventArgs args)
        {
            RemoveState(args.State);
        }

        private void OnTransitionAdd(object sender, TransitionEventArgs args)
        {
            AddTransition(args.Transition);
        }

        private void OnTransitionRemove(object sender, TransitionEventArgs args)
        {
            RemoveTransition(args.Transition);
        }
        #endregion

        #region Simulation
        public void StartSimulation()
        {

        }

        public void EndSimulation()
        {

        }
        #endregion

        #region Drawing
        private void SetupGraph()
        {
            foreach (var logicState in Automata.States)
                AddState(logicState);

            foreach (var logicTransition in Automata.Transitions)
                AddTransition(logicTransition);
        }

        private void TeardownGraph()
        {
            foreach (var logicState in Automata.States)
                RemoveState(logicState);

            foreach (var logicTransition in Automata.Transitions)
                RemoveTransition(logicTransition);

            if (NodeCount > 0 || EdgeCount > 0)
                throw new Exception("The automata graph is in an invalid state!");
        }

        public void Redraw()
        {
            OnRedraw?.Invoke();
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

            if (NodeMap[state.Id] is State drawingState)
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

            IsSaved = false;

            Redraw();
        }

        private void RemoveTransition(IStateTransition transition)
        {
            Edge edgeToRemove = null;

            foreach (var edge in Edges.Select(e => e as Edge))
                if (edge != null && edge.LogicTransition == transition)
                    edgeToRemove = edge as Edge;

            if (edgeToRemove != null)
            {
                IsSaved = false;

                RemoveEdge(edgeToRemove);
            }
        }

        private bool DrawNode(Node node, object graphics)
        {
            var drawGraphics = graphics as Graphics;
            var state = node as State;

            if (drawGraphics == null || state == null)
                return false;

            state.Attr.Shape = Shape.DrawFromGeometry;
            state.Attr.LineWidth = 0.7;

            var curve = new Curve();

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
        #endregion

        #region Helpers
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
                throw new Exception("The program was unable to mine out the \"segs\" field!");

            var segments = SegmentsFieldInfo.GetValue(curve) as IList<ICurve>;
            if (segments == null)
                throw new Exception("The segs field's value isn't an IList<ICurve>!");

            return segments;
        }
        #endregion
    }
}

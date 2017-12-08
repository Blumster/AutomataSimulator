﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;

using MsaglRectangle = Microsoft.Msagl.Core.Geometry.Rectangle;
using MsaglSize = Microsoft.Msagl.Core.DataStructures.Size;
using DrawingEdge = Microsoft.Msagl.Drawing.Edge;

namespace Automata.Simulator.Drawing
{
    using Event;
    using Interface;

    public class AutomataGraph : Graph
    {
        #region Constants
        public const double StateLabelFontSize = 6.0D;
        public const double EdgeLabelFontSize = 4.0D;
        #endregion

        #region Static fields
        private static FieldInfo SegmentsFieldInfo = null;
        #endregion

        #region Fields
        private bool _registerEvents = false;
        private IAutomata _automata;
        private IDictionary<IState, State> _logicToDrawingStateMap = new Dictionary<IState, State>();
        private IDictionary<IStateTransition, Edge> _logicToDrawingTransitionMap = new Dictionary<IStateTransition, Edge>();
        #endregion

        #region Events
        public event Action OnRedraw;
        #endregion

        #region Properties
        public bool IsSaved { get; set; }
        public bool RegisterEvents
        {
            get
            {
                return _registerEvents;
            }
            set
            {
                if (_registerEvents && !value)
                {
                    TeardownEvents();

                    _registerEvents = false;
                }
                else if (!_registerEvents && value)
                {
                    _registerEvents = true;

                    SetupEvents();
                }
            }
        }

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
        public AutomataGraph(IAutomata automata, bool registerEvents = false)
        {
            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");
            RegisterEvents = registerEvents;
        }
        #endregion

        #region Event handling
        private void SetupEvents()
        {
            if (!RegisterEvents)
                return;

            Automata.OnStateAdd += OnStateAdd;
            Automata.OnStateUpdate += OnStateUpdate;
            Automata.OnStateRemove += OnStateRemove;
            Automata.OnTransitionAdd += OnTransitionAdd;
            Automata.OnTransitionUpdate += OnTransitionUpdate;
            Automata.OnTransitionRemove += OnTransitionRemove;
        }

        private void TeardownEvents()
        {
            if (!RegisterEvents)
                return;

            Automata.OnStateAdd -= OnStateAdd;
            Automata.OnStateUpdate -= OnStateUpdate;
            Automata.OnStateRemove -= OnStateRemove;
            Automata.OnTransitionAdd -= OnTransitionAdd;
            Automata.OnTransitionUpdate -= OnTransitionUpdate;
            Automata.OnTransitionRemove -= OnTransitionRemove;
        }

        private void OnStateAdd(object sender, StateEventArgs args)
        {
            AddState(args.State);
        }

        private void OnStateUpdate(object sender, StateEventArgs args)
        {
            if (NodeMap[args.State.Id] is State drawingState)
                IsSaved = false;

            Redraw();
        }

        private void OnStateRemove(object sender, StateEventArgs args)
        {
            RemoveState(args.State);
        }

        private void OnTransitionAdd(object sender, TransitionEventArgs args)
        {
            AddTransition(args.Transition);
        }

        private void OnTransitionUpdate(object sender, TransitionEventArgs args)
        {
            RemoveTransition(args.Transition);
            AddTransition(args.Transition);

            Redraw();
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

            if (_logicToDrawingStateMap.ContainsKey(state))
                throw new ArgumentException("The graph already contains this state!", nameof(state));

            var drawingState = new State(state);

            SetupState(drawingState);

            _logicToDrawingStateMap.Add(state, drawingState);

            NodeMap[state.Id] = drawingState;

            IsSaved = false;

            Redraw();
        }

        private void RemoveState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state), "The logic state can not be null!");

            if (!_logicToDrawingStateMap.ContainsKey(state))
                throw new ArgumentException("The graph doesn't contain this state!", nameof(state));

            RemoveNode(_logicToDrawingStateMap[state]);

            _logicToDrawingStateMap.Remove(state);

            IsSaved = false;

            Redraw();
        }

        private void AddTransition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The logic transition can not be null!");

            if (_logicToDrawingTransitionMap.ContainsKey(transition))
                throw new ArgumentException("The graph already contains this transition!", nameof(transition));

            var sourceState = NodeMap[transition.SourceState.Id] as State;
            var targetState = NodeMap[transition.TargetState.Id] as State;

            if (sourceState == null || targetState == null)
                throw new Exception("You can not add a transition to a non-existant state!");

            var edge = new Edge(sourceState, targetState, transition);

            SetupEdge(edge);

            _logicToDrawingTransitionMap.Add(transition, edge);

            AddPrecalculatedEdge(edge);

            IsSaved = false;

            Redraw();
        }

        private void RemoveTransition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The logic transition can not be null!");

            if (!_logicToDrawingTransitionMap.ContainsKey(transition))
                throw new ArgumentException("The graph doesn't contain this transition!", nameof(transition));

            RemoveEdge(_logicToDrawingTransitionMap[transition]);

            _logicToDrawingTransitionMap.Remove(transition);

            IsSaved = false;

            Redraw();
        }

        public void SetupState(State state)
        {
            state.Label.FontSize = StateLabelFontSize;
            state.DrawNodeDelegate = DrawNode;
        }

        public void SetupEdge(Edge edge)
        {
            edge.Label.FontSize = EdgeLabelFontSize;
            edge.DrawEdgeDelegate = DrawEdge;
        }

        private bool DrawNode(Node node, object graphics)
        {
            var drawGraphics = graphics as Graphics;
            var state = node as State;

            if (drawGraphics == null || state == null)
                return false;

            state.Attr.Shape = Shape.DrawFromGeometry;
            state.Attr.LineWidth = 0.7;

            if (state.IsStartState)
            {
                if (state.Id.Length > 2)
                    state.Label.Width -= 6;
                else
                    state.Label.Width -= 5;
            }

            var curve = new Curve();

            var center = state.GeometryNode.BoundaryCurve.BoundingBox.Center;

            if (state.IsAcceptState)
                AddDiscontinousSegment(curve, new Ellipse(9, 9, center));

            AddDiscontinousSegment(curve, new Ellipse(10, 10, center));

            if (state.IsStartState)
            {
                AddDiscontinousSegment(curve, new LineSegment(center.X - 12, center.Y - 2, center.X - 10, center.Y));
                AddDiscontinousSegment(curve, new LineSegment(center.X - 15, center.Y, center.X - 10, center.Y));
                AddDiscontinousSegment(curve, new LineSegment(center.X - 12, center.Y + 2, center.X - 10, center.Y));
            }

            state.GeometryNode.BoundaryCurve = curve;

            return false;
        }

        private bool DrawEdge(DrawingEdge drawingEdge, object graphics)
        {
            return false;
            /*if (drawingEdge is Edge edge)
            {
                // TODO: kell ez?
            }
            return false;*/
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

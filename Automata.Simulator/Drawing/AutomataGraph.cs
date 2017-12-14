using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Timers;

using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;

using MsaglColor = Microsoft.Msagl.Drawing.Color;

namespace Automata.Simulator.Drawing
{
    using Enum;
    using Event;
    using Interface;
    using Microsoft.Msagl.GraphViewerGdi;
    using Simulation;

    /// <summary>
    /// Defines a graph for the given automata to draw it.
    /// </summary>
    public class AutomataGraph : Graph
    {
        #region Constants
        public const double StateLabelFontSize = 6.0D;
        public const double EdgeLabelFontSize = 4.0D;
        #endregion

        #region Static fields
        /// <summary>
        /// A cache for the segments field reflection data.
        /// </summary>
        private static FieldInfo SegmentsFieldInfo = null;
        #endregion

        #region Fields
        /// <summary>
        /// Variable if the event should be registered for the automata.
        /// </summary>
        private bool _registerEvents = false;

        /// <summary>
        /// The timer for timed stepping.
        /// </summary>
        private Timer _stepTimer = null;

        /// <summary>
        /// The automata to be drawn.
        /// </summary>
        private IAutomata _automata;

        /// <summary>
        /// The drawing state to logic state map.
        /// </summary>
        private IDictionary<IState, State> _logicToDrawingStateMap = new Dictionary<IState, State>();

        /// <summary>
        /// The drawing edte to logic transition map.
        /// </summary>
        private IDictionary<IStateTransition, Edge> _logicToDrawingTransitionMap = new Dictionary<IStateTransition, Edge>();
        #endregion

        #region Events
        /// <summary>
        /// Defines an event handler that is called when a redraw is needed.
        /// </summary>
        public event Action OnRedraw;

        /// <summary>
        /// Defines an event handler that is called when the simulation finished.
        /// </summary>
        public event Action OnSimulationFinished;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets, if the automata that is currently edited should be saved.
        /// </summary>
        public bool IsSaved { get; set; }

        /// <summary>
        /// Gets or sets, if the events of the automata should be registered.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the automata that is drawn.
        /// </summary>
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

        /// <summary>
        /// The current simulation that is ran on the automata.
        /// </summary>
        public ISimulation Simulation { get; private set; }

        /// <summary>
        /// The simulation drawer instance that draws the simulation data on the graphical surface.
        /// </summary>
        public SimulationDrawer SimulationDrawer { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new graph representation for the given automata.
        /// </summary>
        /// <param name="automata">The automata instance.</param>
        /// <param name="registerEvents">If true, the automata's events will be registered.</param>
        public AutomataGraph(IAutomata automata, bool registerEvents = false)
        {
            Automata = automata ?? throw new ArgumentNullException(nameof(automata), "The automata can not be null!");
            RegisterEvents = registerEvents;

            SimulationDrawer = new SimulationDrawer(this);
        }
        #endregion

        #region Event handling
        /// <summary>
        /// Register to the automata's events.
        /// </summary>
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

        /// <summary>
        /// Unregister from the automata's events.
        /// </summary>
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

        /// <summary>
        /// Handles the state add event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="args">The event arguments.</param>
        private void OnStateAdd(object sender, StateEventArgs args)
        {
            AddState(args.State);
        }

        /// <summary>
        /// Handles the state update event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="args">The event arguments.</param>
        private void OnStateUpdate(object sender, StateEventArgs args)
        {
            if (NodeMap[args.State.Id] is State drawingState)
                IsSaved = false;

            Redraw();
        }

        /// <summary>
        /// Handles the state remove event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="args">The event arguments.</param>
        private void OnStateRemove(object sender, StateEventArgs args)
        {
            RemoveState(args.State);
        }

        /// <summary>
        /// Handles the transition add event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="args">The event arguments.</param>
        private void OnTransitionAdd(object sender, TransitionEventArgs args)
        {
            AddTransition(args.Transition);
        }

        /// <summary>
        /// Handles the transition update event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="args">The event arguments.</param>
        private void OnTransitionUpdate(object sender, TransitionEventArgs args)
        {
            RemoveTransition(args.Transition);
            AddTransition(args.Transition);

            Redraw();
        }

        /// <summary>
        /// Handles the transition remove event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="args">The event arguments.</param>
        private void OnTransitionRemove(object sender, TransitionEventArgs args)
        {
            RemoveTransition(args.Transition);
        }
        #endregion

        #region Simulation
        /// <summary>
        /// Starts a new simulation based on the given parameters on the automata.
        /// </summary>
        /// <param name="stepMethod">The simulation step method.</param>
        /// <param name="input">The input symbols array.</param>
        /// <param name="timedDelaySeconds">The delay of the timer in seconds.</param>
        /// <param name="resolver">The ambiguity resolver instance.</param>
        public void StartSimulation(SimulationStepMethod stepMethod, object[] input, int timedDelaySeconds, IAmbiguityResolver resolver = null)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "The input symbols array can not be null!");

            if (Simulation != null)
                throw new Exception("You must stop the previous simulation!");

            var filteredSymbols = new List<object>();

            foreach (var symbol in input)
                if (Automata.Alphabet.ContainsSymbol(symbol))
                    filteredSymbols.Add(symbol);

            Simulation = new SimpleSimulation(Automata, filteredSymbols.ToArray())
            {
                Resolver = resolver
            };

            switch (stepMethod)
            {
                case SimulationStepMethod.Manual:
                    Simulation.OnStep += OnSimulationStep;
                    break;

                case SimulationStepMethod.Instant:
                    Simulation.DoAllSteps();

                    OnSimulationFinished?.Invoke();
                    break;

                case SimulationStepMethod.Timed:
                    Simulation.OnStep += OnSimulationStep;

                    _stepTimer = new Timer(timedDelaySeconds * 1000)
                    {
                        AutoReset = true
                    };
                    _stepTimer.Elapsed += StepTimer_Elapsed;
                    _stepTimer.Start();
                    break;
            }

            ColorizeCurrentStateAndEdges();
        }

        /// <summary>
        /// Handles the timer's elapsed event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void StepTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (StepSimulation() != SimulationStepResult.Success)
            {
                if (_stepTimer != null)
                {
                    _stepTimer.Stop();
                    _stepTimer = null;
                }
            }
        }

        /// <summary>
        /// Stops the current simulation.
        /// </summary>
        public void StopSimulation()
        {
            if (Simulation == null)
                throw new Exception("Can't stop a non-existant simulation!");

            Simulation.OnStep -= OnSimulationStep;
            Simulation = null;

            if (_stepTimer != null)
            {
                _stepTimer.Stop();
                _stepTimer = null;
            }

            ClearColors();

            Redraw();
        }

        /// <summary>
        /// Does a step in the current simulation.
        /// </summary>
        /// <returns></returns>
        public SimulationStepResult StepSimulation()
        {
            if (Simulation == null)
                throw new Exception("Can't step a non-existant simulation!");

            var result = Simulation.Step();
            if (result == SimulationStepResult.Ambiguous)
                result = Simulation.SpecificStep(Simulation.Resolver.Resolve(Simulation));

            ClearColors();

            ColorizeCurrentStateAndEdges();

            if (result == SimulationStepResult.Success)
                return SimulationStepResult.Success;

            if (Simulation.IsFinished)
            {
                OnSimulationFinished?.Invoke();

                return SimulationStepResult.Finished;
            }

            return result;
        }

        /// <summary>
        /// Handles the OnStep event of the simulation.
        /// </summary>
        private void OnSimulationStep()
        {
            ClearColors();

            ColorizeCurrentStateAndEdges();
        }

        /// <summary>
        /// Colorizes the states and transitions based on the simulation's current state.
        /// </summary>
        private void ColorizeCurrentStateAndEdges()
        {
            if (!_logicToDrawingStateMap.ContainsKey(Simulation.CurrentState))
                throw new Exception("The current state of the simulation is not in the graph!");

            var state = _logicToDrawingStateMap[Simulation.CurrentState];

            var color = MsaglColor.Blue;

            if (Simulation.IsFinished)
                color = Simulation.IsInputAccepted ? MsaglColor.Green : MsaglColor.Red;

            state.Attr.Color = color;
            state.Label.FontColor = color;

            foreach (var edge in state.Edges)
            {
                if (edge.SourceNode != state)
                    continue;

                if (edge is Edge transition && transition.LogicTransition.HandlesSymbol(Simulation.CurrentInputSymbol))
                {
                    transition.Attr.Color = MsaglColor.Cyan;
                    transition.Label.FontColor = MsaglColor.Cyan;
                }
            }

            Redraw();
        }
        #endregion

        #region Graph
        /// <summary>
        /// Sets up the graph representation based on the automata.
        /// </summary>
        private void SetupGraph()
        {
            foreach (var logicState in Automata.States)
                AddState(logicState);

            foreach (var logicTransition in Automata.Transitions)
                AddTransition(logicTransition);
        }

        /// <summary>
        /// Tears down the graph representation.
        /// </summary>
        private void TeardownGraph()
        {
            foreach (var logicState in Automata.States)
                RemoveState(logicState);

            foreach (var logicTransition in Automata.Transitions)
                RemoveTransition(logicTransition);

            if (NodeCount > 0 || EdgeCount > 0)
                throw new Exception("The automata graph is in an invalid state!");
        }

        /// <summary>
        /// Adds a new state to the graph representation.
        /// </summary>
        /// <param name="state">The state instance.</param>
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

        /// <summary>
        /// Removes a state from the graph representation.
        /// </summary>
        /// <param name="state">The state instance.</param>
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

        /// <summary>
        /// Adds a new transition to the graph representation.
        /// </summary>
        /// <param name="transition">The transition instance.</param>
        private void AddTransition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition), "The logic transition can not be null!");

            if (transition.SourceState == null)
                throw new ArgumentNullException(nameof(transition), "The logic transition's source state can not be null!");

            if (transition.TargetState == null)
                throw new ArgumentNullException(nameof(transition), "The logic transition's target state can not be null!");

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

        /// <summary>
        /// Removes a transition from the graph representation.
        /// </summary>
        /// <param name="transition">The transition instance.</param>
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
        #endregion

        #region Drawing
        /// <summary>
        /// Redraws the automata.
        /// </summary>
        public void Redraw()
        {
            OnRedraw?.Invoke();
        }

        /// <summary>
        /// Draws the automata on the given graphical surface.
        /// </summary>
        /// <param name="graphics">The graphical surface.</param>
        /// <param name="left">The left coordinate to draw from.</param>
        /// <param name="top">The top coordinate to draw from.</param>
        /// <param name="width">The width of the surface.</param>
        /// <param name="height">The height of the surface.</param>
        public void DrawAutomata(Graphics graphics, int left, int top, int width, int height)
        {
            var renderer = new GraphRenderer(this);
            renderer.CalculateLayout();

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphics.Clear(System.Drawing.Color.White);

            if (Simulation != null)
                SimulationDrawer.DrawSimulationData(graphics, left, top + height - SimulationDrawer.SimulationBarHeight, width);

            renderer.Render(graphics, left, top, width, height - SimulationDrawer.SimulationBarHeight);
        }

        /// <summary>
        /// Sets up the state with the default values.
        /// </summary>
        /// <param name="state">The state instance.</param>
        public void SetupState(State state)
        {
            state.Label.FontSize = StateLabelFontSize;
            state.DrawNodeDelegate = DrawNode;
        }

        /// <summary>
        /// Sets up the edge with the default values.
        /// </summary>
        /// <param name="edge">The edge instance</param>
        public void SetupEdge(Edge edge)
        {
            edge.Label.FontSize = EdgeLabelFontSize;
        }

        /// <summary>
        /// Handles the drawing of the state.
        /// </summary>
        /// <param name="node">The drawing node.</param>
        /// <param name="graphics">The graphic surface.</param>
        /// <returns>False, so the default drawing method is not called.</returns>
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

        /// <summary>
        /// Removes every color from the graph, resetting back to the default black.
        /// </summary>
        private void ClearColors()
        {
            foreach (var state in Nodes)
            {
                state.Attr.Color = MsaglColor.Black;
                state.Attr.FillColor = MsaglColor.Transparent;

                state.Label.FontColor = MsaglColor.Black;

                foreach (var edge in state.Edges)
                {
                    edge.Attr.Color = MsaglColor.Black;
                    edge.Label.FontColor = MsaglColor.Black;
                }
            }
        }
        #endregion

        #region Reflection
        /// <summary>
        /// Helper function to add a new curve segment to a container.
        /// </summary>
        /// <param name="container">The curve container.</param>
        /// <param name="curve">The curve segment.</param>
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

        /// <summary>
        /// Returns the list of segments of a curve container.
        /// </summary>
        /// <param name="curve">The curve container.</param>
        /// <returns>The hidden list of segments.</returns>
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

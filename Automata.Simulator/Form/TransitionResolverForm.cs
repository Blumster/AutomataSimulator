using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

using Microsoft.Msagl.GraphViewerGdi;

using MsaglColor = Microsoft.Msagl.Drawing.Color;

namespace Automata.Simulator.Form
{
    using Drawing;
    using Interface;

    /// <summary>
    /// Defines a form to manually resolve ambiguous transitions.
    /// </summary>
    public partial class TransitionResolverForm : WinForm
    {
        #region Fields
        /// <summary>
        /// Field to store the combobox's items map.
        /// </summary>
        private IList<IStateTransition> _comboBoxSelectionList = new List<IStateTransition>();
        #endregion

        #region Properties
        /// <summary>
        /// The current simulation's automata.
        /// </summary>
        public IAutomata Automata
        {
            get
            {
                return Simulation.Automata;
            }
        }

        /// <summary>
        /// The current simulation.
        /// </summary>
        public ISimulation Simulation { get; }

        /// <summary>
        /// The resolved transition.
        /// </summary>
        public IStateTransition SelectedTransition { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new resolver form.
        /// </summary>
        /// <param name="simulation"></param>
        public TransitionResolverForm(ISimulation simulation)
        {
            Simulation = simulation ?? throw new ArgumentNullException(nameof(simulation), "The simulation can not be null!");

            InitializeComponent();

            SetupTransitionComboBox();
        }
        #endregion

        #region Control event handlers
        /// <summary>
        /// Handles the draw panel's paint event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        /// <summary>
        /// Handles the transition combobox's change event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void TransitionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        /// <summary>
        /// Handles the select button's click event.
        /// </summary>
        /// <param name="sender">The triggerer object.</param>
        /// <param name="e">The event arguments.</param>
        private void SelectTransitionButton_Click(object sender, EventArgs e)
        {
            if (_comboBoxSelectionList.Count <= TransitionComboBox.SelectedIndex)
                return;

            SelectedTransition = _comboBoxSelectionList[TransitionComboBox.SelectedIndex];

            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets up the transition combobox's items.
        /// </summary>
        private void SetupTransitionComboBox()
        {
            _comboBoxSelectionList.Clear();
            TransitionComboBox.Items.Clear();

            var sourceState = Simulation.CurrentState;

            foreach (var transition in sourceState.OutTransitions)
            {
                if (!transition.HandlesSymbol(Simulation.CurrentInputSymbol))
                    continue;

                _comboBoxSelectionList.Add(transition);

                TransitionComboBox.Items.Add($"{transition.SourceState.Id}{Simulation.CurrentInputSymbol} -> {transition.TargetState.Id}");
            }

            if (TransitionComboBox.Items.Count > 0)
                TransitionComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Draws the preview automata based on the form fields' value.
        /// </summary>
        private void DrawPreviewAutomata()
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            var graph = new AutomataGraph(Automata);

            if (TransitionComboBox.SelectedIndex != -1 && _comboBoxSelectionList.Count > TransitionComboBox.SelectedIndex)
            {
                var sourceDrawingState = graph.FindNode(Simulation.CurrentState.Id) as State;

                foreach (var outEdge in sourceDrawingState.Edges)
                {
                    if (outEdge is Edge edge && edge.SourceNode == sourceDrawingState && edge.LogicTransition.HandlesSymbol(Simulation.CurrentInputSymbol))
                    {
                        if (edge.LogicTransition == _comboBoxSelectionList[TransitionComboBox.SelectedIndex])
                        {
                            edge.Attr.Color = MsaglColor.Blue;
                            edge.Label.FontColor = MsaglColor.Blue;
                        }
                        else
                        {
                            edge.Attr.Color = MsaglColor.Orange;
                            edge.Label.FontColor = MsaglColor.Orange;
                        }
                    }
                    else
                    {
                        outEdge.Attr.Color = MsaglColor.Black;
                        outEdge.Label.FontColor = MsaglColor.Black;
                    }
                }
            }

            var renderer = new GraphRenderer(graph);
            renderer.CalculateLayout();

            using (var graphics = DrawPanel.CreateGraphics())
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                renderer.Render(graphics, 0, 0, DrawPanel.Width, DrawPanel.Height);
            }
        }
        #endregion
    }
}

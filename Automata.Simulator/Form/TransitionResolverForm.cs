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

    public partial class TransitionResolverForm : WinForm
    {
        #region Fields
        private IList<IStateTransition> _comboBoxSelectionList = new List<IStateTransition>();
        #endregion

        #region Properties
        public IAutomata Automata { get; }
        public ISimulation Simulation { get; }
        public IStateTransition SelectedTransition { get; private set; }
        #endregion

        #region Constructors
        public TransitionResolverForm(ISimulation simulation)
        {
            Simulation = simulation;
            Automata = Simulation.Automata;

            InitializeComponent();

            SetupTransitionComboBox();
        }
        #endregion

        #region Control event handlers
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void TransitionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

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

                TransitionComboBox.Items.Add($"{transition.SourceState.Id} -> {transition.TargetState.Id}: {transition.Label}");
            }

            if (TransitionComboBox.Items.Count > 0)
                TransitionComboBox.SelectedIndex = 0;
        }

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

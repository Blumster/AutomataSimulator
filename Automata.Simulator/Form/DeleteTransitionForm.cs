using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinForm = System.Windows.Forms.Form;

using Microsoft.Msagl.GraphViewerGdi;

using MsaglColor = Microsoft.Msagl.Drawing.Color;

namespace Automata.Simulator.Form
{
    using Drawing;
    using Interface;
    using Pushdown;

    public partial class DeleteTransitionForm : WinForm
    {
        #region Fields
        private IList<IStateTransition> _comboBoxSelectionList = new List<IStateTransition>();
        #endregion

        #region Properties
        public IAutomata Automata { get; }
        public bool IsPushdownAutomata
        {
            get
            {
                return Automata is PushdownAutomata;
            }
        }
        #endregion

        #region Constructors
        public DeleteTransitionForm(IAutomata automata)
        {
            Automata = automata;

            InitializeComponent();

            foreach (var state in Automata.States)
                SourceStateIdComboBox.Items.Add(state.Id);

            SourceStateIdComboBox.SelectedIndex = 0;

            SetupTransitionComboBox();
        }
        #endregion

        #region Control event handlers
        private void SourceStateIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupTransitionComboBox();

            DrawPreviewAutomata();
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void TransitionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPreviewAutomata();
        }

        private void DeleteTransitionButton_Click(object sender, EventArgs e)
        {
            if (_comboBoxSelectionList.Count <= TransitionComboBox.SelectedIndex)
                return;

            Automata.RemoveTrasition(_comboBoxSelectionList[TransitionComboBox.SelectedIndex]);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelDeletionButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region Methods
        private void SetupTransitionComboBox()
        {
            _comboBoxSelectionList.Clear();
            TransitionComboBox.Items.Clear();

            if (SourceStateIdComboBox.SelectedIndex == -1)
                return;

            var sourceState = Automata.GetState(SourceStateIdComboBox.SelectedItem as string);

            foreach (var transition in sourceState.OutTransitions)
            {
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

            if (SourceStateIdComboBox.SelectedIndex != -1 && TransitionComboBox.SelectedIndex != -1 && _comboBoxSelectionList.Count > TransitionComboBox.SelectedIndex)
            {
                var sourceDrawingState = graph.FindNode(SourceStateIdComboBox.SelectedItem as string) as State;

                foreach (var outEdge in sourceDrawingState.Edges)
                {
                    if (outEdge is Edge edge && edge.SourceNode == sourceDrawingState)
                    {
                        if (edge.LogicTransition == _comboBoxSelectionList[TransitionComboBox.SelectedIndex])
                        {
                            edge.Attr.Color = MsaglColor.Red;
                            edge.Label.FontColor = MsaglColor.Red;
                        }
                        else
                        {
                            edge.Attr.Color = MsaglColor.Orange;
                            edge.Label.FontColor = MsaglColor.Orange;
                        }
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
